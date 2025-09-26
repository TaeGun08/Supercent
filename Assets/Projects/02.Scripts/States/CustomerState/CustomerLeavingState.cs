using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLeavingState : CustomerStateBase
{
    public override void StateEnter()
    {
        InGameManager.MaxCustomerCheckAndGenerate();
        //Agent.SetDestination(InGameManager.CustomerGenerator.transform.position);
    }
    
    public override void OnUpdate()
    {
        if (Agent.pathPending || !(Agent.remainingDistance <= Agent.stoppingDistance)) return;
        Customer.gameObject.SetActive(false);
        Customer.GetPaperBag.gameObject.SetActive(false);
        InGameManager.CustomerGenerator.CustomerPool.Return(Customer);
    }

    public override void StateExit()
    {
    }
}
