using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CustomerDecisionState : CustomerStateBase
{
    private int decision;

    public override void StateEnter()
    {
        decision = 0;
        Decision();
    }

    public override void OnUpdate()
    {
        if (Agent.pathPending || Agent.remainingDistance > Agent.stoppingDistance) return;

        Quaternion targetRot = Quaternion.LookRotation(-Vector3.forward);
        Controller.transform.DORotate(targetRot.eulerAngles, 0.3f);
        Controller.ChangeState<CustomerWaitingState>();
    }

    private void Decision()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                decision = InGameManager.CHECKOUT_INDEX;
                break;
            case 1:
                decision = !InGameManager.MaxEatingWaiting()
                    ? InGameManager.CHECKOUT_INDEX
                    : InGameManager.EATING_INDEX;
                break;
        }

        Going();
    }

    private void Going()
    {
        int count = InGameManager.GetQueueCount(decision);
        count = Mathf.Max(count, 0);
        Vector3 pos = InGameManager.POSTable.GetPos(decision - 1, count);

        InGameManager.EnqueueCustomer(decision, Controller);

        switch (decision)
        {
            case InGameManager.CHECKOUT_INDEX:
                Customer.GetIconBubble.UpdatePOSIcon();
                Agent.SetDestination(pos);
                break;
            case InGameManager.EATING_INDEX:
                Customer.GetIconBubble.UpdateEatIcon();
                
                InGameManager.EatingHole.SetCustomerEmptyTable(Customer);

                Agent.SetDestination(pos);

                if (Customer.SitTable == null || !Customer.SitTable.IsSeatAvailable()) break;
                Controller.ChangeState<CustomerGoingToEatState>();
                break;
        }
    }

    public override void StateExit()
    {
    }
}