using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POSTable : OnTriggerInteraction
{
    private InGameManager InGameManager;

    [SerializeField] private Vector3[] startPos;
    [SerializeField] private float zStep;

    private bool inside = true;

    private void Start()
    {
        InGameManager = InGameManager.Instance;
    }

    protected override void TriggerEnter(Collider other)
    {
        inside = true;
        StartCoroutine(CheckOutCoroutine());
    }

    private IEnumerator CheckOutCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (inside)
        {
            yield return wait;

            CustomerController customer = InGameManager.PeekCustomer(InGameManager.CHECKOUT_INDEX);

            if(customer == null) continue;
            if(customer.CurrentState is CustomerWaitingCheckoutState == false) continue;
            if(InGameManager.CheckingOut) continue;
              
            
            InGameManager.CheckingOut = true;
                
            yield return new WaitForSeconds(0.25f);
            InGameManager.PaperBagGenerator.Generate();
                
            yield return new WaitForSeconds(0.5f);
            customer.ChangeState<CustomerCheckingOutState>();
        }

        InGameManager.CheckingOut = false;
    }

    protected override void TriggerExit(Collider other)
    {
        inside = false;
    }

    /// <summary>
    /// 0번은 계산, 1번은 매장 식사
    /// </summary>
    /// <param name="posIndex"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public Vector3 GetPos(int posIndex, int index)
    {
        Vector3 pos = startPos[posIndex];
        pos.z += index * zStep;
        return pos;
    }
}