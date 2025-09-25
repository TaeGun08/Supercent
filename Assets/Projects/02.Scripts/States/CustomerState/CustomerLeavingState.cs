using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLeavingState : CustomerStateBase
{
    public override void StateEnter()
    {
        Agent.SetDestination(InGameManager.CustomerGenerator.transform.position);
    }

    public override void StateExit()
    {
    }
}
