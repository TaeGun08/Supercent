using System.Collections;
using System.Collections.Generic;
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
                Agent.SetDestination(pos);
                break;
            case InGameManager.EATING_INDEX:

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