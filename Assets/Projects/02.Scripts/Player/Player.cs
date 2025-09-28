using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatus
{
    public float MoveSpeed;
    public float Gravity;
}

public class Player : BreadHandler
{
    private InGameManager InGameManager;
    
    [Header("PlayerStatus Settings")]
    [SerializeField] private PlayerStatus playerStatus;
    public PlayerStatus PlayerStatus => playerStatus;

    public int HasMoney { get; set; } = 99999;

    private void Start()
    {
        InGameManager = InGameManager.Instance;
    }

    public Money Pay()
    {
        HasMoney--;
        return InGameManager.MoneyGenerator.GetGenerateMoney(transform);
    }
}
