using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEnteringState : CustomerStateBase
{
    public override void StateEnter()
    {
        StartCoroutine(MoveCheckCoroutine());
    }

    private IEnumerator MoveCheckCoroutine()
    {
        Agent.SetDestination(InGameManager.Instance.transform.position);
        
        WaitForSeconds wait = new WaitForSeconds(1f);
        while (Agent.pathPending)
        {
            yield return wait;
        }
        
        Controller.ChangeState<CustomerSelectingBreadState>();
    }
    
    public override void StateExit()
    {
    }
}
