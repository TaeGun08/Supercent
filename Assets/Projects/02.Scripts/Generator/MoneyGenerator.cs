using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGenerator : MonoBehaviour
{
    [Header("MoneyGenerator Settings")] 
    [SerializeField] private Money moneyPrefab;
    [SerializeField] private int initialSize;
    public GenericPool<Money> MoneyPool { get; private set; }

    private void Awake()
    {
        MoneyPool = new GenericPool<Money>(moneyPrefab, initialSize, transform);
    }

    public void Generate(int generate, GetMoneyArea getMoneyArea)
    {
        for (int i = 0; i < generate; i++)
        {
            getMoneyArea.PushMoney(MoneyPool.Get(transform.position, Quaternion.identity));
        }
    }
}