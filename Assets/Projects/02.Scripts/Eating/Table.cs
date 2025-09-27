using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Table : OnTriggerInteraction
{
    private InGameManager InGameManager;

    private Customer currentEatingCustomer;

    [Header("Table Settings")] [SerializeField]
    private DOAnimation animTarget;

    [SerializeField] private Transform putDownTrs;
    public Transform PutDownTrs => putDownTrs;
    [SerializeField] private Transform sitTrs;
    public Transform SitTrs => sitTrs;
    [Space] [SerializeField] private GameObject trashPrefab;
    private bool isDirty;
    [SerializeField] private GetMoneyArea getMoneyArea;

    private bool inside;

    private void Start()
    {
        InGameManager = InGameManager.Instance;

        trashPrefab.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(OnTableCustomerMoveCoroutine());
    }

    private IEnumerator OnTableCustomerMoveCoroutine()
    {
        currentEatingCustomer?.GoingEatingTable(sitTrs);
        NavMeshAgent agent = currentEatingCustomer?.Agent;
        WaitForSeconds wfs = new WaitForSeconds(1f);

        while (agent != null && (agent.pathPending || agent.remainingDistance > agent.stoppingDistance))
        {
            yield return wfs;
        }

        currentEatingCustomer?.CustomerController.ChangeState<CustomerEatingState>();
    }

    protected override void TriggerEnter(Collider other)
    {
        inside = true;
        StartCoroutine(TableEtnerCoroutine());
    }

    private IEnumerator TableEtnerCoroutine()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.1f);

        while (inside)
        {
            yield return wfs;

            if (!isDirty) continue;
            animTarget.SetRotateAnimation(Vector3.zero, 0f);
            isDirty = false;
        }
    }

    protected override void TriggerExit(Collider other)
    {
        inside = false;
    }

    public void SetCustomer(Customer customer)
    {
        if (currentEatingCustomer != null) return;
        currentEatingCustomer = customer;
        currentEatingCustomer.SitTable = this;
    }

    public bool EatingCustomerEmpty()
    {
        return currentEatingCustomer == null;
    }

    public void FinishEating()
    {
        isDirty = false;
        trashPrefab.SetActive(true);
        animTarget.SetRotateAnimation(new Vector3(0f, 130f, 0f), 0f); 
        InGameManager.MoneyGenerator.Generate(10, getMoneyArea);
    }
}