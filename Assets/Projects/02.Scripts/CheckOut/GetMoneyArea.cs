using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMoneyArea : OnTriggerInteraction
{
    private bool inside;
    
    private Stack<Money> moneyStack = new Stack<Money>();
    
    [Header("GetMoneyArea Settings")]
    [SerializeField] private Transform moneyTrs;
    [SerializeField] private float xStep, yStep, zStep;

    protected override void TriggerEnter(Collider other)
    {
        inside = true;
        StartCoroutine(PickupBreadCoroutine(other));
    }
    
    private IEnumerator PickupBreadCoroutine(Collider other)
    {
        WaitForSeconds wait = new WaitForSeconds(0.05f);
        
        Player player = other.GetComponent<Player>();
        
        while (other != null && inside)
        {
            yield return wait;
            
            if (moneyStack.Count <= 0) continue;

            Money money = moneyStack.Pop();
            money.SetCurveMovement(other.transform, 0f, 0f, 5f, transform);
            player.HasMoney++;
        }
    }

    protected override void TriggerExit(Collider other)
    {
        inside = false;
    }

    private Vector3 GetPutDownPos()
    {
        int count = moneyStack.Count;

        int perRow = 3;
        int perLayer = 3 * 3;

        int layer = count / perLayer;
        int rowInLayer = (count % perLayer) / perRow;
        int colInRow = count % perRow;

        Vector3 pos = moneyTrs.position;
        pos.x += colInRow * xStep;
        pos.z += rowInLayer * zStep;
        pos.y += layer * yStep;

        return pos;
    }
    
    public void PushMoney(Money money)
    {
        money.transform.position = GetPutDownPos();
        moneyStack.Push(money);
    }
}
