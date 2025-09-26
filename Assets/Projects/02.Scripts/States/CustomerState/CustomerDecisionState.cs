using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDecisionState : CustomerStateBase
{
    public override void StateEnter()
    {
        int count = InGameManager.FirstWaitingCustomer(CHECKOUT_INDEX) != null ? 1 : 0;
        
        Vector3 pos = InGameManager.POSTable.GetPos(GOING_CHECKOUT_INDEX, InGameManager.WaitingCustomers[CHECKOUT_INDEX].Count + count);
        Agent.SetDestination(pos);
        
        InGameManager.WaitingCustomers[CHECKOUT_INDEX].Enqueue(Controller);
    }

    public override void OnUpdate()
    {
        if (Agent.pathPending || !(Agent.remainingDistance <= Agent.stoppingDistance)) return;
        Controller.ChangeState<CustomerWaitingCheckoutState>();
    }
    
    public override void StateExit()
    {
    }
}
