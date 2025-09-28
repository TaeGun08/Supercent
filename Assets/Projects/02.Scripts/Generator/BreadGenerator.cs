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
    [SerializeField] private float bakeDuration;

    public Queue<Bread> BakeBreads { get; private set; } = new Queue<Bread>();
    private GenericPool<Bread> breadPool;
    
    private void Awake()
    {
        breadPool = new GenericPool<Bread>(breadPrefab, initialSize, transform);

        StartCoroutine(BakeBreadCoroutine());
    }

    private IEnumerator BakeBreadCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(bakeDuration);
        
        while (true)
        {
            yield return wait;
            
            if (BakeBreads.Count >= maxBake) continue;
            Bread bread = breadPool.Get(bakeTransform.position, Quaternion.identity);
            bread.BakeBread();
            BakeBreads.Enqueue(bread);
        }
    }

    public void Return(Bread bread)
    {
        breadPool.Return(bread);
    }
}
