using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCheckingOutState : CustomerStateBase
{
    private float timer;
    private bool checkingOut;

    public override void StateEnter()
    {
        timer = 0f;
        Animator.SetFloat(MOVE, 0);
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
            if (timer >= 0.5f && !checkingOut)
            {
                StartCoroutine(CheckOut());
            }

            return;
        }

        if (timer >= 0.15f)
        {
            PackagingBread();
        }
    }

    private IEnumerator CheckOut()
    {
        checkingOut = true;
        
        Customer.GetIconBubble.Release();
        
        Customer.GetPaperBag = InGameManager.PaperBagGenerator.PaperBag;
        
        Customer.GetPaperBag.CloseAnimationPlay();

        yield return null;
        
        yield return new WaitForSeconds(1f);
        
        Customer.GetPaperBag.SetCurveMovement(Customer.HandTrs, 0f, 0f, 5f, Customer.HandTrs);
        
        InGameManager.MoneyGenerator.Generate(10, InGameManager.POSTable.GetMoneyArea);
        
        InGameManager.CheckingOut = false;
        InGameManager.PaperBagGenerator.PaperBag = null;
        
        Controller.ChangeState<CustomerLeavingState>();
        
        InGameManager.NextStep<CustomerCheckingOutState>(InGameManager.CHECKOUT_INDEX);
        InGameManager.ArrangeWaitingLine(InGameManager.CHECKOUT_INDEX);

        AudioManager.Instance.CheckOutSound();
        
        checkingOut = false;
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