using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class OnTriggerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] protected Transform plane;
    [SerializeField] protected float planeScaleSize;
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Player"))) return;
        plane?.DOScale(Vector3.one + Vector3.one * planeScaleSize, 0.1f);
        TriggerEnter(other);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Player"))) return;
        plane?.DOScale(Vector3.one, 0.1f);
        TriggerExit(other);
    }

    protected abstract void TriggerEnter(Collider other);
    protected abstract void TriggerExit(Collider other);
}
