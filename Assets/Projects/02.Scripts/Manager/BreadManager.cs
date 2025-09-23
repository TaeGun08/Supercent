using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadManager : SingletonBehaviour<BreadManager>
{
    public Action BreadEvent { get; set; }

    private void Update()
    {
        BreadEvent?.Invoke();
    }
}
