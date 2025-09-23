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
    
    public virtual void Initialize(CustomerContext context)
    {
        Controller = context.Controller;
        Animator = context.Animator;
        Rigidbody = context.Rigidbody;
        Agent = context.Agent;
    }
    
    public abstract void StateEnter();
    public abstract void StateExit();
}
