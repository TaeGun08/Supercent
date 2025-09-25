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
        moveTrs = InGameManager.Instance.BasketTable.GetAvailableSlot();

        Agent.isStopped = false;
        Agent.SetDestination(centerPoint.position);
    }

    public override void OnUpdate()
    {
        if (Agent.pathPending || !(Agent.remainingDistance <= Agent.stoppingDistance)) return;
        
        if (!reachedCenter)
        {
            reachedCenter = true;
            Agent.SetDestination(moveTrs.position);
        }
        else
        {
            Controller.ChangeState<CustomerPickingBreadState>();
        }
    }

    public override void StateExit()
    {
        reachedCenter = false;
        Agent.isStopped = true;
    }
}
