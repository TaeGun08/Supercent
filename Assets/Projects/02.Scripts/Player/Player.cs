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
    public int HasMoney { get; set; }

    private void Start()
    {
        InGameManager = InGameManager.Instance;
        InGameManager.BreadMaxUI.SetTarget(this, new Vector3(0f, 5.5f, 0f));
    }

    public Money Pay()
    {
        HasMoney--;
        return InGameManager.MoneyGenerator.GetGenerateMoney(transform);
    }
}
