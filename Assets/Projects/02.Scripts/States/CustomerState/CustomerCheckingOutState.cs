using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCheckingOutState : CustomerStateBase
{
    private float timer = 0f;
    
    public override void StateEnter()
    {
        timer = 0f;
    }

    public override void OnUpdate()
    {
        if (InGameManager.CheckingOut == false)
        {
            Controller.ChangeState<CustomerWaitingCheckoutState>();
            return;
        }
        
        timer += Time.deltaTime;
        
        if (Customer.BreadStack.Count <= 0)
        {
            if (timer >= 1f)
            {
                InGameManager.PaperBagGenerator.PaperBag.SetCurveMovement(Customer.HandTrs, 0f, 0f, Customer.HandTrs);
                Controller.ChangeState<CustomerLeavingState>();
            }
            
            return;
        }
        
        if (timer >= 0.15f)
        {
            Customer.GetBread().SetCurveMovement(InGameManager.PaperBagGenerator.PaperBagTrs.position, 3f, 0f, Customer.HandTrs, false);
            timer = 0f;
        }
    }

    public override void StateExit()
    {
    }
}
