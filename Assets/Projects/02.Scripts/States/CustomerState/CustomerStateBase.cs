using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class CustomerStateBase : MonoBehaviour
{
    public CustomerController Controller { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    
    protected InGameManager InGameManager { get; private set; }

    protected virtual void Start()
    {
        InGameManager = InGameManager.Instance;
    }
    
    public virtual void Initialize(CustomerContext context)
    {
        Controller = context.Controller;
        Animator = context.Animator;
        Rigidbody = context.Rigidbody;
        Agent = context.Agent;
    }
    
    public abstract void StateEnter();

    public virtual void OnUpdate() {}

    public abstract void StateExit();
}
