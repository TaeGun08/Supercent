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
    [Header("PlayerStatus Settings")]
    [SerializeField] private PlayerStatus playerStatus;
    public PlayerStatus PlayerStatus => playerStatus;
    private int hasMoney;

    protected override void Start()
    {
        base.Start();
        InGameManager.BreadMaxUI.SetTarget(this, new Vector3(0f, 5.5f, 0f));
    }

    public void SetHasMoney()
    {
        hasMoney++;
        AudioManager.MoneySound();
        InGameManager.UpdateHasMoneyText(hasMoney);
    }

    public int GetHasMoney()
    {
        return hasMoney;
    }

    public Money Pay()
    {
        hasMoney--;
        InGameManager.UpdateHasMoneyText(hasMoney);
        return InGameManager.MoneyGenerator.GetGenerateMoney(transform);
    }
}
