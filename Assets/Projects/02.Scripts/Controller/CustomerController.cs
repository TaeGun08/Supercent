using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CustomerContext
{
    public Customer Customer { get; set; }
    public CustomerController Controller { get; set; }
    public Animator Animator { get; set; }
    public Rigidbody Rigidbody { get; set; }
    public NavMeshAgent Agent { get; set; }
}

public class CustomerController : MonoBehaviour
{
    private CustomerStateBase currentState;
    public CustomerStateBase CurrentState => currentState;
    private CustomerStateBase[] states;

    public NavMeshAgent Agent { get; private set; }

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        
        var context = new CustomerContext()
        {
            Customer = GetComponent<Customer>(),
            Controller = this,
            Animator = GetComponentInChildren<Animator>(),
            Rigidbody = GetComponent<Rigidbody>(),
            Agent = this.Agent,
        };
        
        states = GetComponentsInChildren<CustomerStateBase>();

        foreach (var state in states)
        {
            state.Initialize(context);
        }
        
        currentState = states[0];
    }

    private void OnEnable()
    {
        currentState?.StateEnter();
    }

    private void Update()
    {
        currentState?.OnUpdate();
    }

    public void ChangeState<T>() where T : CustomerStateBase
    {
        if (enabled == false) return;
        
        currentState?.StateExit();
        currentState = states.FirstOrDefault(state => state is T);
        
        if (currentState == null) currentState = states[0];
        
        currentState?.StateEnter();
    }
}
