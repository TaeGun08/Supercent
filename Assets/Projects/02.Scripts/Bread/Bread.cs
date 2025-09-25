using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bread : DOCurveMovement
{
    private void OnDisable()
    {
        rigid.isKinematic = false;
        coll.isTrigger = false;
    }
    
    public void BakeBread()
    {
        rigid.AddForce(-Vector3.forward * 4f, ForceMode.Impulse);
    }
}