using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BreadGenerator : MonoBehaviour
{
    [Header("BreadGenerator Settings")]
    [SerializeField] private Bread breadPrefab;
    [SerializeField] private Transform bakeTransform;
    [SerializeField] private int initialSize = 10;
    [SerializeField] private int maxBake = 10;
    
    private Queue<Bread> pool = new Queue<Bread>();
    private Queue<Bread> bakeBreads = new Queue<Bread>();
    
    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            Bread bread = CreateBread();
            pool.Enqueue(bread);
            bread.gameObject.SetActive(false);
        }

        StartCoroutine(BakeBreadCoroutine());
    }

    private IEnumerator BakeBreadCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(2f);
        
        while (true)
        {
            yield return wait;
            
            if (bakeBreads.Count >= maxBake) continue;
            bakeBreads.Enqueue(GetBread());
        }
    }
    
    /// <summary>
    /// 货肺款 户 积己
    /// </summary>
    private Bread CreateBread()
    {
        Bread bread = Instantiate(breadPrefab, bakeTransform.position, Quaternion.identity, transform);
        bread.BakeBread();
        return bread;
    }

    /// <summary>
    /// 户 波郴扁
    /// </summary>
    private Bread GetBread()
    {
        if (pool.Count <= 0) return CreateBread();
        
        Bread bread = pool.Dequeue();
        bread.transform.position = bakeTransform.position;
        bread.gameObject.SetActive(true);
        bread.BakeBread();
        return bread;
    }

    /// <summary>
    /// 户 馆券窍扁
    /// </summary>
    public void ReturnBread(Bread bread)
    {
        bread.gameObject.SetActive(false);
        bread.transform.SetParent(transform);
        pool.Enqueue(bread);
    }
}
