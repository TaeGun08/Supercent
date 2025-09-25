using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Bread : DOCurveMovement
{
    public void BakeBread()
    {
        rigid.AddForce(-Vector3.forward * 4f, ForceMode.Impulse);
    }
}