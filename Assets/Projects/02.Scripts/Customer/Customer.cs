using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Customer : BreadHandler
{
    private InGameManager InGameManager;
    
    public NavMeshAgent Agent { get; private set; }
    public CustomerController Controller { get; private set; }

    [Header("Customer Settings")] [SerializeField]
    private int breadMaxCount;
    [SerializeField] private int hasMoney;
    public int HasMoney => hasMoney;
    
    public int PickingBreadCount { get; private set; }
    
    public Transform MoveTargetTrs { get; set; }
    
    public PaperBag GetPaperBag { get; set; }

    public Table SitTable { get; set; }
    public IconBubble GetIconBubble { get; set; }

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Controller = GetComponent<CustomerController>();
    }

    private void Start()
    {
        InGameManager = InGameManager.Instance;
    }

    private void OnEnable()
    {
        PickingBreadCount = Random.Range(1, breadMaxCount + 1);
    }

    public void Reset()
    {
        MoveTargetTrs = null;
        GetPaperBag = null;
        SitTable = null;
    }
}