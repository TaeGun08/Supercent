using System;
using System.Collections;
using UnityEngine;

public class CustomerGenerator : MonoBehaviour
{
    private InGameManager InGameManager;

    [Header("CustomerGenerator Settings")] [SerializeField]
    private Customer customerPrefab;

    [SerializeField] private int initialSize = 10;
    private GenericPool<Customer> customerPool;

    private int count;

    private void Start()
    {
        InGameManager = InGameManager.Instance;

        customerPool = new GenericPool<Customer>(customerPrefab, initialSize, transform);

        StartCoroutine(GenerateCoroutine());
    }

    private IEnumerator GenerateCoroutine()
    {
        WaitForSeconds wfs = new WaitForSeconds(0.5f);

        while (gameObject.activeSelf)
        {
            yield return wfs;
            
            if (!InGameManager.MaxBasketSlotAndMaxCheckOutWaiting()) continue;

            customerPool.Get(transform.position, Quaternion.identity).gameObject.SetActive(true);
        }
    }

    public void Return(Customer customer)
    {
        customerPool.Return(customer);
    }
}