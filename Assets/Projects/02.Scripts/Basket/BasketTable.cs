using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasketTable : MonoBehaviour
{
    private Vector3 putDownPos;
    private Stack<Bread> breads = new Stack<Bread>();
    [SerializeField] private Transform plane;
    [SerializeField] private Transform[] targetPos;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Player"))) return;
        plane.localScale = Vector3.Lerp(plane.localScale, Vector3.one + Vector3.one * 0.15f, 1f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Player"))) return;
        plane.localScale = Vector3.Lerp(plane.localScale, Vector3.one, 1f);
    }
    
    public void PickUpBread()
    {
    }

    public void PutDownBread()
    {
    }
}