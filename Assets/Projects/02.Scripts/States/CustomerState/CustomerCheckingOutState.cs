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
                CheckOut();
            }

            return;
        }

        if (timer >= 0.15f)
        {
            PackagingBread();
        }
    }

    private void CheckOut()
    {
        Customer.GetPaperBag = InGameManager.PaperBagGenerator.PaperBag;
        Customer.GetPaperBag.SetCurveMovement(Customer.HandTrs.position, 2f, 0f, Customer.HandTrs, true);
        
        InGameManager.MoneyGenerator.Generate(Customer.HasMoney);
        
        InGameManager.CheckingOut = false;
        InGameManager.PaperBagGenerator.PaperBag = null;
        
        Controller.ChangeState<CustomerLeavingState>();
        
        InGameManager.NextStep<CustomerCheckingOutState>(InGameManager.CHECKOUT_INDEX);
        InGameManager.ArrangeWaitingLine(InGameManager.CHECKOUT_INDEX);
    }

    private void PackagingBread()
    {
        Bread bread = Customer.GetBread();
        bread.SetCurveMovement(InGameManager.PaperBagGenerator.PaperBagTrs.position, 2.5f, 0f,
            Customer.HandTrs, false);
        timer = 0f;
    }
    
    public override void StateExit()
    {
    }
}