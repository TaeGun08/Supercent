using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEnteringState : CustomerStateBase
{
    private Transform centerPoint; 
    private Transform breadTablePoint; 

    private bool reachedCenter;

    public override void StateEnter()
    {
        centerPoint = InGameManager.Instance.transform;
        breadTablePoint = BreadManager.Instance.BasketTable.GetAvailableSlot();

        Agent.isStopped = false;
        Agent.SetDestination(centerPoint.position);
    }

    public override void OnUpdate()
    {
        if (!Agent.pathPending && Agent.remainingDistance <= Agent.stoppingDistance)
        {
            if (!reachedCenter)
            {
                reachedCenter = true;
                Agent.SetDestination(breadTablePoint.position);
            }
            else
            {
                Controller.ChangeState<CustomerPickingBreadState>();
            }
        }
    }

    public override void StateExit()
    {
        breadTablePoint = null;
        reachedCenter = false;
        Agent.isStopped = true;
    }
}
