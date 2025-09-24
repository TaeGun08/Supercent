using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatus
{
    public float MoveSpeed;
}

public class Player : BreadHandler
{
    [Header("PlayerStatus Settings")]
    [SerializeField] private PlayerStatus playerStatus;
    public PlayerStatus PlayerStatus => playerStatus;
}
