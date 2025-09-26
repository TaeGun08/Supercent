using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDecisionState : CustomerStateBase
{
    public override void StateEnter()
    {
        int count = InGameManager.GetQueueCount(InGameManager.CHECKOUT_INDEX);
        count = Mathf.Max(count, 0);
        
        Vector3 pos = InGameManager.POSTable.GetPos
            (GOING_CHECKOUT_INDEX, count);
        
        Agent.SetDestination(pos);
        InGameManager.EnqueueCustomer(InGameManager.CHECKOUT_INDEX, Controller);
    }

    public override void OnUpdate()
    {
        if (Agent.pathPending || (Agent.remainingDistance <= Agent.stoppingDistance) == false) return;
        Controller.ChangeState<CustomerWaitingCheckoutState>();
    }
    
    public override void StateExit()
    {
    }
}
