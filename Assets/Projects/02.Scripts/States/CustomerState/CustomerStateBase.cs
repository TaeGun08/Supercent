using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class CustomerStateBase : MonoBehaviour
{
    protected const int BREAD_INDEX = 0;
    protected const int CHECKOUT_INDEX = 1;
    protected const int EATING_INDEX = 2;
    
    protected const int GOING_CHECKOUT_INDEX = 0;
    protected const int GOING_EATING_INDEX = 1;
    
    public Customer Customer { get; private set; }
    public CustomerController Controller { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    protected InGameManager InGameManager;
    
    protected virtual void Start()
    {
        InGameManager = InGameManager.Instance;
    }
    
    public virtual void Initialize(CustomerContext context)
    {
        Customer = context.Customer;
        Controller = context.Controller;
        Animator = context.Animator;
        Rigidbody = context.Rigidbody;
        Agent = context.Agent;
    }
    
    public abstract void StateEnter();

    public virtual void OnUpdate() {}

    public abstract void StateExit();
}
