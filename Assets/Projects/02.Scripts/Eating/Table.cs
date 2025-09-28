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
    [Space] 
    [SerializeField] private GameObject trashPrefab;
    private bool isDirty;
    public bool IsDirty => isDirty;
    [SerializeField] private GetMoneyArea getMoneyArea;

    private bool inside;

    private void Start()
    {
        trashPrefab.SetActive(false);
    }

    private void OnEnable()
    {
        CustomerController customer = InGameManager.Instance.PeekCustomer(InGameManager.EATING_INDEX);
        currentEatingCustomer = customer == null ? null : customer.Customer;
        OnTableCustomerMove();
    }

    private IEnumerator OnTableCustomerMoveCoroutine()
    {
        if (isDirty) yield break;

        currentEatingCustomer.SitTable = this;

        NavMeshAgent agent = currentEatingCustomer.Agent;
        agent.SetDestination(sitTrs.position);
        InGameManager.Instance.NextStep<CustomerWaitingState>(InGameManager.EATING_INDEX);
        InGameManager.Instance.ArrangeWaitingLine(InGameManager.EATING_INDEX);

        yield return null;

        WaitForSeconds wfs = new WaitForSeconds(0.1f);

        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return wfs;
        }

        currentEatingCustomer.CustomerController.ChangeState<CustomerEatingState>();
    }

    public void OnTableCustomerMove()
    {
        if (currentEatingCustomer == null) return;
        StartCoroutine(OnTableCustomerMoveCoroutine());
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
            animTarget.SetRotateAnimation(Vector3.zero, 0f);
            isDirty = false;

            CustomerController customer = InGameManager.Instance.PeekCustomer(InGameManager.EATING_INDEX);
            if (customer == null) break;
            currentEatingCustomer = customer.Customer;
            OnTableCustomerMove();
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

    public void FinishEating()
    {
        isDirty = true;
        trashPrefab.SetActive(true);
        animTarget.RotateAnimation();
        currentEatingCustomer = null;
        InGameManager.Instance.MoneyGenerator.Generate(10, getMoneyArea);
    }
}