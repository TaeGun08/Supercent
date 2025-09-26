using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : SingletonBehaviour<InGameManager>
{
    protected const int BREAD_INDEX = 0;
    protected const int CHECKOUT_INDEX = 1;
    protected const int EATING_INDEX = 2;
    
    protected const int GOING_CHECKOUT_INDEX = 0;
    
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
    public CustomerGenerator CustomerGenerator => customerGenerator;
    [SerializeField] private PaperBagGenerator paperBagGenerator;
    public PaperBagGenerator PaperBagGenerator => paperBagGenerator;
    [SerializeField] private BreadGenerator breadGenerator;
    public BreadGenerator BreadGenerator => breadGenerator;
    [SerializeField] private MoneyGenerator moneyGenerator;
    public MoneyGenerator MoneyGenerator => moneyGenerator;

    [Space] [Header("Customer Info")] [SerializeField]
    private int waitingCustomerSize;
    [SerializeField] private int maxBreadWaiting;
    [SerializeField] private int maxCheckOutWaiting;
    [SerializeField] private int maxEatingWaiting;
    
    public List<Queue<CustomerController>> WaitingCustomers { get; private set; } =
        new List<Queue<CustomerController>>();

    private CustomerController[] waitingCustomer;
    
    public bool CheckingOut { get; set; }

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
        if (waitingCustomer[index] == null)
        {
            if (WaitingCustomers[index].Count <= 0) return null;
            
            waitingCustomer[index] = WaitingCustomers[index].Dequeue();
        }

        return waitingCustomer[index];
    }

    public void CustomerBehaviour<T>(int index) where T : CustomerStateBase
    {
        waitingCustomer[index] = null;

        FirstWaitingCustomer(index)?.ChangeState<T>();
    }

    public void MaxCustomerCheckAndGenerate()
    {
         if (WaitingCustomers[BREAD_INDEX].Count >= maxBreadWaiting || WaitingCustomers[CHECKOUT_INDEX].Count >= maxCheckOutWaiting) return;
         CustomerGenerator.Generate();
    }

    public void ArrangeWaitingLine(int index)
    {
        int count = 0;
        foreach (var customer in WaitingCustomers[index])
        {
            customer.Agent.SetDestination(pOSTable.GetPos(GOING_CHECKOUT_INDEX, count));
            count++;
        }
    }
}