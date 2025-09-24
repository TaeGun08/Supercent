using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CustomerGenerator : MonoBehaviour
{
    [Header("CustomerGenerator Settings")]
    [SerializeField] private Customer customerPrefab;
    [SerializeField] private int initialSize = 10;
    public GenericPool<Customer> CustomerPool { get; private set; }

    private IEnumerator Start()
    {
        CustomerPool = new GenericPool<Customer>(customerPrefab, initialSize, transform);

        for (int i = 0; i < 2; i++)
        {
            CustomerPool.Get(transform.position, Quaternion.identity).gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }
}
