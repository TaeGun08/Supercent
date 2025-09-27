using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCheckingOutState : CustomerStateBase
{
    private float timer;

    public override void StateEnter()
    {
        timer = 0f;
    }

    public override void OnUpdate()
    {
        if (InGameManager.CheckingOut == false)
        {
            Controller.ChangeState<CustomerWaitingState>();
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
        Customer.GetPaperBag.SetCurveMovement(Customer.HandTrs, 0f, 0f, 5f, Customer.HandTrs);
        
        InGameManager.MoneyGenerator.Generate(10, InGameManager.POSTable.GetMoneyArea);
        
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