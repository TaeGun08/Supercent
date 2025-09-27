using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Customer : BreadHandler
{
    public NavMeshAgent Agent { get; private set; }
    public CustomerController CustomerController { get; private set; }

    [Header("Customer Settings")] [SerializeField]
    private int breadMaxCount;
    [SerializeField] private int hasMoney;
    public int HasMoney => hasMoney;
    
    public int PickingBreadCount { get; private set; }
    
    public Transform MoveTargetTrs { get; set; }
    
    public PaperBag GetPaperBag { get; set; }
    
    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        CustomerController = GetComponent<CustomerController>();
    }

    private void OnEnable()
    {
        PickingBreadCount = Random.Range(1, breadMaxCount + 1);
    }

    public void Reset()
    {
        MoveTargetTrs = null;
        GetPaperBag = null;
        
    }
}