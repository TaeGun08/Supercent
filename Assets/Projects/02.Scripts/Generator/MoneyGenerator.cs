using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyGenerator : MonoBehaviour
{
    public Stack<Money> Moneys { get; private set; } = new Stack<Money>();

    [Header("MoneyGenerator Settings")] [SerializeField]
    private Vector3 startPos;

    [SerializeField] private Money moneyPrefab;
    [SerializeField] private int initialSize;
    [SerializeField] private float xStep, yStep, zStep;
    public GenericPool<Money> MoneyPool { get; private set; }

    private void Awake()
    {
        MoneyPool = new GenericPool<Money>(moneyPrefab, initialSize, transform);
    }

    private Vector3 GetPutDownPos()
    {
        int count = Moneys.Count;

        int perRow = 5;
        int perLayer = 5 * 3;

        int layer = count / perLayer;
        int rowInLayer = (count % perLayer) / perRow;
        int colInRow = count % perRow;

        Vector3 pos = startPos;
        pos.x += colInRow * xStep;
        pos.z += rowInLayer * zStep;
        pos.y += layer * yStep;

        return pos;
    }

    public void Generate(int generate)
    {
        for (int i = 0; i < generate; i++)
        {
            Moneys.Push(MoneyPool.Get(GetPutDownPos(), Quaternion.identity));
        }
    }
}