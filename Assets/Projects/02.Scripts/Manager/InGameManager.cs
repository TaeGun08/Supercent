using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : SingletonBehaviour<InGameManager>
{
    public const int BREAD_INDEX = 0;
    public const int CHECKOUT_INDEX = 1;
    public const int EATING_INDEX = 2;
    
    [Header("JoyStick")]
    [SerializeField] private JoyStickController joyStickController;
    public JoyStickController JoyStickController => joyStickController;

    [Space]
    [Header("BasketTable")] 
    [SerializeField] private BasketTable basketTable;
    public BasketTable BasketTable => basketTable;

    [Space] 
    [Header("POSTable")] 
    [SerializeField] private POSTable pOSTable;
    public POSTable POSTable => pOSTable;

    [Space] 
    [Header("EatingHole")] 
    [SerializeField] private EatingHole eatingHole;
    public EatingHole EatingHole => eatingHole;

    [Space] 
    [Header("Generator")] 
    [SerializeField] private CustomerGenerator customerGenerator;
    public CustomerGenerator CustomerGenerator => customerGenerator;
    [SerializeField] private PaperBagGenerator paperBagGenerator;
    public PaperBagGenerator PaperBagGenerator => paperBagGenerator;
    [SerializeField] private BreadGenerator breadGenerator;
    public BreadGenerator BreadGenerator => breadGenerator;
    [SerializeField] private MoneyGenerator moneyGenerator;
    public MoneyGenerator MoneyGenerator => moneyGenerator;

    [Space] 
    [Header("Customer Info")] 
    [SerializeField] private int waitingCustomerSize;
    [SerializeField] private int maxCheckOutWaiting;
    [SerializeField] private int maxEatingWaiting;
    
    public List<Queue<CustomerController>> WaitingCustomers { get; private set; } =
        new List<Queue<CustomerController>>();
    
    public bool CheckingOut { get; set; }

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < waitingCustomerSize; i++)
        {
            WaitingCustomers.Add(new Queue<CustomerController>());
        }
    }

    private Queue<CustomerController> GetWaitingQueue(int index)
    {
        return WaitingCustomers[index];
    }

    public void DequeueCustomer(int index)
    {
        GetWaitingQueue(index).Dequeue();
    }
    
    public int GetQueueCount(int index)
    {
        return WaitingCustomers[index].Count;
    }

    public void EnqueueCustomer(int index, CustomerController customer)
    {
        GetWaitingQueue(index).Enqueue(customer);
    }

    public CustomerController PeekCustomer(int index)
    {
        if(GetQueueCount(index) == 0) return null;
        return GetWaitingQueue(index).Peek();
    }

    public bool CustomerChecker(int index, CustomerController customer)
    {
        CustomerController controller = GetWaitingQueue(index).Peek();
        return controller == customer;
    }

    /// <summary>
    /// 다음 해당 대기열의 다음 Customer의 상태 지정 필요함
    /// </summary>
    /// <param name="index"></param>
    /// <typeparam name="T"></typeparam>
    public void NextStep<T>(int index) where T : CustomerStateBase
    {
        if (GetQueueCount(index) == 0) return;
        DequeueCustomer(index);
        var nextCustomer = PeekCustomer(index);
        nextCustomer?.ChangeState<T>();
    }

    public void ArrangeWaitingLine(int index)
    {
        var queue = GetWaitingQueue(index);
        
        if (queue.Count == 0) return;
        
        int count = 0;
        foreach (var customer in queue)
        {
            var position = pOSTable.GetPos(index - 1,count);
            customer.Agent.SetDestination(position);
            count++;
        }
    }

    public bool MaxBasketSlotAndMaxCheckOutWaiting()
    {
        return basketTable.SlotCheck() && GetQueueCount(CHECKOUT_INDEX) < maxCheckOutWaiting;
    }

    public bool MaxEatingWaiting()
    {
        return GetQueueCount(EATING_INDEX) < maxEatingWaiting;
    }
}