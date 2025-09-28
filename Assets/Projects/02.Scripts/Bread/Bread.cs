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
    
    protected override void ResetVector()
    {
        base.ResetVector();
        
        if (gameObject.activeSelf) return;
        InGameManager.Instance.BreadGenerator.Return(this);
    }

    
    public void BakeBread()
    {
        rigid.AddForce(-Vector3.forward * 7f, ForceMode.Impulse);
    }
}