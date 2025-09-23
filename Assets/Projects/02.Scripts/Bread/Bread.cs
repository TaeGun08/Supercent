using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : MonoBehaviour
{
    private Vector3 targetPos;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnDisable()
    {
        RemoveEvent();
    }

    public void SetEvent(Vector3 pos)
    {
        BreadManager.Instance.BreadEvent += BreadMovement;
        targetPos = pos;
    }

    private void RemoveEvent()
    {
        BreadManager.Instance.BreadEvent -= BreadMovement;
        targetPos = Vector3.zero;
    }
    
    private void BreadMovement()
    {
        float distance = Vector3.Distance(transform.position, targetPos);
        transform.position = Vector3.Slerp(transform.position, targetPos, Time.deltaTime * 10f);
        if (distance > 0.1f) return;
        RemoveEvent();
    }

    public void BakeBread()
    {
        rigidbody.AddForce(-Vector3.forward * 5f, ForceMode.Impulse);
    }
}
