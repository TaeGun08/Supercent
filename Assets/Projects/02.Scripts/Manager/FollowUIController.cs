using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUIController : MonoBehaviour
{
    public static Action FollowEvent { get; set; }

    private void LateUpdate()
    {
        FollowEvent?.Invoke();
    }
}
