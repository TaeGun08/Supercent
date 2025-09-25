using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : SingletonBehaviour<InGameManager>
{
    [Header("JoyStick")]
    [SerializeField] private JoyStickController joyStickController;
    public JoyStickController JoyStickController => joyStickController;
    
    [Space]
    [Header("BasketTable")]
    [SerializeField] private BasketTable basketTable;
    public BasketTable BasketTable => basketTable;
    
    [Space]
    [Header("Generator")]
    [SerializeField] private CustomerGenerator customerGenerator;
    
    public Queue<CustomerController> BreadWaitingCustomers { get; private set; } = new Queue<CustomerController>();
    public Queue<CustomerController> CheckOutWaitingCustomers { get; private set; } = new Queue<CustomerController>();

    private CustomerController breadWaitingCustomer;
    private CustomerController checkOutWaitingCustomer;
    
    [Space]
    [Header("Customer Info")]
    [SerializeField] private int maxBreadWaiting;
    [SerializeField] private int maxCheckOutWaiting;

    public CustomerController FirstBreadCustomer()
    {
        if (breadWaitingCustomer == null && BreadWaitingCustomers.Count > 0)
        {
            breadWaitingCustomer = BreadWaitingCustomers.Dequeue();
        }
        
        return breadWaitingCustomer;
    }

    public void GoCheckOutCustomer()
    {
        breadWaitingCustomer = null;

        if (BreadWaitingCustomers.Count > 0)
        {
            FirstBreadCustomer().ChangeState<CustomerPickingBreadState>();

            for (int i = BreadWaitingCustomers.Count + 1; i < maxBreadWaiting; i++)
            {
                customerGenerator.CustomerPool.Get(customerGenerator.transform.position, Quaternion.identity).gameObject.SetActive(true);
            }
        }
    }
}