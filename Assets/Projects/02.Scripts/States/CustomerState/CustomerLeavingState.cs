using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLeavingState : CustomerStateBase
{
    public override void StateEnter()
    {
        Animator.SetFloat(MOVE, 1);
        Customer.PlayEmoji();
        Agent.SetDestination(new Vector3(2.4f, 0.5f, 12f));
    }
    
    public override void OnUpdate()
    {
        if (Agent.pathPending || Agent.remainingDistance > Agent.stoppingDistance) return;
        
        Customer.gameObject.SetActive(false);
        Customer.GetPaperBag?.Return();
        InGameManager.CustomerGenerator.Return(Customer);
    }

    public override void StateExit()
    {
    }
}
