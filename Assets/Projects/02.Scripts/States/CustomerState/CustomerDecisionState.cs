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
                Going();
                break; 
            case 1:
                decision = !InGameManager.MaxEatingWaiting() ? InGameManager.CHECKOUT_INDEX : InGameManager.EATING_INDEX;
                Going();
                break;
        }
    }

    private void Going()
    {
        int count = InGameManager.GetQueueCount(decision);
        count = Mathf.Max(count, 0);
        Vector3 pos = InGameManager.POSTable.GetPos(decision - 1, count);
        
        switch (decision)
        {
            case InGameManager.CHECKOUT_INDEX:
                Agent.SetDestination(pos);
                break;
            case InGameManager.EATING_INDEX:
                
                InGameManager.EatingHole.SetCustomerEmptyTable(Customer);
                
                if (Customer.SitTable != null 
                    && !Customer.SitTable.IsDirty
                    && InGameManager.EatingHole.gameObject.activeSelf)
                {
                    Customer.SitTable.OnTableCustomerMove();
                }
                else
                {
                    Agent.SetDestination(pos);
                }
                
                break;
        }
        
        InGameManager.EnqueueCustomer(decision, Controller);

        if (decision == InGameManager.EATING_INDEX)
        {
            Debug.Log(InGameManager.GetQueueCount(InGameManager.EATING_INDEX));
        }
    }
    
    public override void StateExit()
    {
    }
}
