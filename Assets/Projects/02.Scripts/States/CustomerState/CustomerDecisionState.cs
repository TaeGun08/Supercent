using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDecisionState : CustomerStateBase
{
    private int decision;
    
    public override void StateEnter()
    {
        Decision();
    }

    public override void OnUpdate()
    {
        if (Agent.pathPending || (Agent.remainingDistance <= Agent.stoppingDistance) == false) return;
        Controller.ChangeState<CustomerWaitingState>();
    }

    private void Decision()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                decision = InGameManager.CHECKOUT_INDEX;
                Going(decision);
                break; 
            case 1:
                decision = !InGameManager.MaxEatingWaiting() ? InGameManager.CHECKOUT_INDEX : InGameManager.EATING_INDEX;
                if (decision == InGameManager.EATING_INDEX)
                {
                    InGameManager.EatingHole.SetCustomerEmptyTable(Customer);
                }
                Going(decision);
                break;
        }
    }

    private void Going(int decision)
    {
        int count = InGameManager.GetQueueCount(decision);
        count = Mathf.Max(count, 0);
        Vector3 pos = InGameManager.POSTable.GetPos(decision - 1, count);

        if (decision == InGameManager.EATING_INDEX && Customer.SitTable != null)
            pos = InGameManager.EatingHole.gameObject.activeSelf ? Customer.SitTable.SitTrs.position : pos;
        
        Agent.SetDestination(pos);
        InGameManager.EnqueueCustomer(decision, Controller);
    }
    
    public override void StateExit()
    {
    }
}
