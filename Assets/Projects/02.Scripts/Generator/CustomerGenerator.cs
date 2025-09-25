using System;
using System.Collections;
using UnityEngine;

public class CustomerGenerator : MonoBehaviour
{
    private InGameManager InGameManager;
    
    [Header("CustomerGenerator Settings")]
    [SerializeField] private Customer customerPrefab;
    [SerializeField] private int initialSize = 10;
    public GenericPool<Customer> CustomerPool { get; private set; }

    private int count;
    
    private IEnumerator Start()
    {
        InGameManager = InGameManager.Instance;
        
        CustomerPool = new GenericPool<Customer>(customerPrefab, initialSize, transform);

        for (int i = 0; i < 3; i++)
        {
            CustomerPool.Get(transform.position, Quaternion.identity).gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }

    public void Generate()
    {
        CustomerPool.Get(transform.position, Quaternion.identity).gameObject.SetActive(true);
    }
}
