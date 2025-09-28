using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        if (Agent.pathPending || Agent.remainingDistance > Agent.stoppingDistance) return;

        if (!reachedCenter)
        {
            reachedCenter = true;
            Agent.SetDestination(Customer.MoveTargetTrs.position);
        }
        else
        {
            InGameManager.EnqueueCustomer(InGameManager.BREAD_INDEX, Controller);
            
            Customer.GetIconBubble = InGameManager.IconBubbleGenerator.GetGenerate();
            Customer.GetIconBubble.SetTarget(Controller.transform, new Vector3(0, 3f, 0));
            Customer.GetIconBubble.UpdateBreadIcon(Customer.PickingBreadCount);
            
            Quaternion targetRot = Quaternion.LookRotation(Customer.MoveTargetTrs.forward);
            Controller.transform.DORotate(targetRot.eulerAngles, 0.3f);
            Controller.ChangeState<CustomerPickingBreadState>();
        }
    }

    public override void StateExit()
    {
        reachedCenter = false;
    }
}