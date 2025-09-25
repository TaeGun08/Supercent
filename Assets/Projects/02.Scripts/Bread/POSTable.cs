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
            CustomerController customer = InGameManager.FirstWaitingCustomer(1);
            
            if (customer != null && customer.CurrentState is CustomerWaitingCheckoutState)
            {
                InGameManager.PaperBagGenerator.Generate();
                InGameManager.CheckingOut = true;
                customer.ChangeState<CustomerCheckingOutState>();
            }
        }
    }

    protected override void TriggerExit(Collider other)
    {
        inside = false;
        InGameManager.CheckingOut = false;
    }
    
    public Vector3 GetPos(int posIndex, int index)
    {
        Vector3 pos = startPos[posIndex];
        pos.z += index * zStep;
        return pos;
    }
}
