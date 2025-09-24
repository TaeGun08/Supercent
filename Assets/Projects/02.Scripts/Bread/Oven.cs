using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : OnTriggerInteraction
{
    [Space] [Header("Oven Settings")] [SerializeField]
    private BreadGenerator breadGenerator;

    private bool inside;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Player"))) return;
        inside = true;

        StartCoroutine(PickupBreadCoroutine(other.gameObject.GetComponent<BreadHandler>()));
    }

    private IEnumerator PickupBreadCoroutine(BreadHandler breadHandler)
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (breadHandler != null && inside)
        {
            yield return wait;
            if (breadGenerator.BakeBreads.Count <= 0) continue;
            breadHandler?.PickupBread(breadGenerator.BakeBreads.Dequeue());
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        
        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Player"))) return;
        inside = false;
    }
}