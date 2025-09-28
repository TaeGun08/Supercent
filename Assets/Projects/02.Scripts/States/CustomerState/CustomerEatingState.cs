using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CustomerEatingState : CustomerStateBase
{
    Stack<Bread> eatingBread = new Stack<Bread>();
    
    public override void StateEnter()
    {
        Customer.GetIconBubble.Release();
        StartCoroutine(PutDownBreadCoroutine());
    }

    private IEnumerator PutDownBreadCoroutine()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.1f);
        
        while (Agent.pathPending || Agent.remainingDistance > Agent.stoppingDistance)
        {
            yield return wfs;
        }
        
        Quaternion targetRot = Quaternion.LookRotation(-Vector3.forward);
        Controller.transform.DORotate(targetRot.eulerAngles, 0.3f);
        
        Table table = Customer.SitTable;
        float yStep = 0;
        
        
        while (Customer.BreadStack.Count > 0)
        {
            Bread bread = Customer.GetBread();
            bread.SetCurveMovement(table.PutDownTrs.position + new Vector3(0f, yStep, 0f), 2f, 90f, table.transform, true);
            yStep += 0.4f;
            eatingBread.Push(bread);
            yield return wfs;
        }
        
        yield return new WaitForSeconds(3f);

        int count = eatingBread.Count;

        for (int i = 0; i < count; i++)
        {
            Bread bread = eatingBread.Pop();
            bread.gameObject.SetActive(false);
            InGameManager.BreadGenerator.Return(bread);
        }
        
        table.FinishEating();
        yield return null;
        
        Customer.SitTable = null;
        Controller.ChangeState<CustomerLeavingState>();
    }

    public override void StateExit()
    {
    }
}
