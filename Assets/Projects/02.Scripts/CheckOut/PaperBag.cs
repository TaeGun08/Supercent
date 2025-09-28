using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBag : DOCurveMovement
{
    public void Return()
    {
        gameObject.SetActive(false);
        InGameManager.Instance.PaperBagGenerator.Return(this);
    }
}
