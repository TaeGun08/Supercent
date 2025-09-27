using System;
using System.Collections;
using UnityEngine;

public class CustomerGenerator : MonoBehaviour
{
    private InGameManager InGameManager;

    [Header("CustomerGenerator Settings")] [SerializeField]
    private Customer customerPrefab;

    [SerializeField] private int initialSize = 10;
    public GenericPool<Customer> CustomerPool { get; private set; }

    private int count;

    private void Start()
    {
        InGameManager = InGameManager.Instance;

        CustomerPool = new GenericPool<Customer>(customerPrefab, initialSize, transform);

        StartCoroutine(GenerateCoroutine());
    }

    private IEnumerator GenerateCoroutine()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.5f);

        while (gameObject.activeSelf)
        {
            yield return wfs;
            
            if (!InGameManager.CustomerChecker()) continue;

            CustomerPool.Get(transform.position, Quaternion.identity).gameObject.SetActive(true);
        }
    }
}