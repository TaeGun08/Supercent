using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEnteringState : CustomerStateBase
{
    private Transform centerPoint;

    private bool reachedCenter;

    public override void StateEnter()
    {
        centerPoint = InGameManager.Instance.transform;

        Transform trs = InGameManager.Instance.BasketTable.GetAvailableSlot();
        Customer.MoveTargetTrs = trs;
        
        Agent.SetDestination(centerPoint.position);
    }

    public override void OnUpdate()
    {
        if (Agent.pathPending || !(Agent.remainingDistance <= Agent.stoppingDistance)) return;
        
        if (!reachedCenter)
        {
            reachedCenter = true;
            Agent.SetDestination(Customer.MoveTargetTrs.position);
            InGameManager.WaitingCustomers[0].Enqueue(Controller);
        }
        else
        {
            Controller.ChangeState<CustomerPickingBreadState>();
        }
    }

    public override void StateExit()
    {
        reachedCenter = false;
    }
}
