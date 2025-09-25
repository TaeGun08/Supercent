using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDecisionState : CustomerStateBase
{
    public override void StateEnter()
    {
        Vector3 pos = InGameManager.POSTable.GetPos(0, InGameManager.WaitingCustomers[1].Count);
        Agent.SetDestination(pos);
        
        InGameManager.WaitingCustomers[1].Enqueue(Controller);
    }

    public override void OnUpdate()
    {
        if (Agent.pathPending || !(Agent.remainingDistance <= Agent.stoppingDistance)) return;
        Controller.ChangeState<CustomerCheckingOutState>();
    }
    
    public override void StateExit()
    {
    }
}
