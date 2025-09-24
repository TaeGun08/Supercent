using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerWaitingBreadState : CustomerStateBase
{
    public override void StateEnter()
    {
        BreadManager.Instance.OnBreadRestocked += OnBreadRestocked;
    }

    private void OnBreadRestocked()
    {
        Controller.ChangeState<CustomerPickingBreadState>();
    }

    public override void StateExit()
    {
        BreadManager.Instance.OnBreadRestocked -= OnBreadRestocked;
    }
}
