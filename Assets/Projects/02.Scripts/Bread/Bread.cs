using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : MonoBehaviour
{
    private Vector3 targetPos;
    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    
    private void OnDisable()
    {
        RemoveEvent();
    }

    public void SetEvent(Vector3 pos)
    {
        targetPos = pos;
        BreadManager.Instance.BreadEvent += BreadMovement;
    }

    private void RemoveEvent()
    {
        targetPos = Vector3.zero;
        BreadManager.Instance.BreadEvent -= BreadMovement;
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
        rigid.AddForce(-Vector3.forward * 4f, ForceMode.Impulse);
    }
}
