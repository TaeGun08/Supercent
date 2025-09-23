using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CustomerGenerator : MonoBehaviour
{
    [Header("CustomerGenerator Settings")]
    [SerializeField] private Customer customerPrefab;
    [SerializeField] private int initialSize = 10;

    private Queue<Customer> pool = new Queue<Customer>();
    
    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            Customer customer = CreateCustomer();
            pool.Enqueue(customer);
            customer.gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    /// 새로운 손님 생성
    /// </summary>
    private Customer CreateCustomer()
    {
        Customer customer = Instantiate(customerPrefab, transform.position, Quaternion.identity, transform);
        return customer;
    }

    /// <summary>
    /// 손님 꺼내기
    /// </summary>
    public Customer GetCustomer()
    {
        if (pool.Count <= 0) return CreateCustomer();
        
        Customer customer = pool.Dequeue();
        customer.transform.position = transform.position;
        customer.gameObject.SetActive(true);
        return customer;
    }

    /// <summary>
    /// 손님 반환하기
    /// </summary>
    public void ReturnCustomer(Customer customer)
    {
        customer.gameObject.SetActive(false);
        customer.transform.SetParent(transform);
        pool.Enqueue(customer);
    }
}
