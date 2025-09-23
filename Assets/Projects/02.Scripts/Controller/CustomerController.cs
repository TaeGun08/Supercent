using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CustomerContext
{
    public CustomerController Controller { get; set; }
    public Animator Animator { get; set; }
    public Rigidbody Rigidbody { get; set; }
    public NavMeshAgent Agent { get; set; }
}

public class CustomerController : MonoBehaviour
{
    private CustomerStateBase currentState;
    private CustomerStateBase[] states;
    
    private void Awake()
    {
        var context = new CustomerContext()
        {
            Controller = this,
            Animator = GetComponentInChildren<Animator>(),
            Rigidbody = GetComponent<Rigidbody>(),
            Agent = GetComponent<NavMeshAgent>(),
        };
        
        states = GetComponentsInChildren<CustomerStateBase>();

        foreach (var state in states)
        {
            state.Initialize(context);
        }
        
        currentState = states[0];
        currentState?.StateEnter();
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
