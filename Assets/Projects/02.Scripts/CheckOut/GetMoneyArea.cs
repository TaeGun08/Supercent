using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMoneyArea : OnTriggerInteraction
{
    private InGameManager InGameManager;
    
    private bool inside;

    private void Start()
    {
        InGameManager = InGameManager.Instance;
    }

    protected override void TriggerEnter(Collider other)
    {
        inside = true;
        StartCoroutine(PickupBreadCoroutine(other));
    }
    
    private IEnumerator PickupBreadCoroutine(Collider other)
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        
        Player player = other.GetComponent<Player>();
        
        while (other != null && inside)
        {
            yield return wait;
            
            if (InGameManager.MoneyGenerator.Moneys.Count <= 0) continue;

            Money money = InGameManager.MoneyGenerator.Moneys.Pop();
            money.SetCurveMovement(other.transform, other.transform.position.y + 0.5f, 0f, 2f, transform);
            player.HasMoney++;
        }
    }

    protected override void TriggerExit(Collider other)
    {
        inside = false;
    }
}
