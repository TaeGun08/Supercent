using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGenerator : SingletonBehaviour<AIGenerator>
{
    [Header("AIGenerator Settings")]
    [SerializeField] private AI aiPrefab;
    [SerializeField] private int initialSize = 10;

    private Queue<AI> pool = new Queue<AI>();
    
    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            AI ai = CreateAI();
            pool.Enqueue(ai);
            ai.gameObject.SetActive(false);
        }
    }

    
    /// <summary>
    /// 새로운 AI 생성
    /// </summary>
    private AI CreateAI()
    {
        AI ai = Instantiate(aiPrefab, transform.position, Quaternion.identity, transform);
        return ai;
    }

    /// <summary>
    /// AI 꺼내기
    /// </summary>
    public AI GetAI()
    {
        if (pool.Count <= 0) return CreateAI();
        
        AI ai = pool.Dequeue();
        ai.gameObject.SetActive(true);
        return ai;
    }

    /// <summary>
    /// AI 반환하기
    /// </summary>
    public void ReturnAI(AI ai)
    {
        ai.gameObject.SetActive(false);
        ai.transform.SetParent(transform);
        pool.Enqueue(ai);
    }
}
