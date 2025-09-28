using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPickingBreadState : CustomerStateBase
{
    private float timer;
    
    public override void StateEnter()
    {
        timer = 0;
    }
    
    public override void OnUpdate()
    {
        if (CheckFullStack()) return;
        GetBread();
    }

    private void GetBread()
    {
        if (InGameManager.BasketTable.Breads.Count <= 0 || !InGameManager.CustomerChecker(InGameManager.BREAD_INDEX, Controller))
        {
            Controller.ChangeState<CustomerWaitingState>();
            return;
        }
        
        timer += Time.deltaTime;
        
        if (timer < 0.1f) return;
        timer = 0;
        
        Customer.PickupBread(InGameManager.BasketTable.PickUp());
        Customer.GetIconBubble.UpdateBreadIcon(Customer.PickingBreadCount - Customer.BreadStack.Count);
    }

    private bool CheckFullStack()
    {
        if (Customer.BreadStack.Count < Customer.PickingBreadCount) return false;
        
        InGameManager.BasketTable.ReleaseSlot(Customer.MoveTargetTrs);
        
        Controller.ChangeState<CustomerDecisionState>();
        InGameManager.NextStep<CustomerPickingBreadState>(InGameManager.BREAD_INDEX);
        
        return true;
    }

    public override void StateExit()
    {
    }
}
