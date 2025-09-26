using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : DOCurveMovement
{
    protected override void ResetTransform()
    {
        gameObject.SetActive(false);
        InGameManager.Instance.MoneyGenerator.MoneyPool.Return(this);
    }
}
