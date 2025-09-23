using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using UnityEngine.Serialization;

public class Customer : MonoBehaviour
{
    public NavMeshAgent Agent { get; private set; }
    public CustomerController CustomerController { get; private set; }

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        CustomerController = GetComponent<CustomerController>();
    }
}