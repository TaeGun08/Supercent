using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : SingletonBehaviour<InGameManager>
{
    [Header("JoyStick")] [SerializeField] private JoyStickController joyStickController;
    public JoyStickController JoyStickController => joyStickController;

    [Space] [Header("BasketTable")] [SerializeField]
    private BasketTable basketTable;

    public BasketTable BasketTable => basketTable;

    [Space] [Header("POSTable")] [SerializeField]
    private POSTable pOSTable;

    public POSTable POSTable => pOSTable;

    [Space] [Header("Generator")] [SerializeField]
    private CustomerGenerator customerGenerator;

    public List<Queue<CustomerController>> WaitingCustomers { get; private set; } =
        new List<Queue<CustomerController>>();

    private CustomerController[] waitingCustomer;

    [Space] [Header("Customer Info")] [SerializeField]
    private int waitingCustomerSize;

    [SerializeField] private int maxBreadWaiting;
    [SerializeField] private int maxCheckOutWaiting;
    [SerializeField] private int maxEatingWaiting;

    protected override void Awake()
    {
        base.Awake();

        waitingCustomer = new CustomerController[waitingCustomerSize];
        for (int i = 0; i < waitingCustomerSize; i++)
        {
            WaitingCustomers.Add(new Queue<CustomerController>());
        }
    }

    public CustomerController FirstWaitingCustomer(int index)
    {
        if (waitingCustomer[index] == null && WaitingCustomers[index].Count > 0)
        {
            waitingCustomer[index] = WaitingCustomers[index].Dequeue();
        }

        return waitingCustomer[index];
    }

    public void GoCheckOutOrEatingCustomer(int index)
    {
        waitingCustomer[index] = null;

        if (WaitingCustomers[index].Count > 0)
        {
            FirstWaitingCustomer(index).ChangeState<CustomerPickingBreadState>();
        }
        
        if (WaitingCustomers[1].Count < maxCheckOutWaiting)
        {
            customerGenerator.Generate();
        }
    }
}