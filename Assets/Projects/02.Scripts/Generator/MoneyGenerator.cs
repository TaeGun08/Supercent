using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGenerator : MonoBehaviour
{
    [Header("MoneyGenerator Settings")] 
    [SerializeField] private Money moneyPrefab;
    [SerializeField] private int initialSize;
    private GenericPool<Money> moneyPool;

    private void Awake()
    {
        moneyPool = new GenericPool<Money>(moneyPrefab, initialSize, transform);
    }

    public void Generate(int generate, GetMoneyArea getMoneyArea)
    {
        for (int i = 0; i < generate; i++)
        {
            Money money = moneyPool.Get(transform.position, Quaternion.Euler(0f, 90f, 0f));
            money.gameObject.SetActive(true);
            getMoneyArea.PushMoney(money);
        }
    }

    public Money GetGenerateMoney(Transform target)
    {
        return moneyPool.Get(target.position, Quaternion.identity);
    }

    public void Return(Money money)
    {
        moneyPool.Return(money);
    }
}