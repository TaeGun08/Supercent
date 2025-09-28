using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : OnTriggerInteraction, ITutorial
{
    [Space] 
    [Header("Oven Settings")] 
    [SerializeField] private BreadGenerator breadGenerator;

    private bool inside;
    
    public Tutorial Tutorial => Tutorial.Oven;
    public Tutorial NextTutorial => Tutorial.BasketTable;

    protected override void TriggerEnter(Collider other)
    {
        TutorialManager.Instance.SetPoints(Tutorial, NextTutorial);
        
        BreadHandler breadHandler = other.gameObject.GetComponent<BreadHandler>();
        
        if (breadHandler.MaxBread) return;
        
        inside = true;
        
        StartCoroutine(PickupBreadCoroutine(breadHandler));
    }
    
    private IEnumerator PickupBreadCoroutine(BreadHandler breadHandler)
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (breadHandler != null && inside && !breadHandler.MaxBread)
        {
            yield return wait;
            if (breadGenerator.BakeBreads.Count <= 0) continue;
            breadHandler?.PickupBread(breadGenerator.BakeBreads.Dequeue());
        }
    }

    protected override void TriggerExit(Collider other)
    {
        inside = false;
    }
}