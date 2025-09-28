using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Table : OnTriggerInteraction
{
    private Customer currentEatingCustomer;

    [Header("Table Settings")] 
    [SerializeField] private DOAnimation animTarget;
    [SerializeField] private Transform putDownTrs;
    public Transform PutDownTrs => putDownTrs;
    [SerializeField] private Transform sitTrs;
    public Transform SitTrs => sitTrs;
    [Space] 
    [SerializeField] private GameObject trashPrefab;
    [SerializeField] private ParticleSystem cleanVfx;
    private bool isDirty;
    [Space]
    [SerializeField] private GetMoneyArea getMoneyArea;

    private bool inside;

    private void Start()
    {
        trashPrefab.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(OnEnableSeatAvailableCoroutine());
    }

    private IEnumerator OnEnableSeatAvailableCoroutine()
    {
        WaitForSeconds wfs = new WaitForSeconds(1f);
        while (gameObject.activeSelf)
        {
            yield return wfs;

            if (isDirty) continue;
            
            if (currentEatingCustomer != null 
                && currentEatingCustomer.Controller.CurrentState is CustomerWaitingState)
            {

                currentEatingCustomer.SitTable = this;
                currentEatingCustomer.Controller.ChangeState<CustomerGoingToEatState>();
            }
            else if (currentEatingCustomer == null)
            {
                CustomerController customer = InGameManager.Instance.PeekCustomer(InGameManager.EATING_INDEX);
                currentEatingCustomer = customer == null ? null : customer.Customer;
                
                if (currentEatingCustomer == null || currentEatingCustomer.Controller.CurrentState is not CustomerWaitingState) continue;
                currentEatingCustomer.SitTable = this;
                currentEatingCustomer.Controller.ChangeState<CustomerGoingToEatState>();
            }
        }
    }

    protected override void TriggerEnter(Collider other)
    {
        inside = true;
        StartCoroutine(TableEnterCoroutine());
    }

    private IEnumerator TableEnterCoroutine()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.1f);

        while (inside)
        {
            yield return wfs;
            if (!isDirty) continue;

            trashPrefab.SetActive(false);
            cleanVfx.Play();
            animTarget.SetRotateAnimation(new Vector3(0f, 180f, 0f), 0.5f);
            isDirty = false;

            CustomerController customer = InGameManager.Instance.PeekCustomer(InGameManager.EATING_INDEX);
            if (customer == null) break;
            currentEatingCustomer = customer.Customer;
            currentEatingCustomer.SitTable = this;
            currentEatingCustomer.Controller.ChangeState<CustomerGoingToEatState>();
        }
    }

    protected override void TriggerExit(Collider other)
    {
        inside = false;
    }

    public void SetCustomer(Customer customer)
    {
        currentEatingCustomer = customer;
        currentEatingCustomer.SitTable = this;
    }

    public bool EatingCustomerEmpty()
    {
        return currentEatingCustomer == null;
    }

    public bool IsSeatAvailable()
    {
        return currentEatingCustomer && !isDirty && InGameManager.Instance.EatingHole.gameObject.activeSelf;
    }

    public void FinishEating()
    {
        isDirty = true;
        currentEatingCustomer = null;
        trashPrefab.SetActive(true);
        animTarget.RotateAnimation();
        InGameManager.Instance.MoneyGenerator.Generate(10, getMoneyArea);
    }
}