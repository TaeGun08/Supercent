using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Customer : BreadHandler
{
    public NavMeshAgent Agent { get; private set; }
    public CustomerController CustomerController { get; private set; }

    [Header("Customer Settings")] [SerializeField]
    private int breadMaxCount;
    public int PickingBreadCount { get; private set; }
    
    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        CustomerController = GetComponent<CustomerController>();
    }

    private void OnEnable()
    {
        PickingBreadCount = Random.Range(1, breadMaxCount + 1);
    }
}