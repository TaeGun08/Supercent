using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEatingState : CustomerStateBase
{
    public override void StateEnter()
    {
        StartCoroutine(PutDownBreadCoroutine());
    }

    private IEnumerator PutDownBreadCoroutine()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.1f);
        
        while (Agent.pathPending || Agent.remainingDistance > Agent.stoppingDistance)
        {
            yield return wfs;
        }
        
        Table table = Customer.SitTable;
        
        while (Customer.BreadStack.Count > 0)
        {
            Bread bread = Customer.GetBread();
            bread.SetCurveMovement(table.PutDownTrs.position, table.transform.position.y , 0f, table.transform, true);
            yield return wfs;
        }
        
        yield return new WaitForSeconds(3f);

        table.FinishEating();
        
        Customer.SitTable = null;
        Controller.ChangeState<CustomerLeavingState>();
    }

    public override void StateExit()
    {
    }
}
