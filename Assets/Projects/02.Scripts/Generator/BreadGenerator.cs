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
    
    private Queue<Bread> bakeBreads = new Queue<Bread>();
    public GenericPool<Bread> BreadPool { get; private set; }
    
    private void Awake()
    {
        BreadPool = new GenericPool<Bread>(breadPrefab, initialSize, transform);

        StartCoroutine(BakeBreadCoroutine());
    }

    private IEnumerator BakeBreadCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(2f);
        
        while (true)
        {
            yield return wait;
            
            if (bakeBreads.Count >= maxBake) continue;
            Bread bread = BreadPool.Get(bakeTransform.position, Quaternion.identity);
            bread.BakeBread();
            bakeBreads.Enqueue(bread);
        }
    }
}
