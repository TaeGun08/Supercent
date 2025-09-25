using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPickingBreadState : CustomerStateBase
{
    private float timer;
    
    public override void StateEnter()
    {
    }
    
    public override void OnUpdate()
    {
        if (Customer.BreadStack.Count >= Customer.PickingBreadCount)
        {
            InGameManager.BasketTable.ReleaseSlot(Customer.MoveTargetTrs);
            InGameManager.GoCheckOutOrEatingCustomer(0);
            Controller.ChangeState<CustomerDecisionState>();
            return;
        }
        
        if (InGameManager.BasketTable.Breads.Count <= 0)
        {
            Controller.ChangeState<CustomerWaitingBreadState>();
            return;
        }
        
        timer += Time.deltaTime;
        
        if (timer < 0.1f) return;
        timer = 0;
        
        Customer.PickupBread(InGameManager.BasketTable.PickUp());
    }

    public override void StateExit()
    {
        timer = 0;
    }
}
