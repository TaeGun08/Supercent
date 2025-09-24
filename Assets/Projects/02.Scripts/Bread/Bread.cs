using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bread : MonoBehaviour
{
    private Transform targetTrs;
    private float curYStep;
    
    private Rigidbody rigid;
    private Collider collider;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }
    
    private void OnDisable()
    {
        rigid.isKinematic = false;
        collider.isTrigger = false;
        Remove();
    }

    public void SetBread(Transform targetTrs, float yStep)
    {
        rigid.isKinematic = true;
        collider.isTrigger = true;

        this.targetTrs = targetTrs;
        curYStep = yStep;
        
        transform.SetParent(targetTrs);
        
        BreadManager.Instance.BreadEvent += BreadEvent;
    }

    private void Remove()
    {
        targetTrs = null;
        curYStep = 0f;
        BreadManager.Instance.BreadEvent -= BreadEvent;
    }

    private void BreadEvent()
    {
        Vector3 targetPosition = targetTrs.position + new Vector3(0f, curYStep, 0f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
    
        Quaternion targetRotation = targetTrs.rotation * Quaternion.Euler(0, 90f, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f
            && Quaternion.Angle(transform.rotation, targetRotation) < 0.5f)
        {
            Remove();
        }
    }

    public void BakeBread()
    {
        rigid.AddForce(-Vector3.forward * 4f, ForceMode.Impulse);
    }
}
