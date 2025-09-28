using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerGoingToEatState : CustomerStateBase
{
    public override void StateEnter()
    {
        Agent.SetDestination(Customer.SitTable.SitTrs.position);
        InGameManager.Instance.NextStep<CustomerWaitingState>(InGameManager.EATING_INDEX);
        InGameManager.Instance.ArrangeWaitingLine(InGameManager.EATING_INDEX);
    }

    public override void OnUpdate()
    {
        if (Agent.pathPending || Agent.remainingDistance > Agent.stoppingDistance) return;
        Animator.SetFloat(MOVE, 0);
        Controller.ChangeState<CustomerEatingState>();
    }
    
    public override void StateExit()
    {
    }
}
