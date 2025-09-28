using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketTable : OnTriggerInteraction, ITutorial
{
    private AudioManager AudioManager;
    
    public Stack<Bread> Breads { get; private set; } = new Stack<Bread>();

    [Space] [Header("BasketTable Settings")] 
    [SerializeField] private Transform breadTargetPos;
    [SerializeField] private float xStep = 0.5f;
    [SerializeField] private float zStep = -1f;
    [SerializeField] private float yStep = 0.5f;
    [Space] [SerializeField] private Transform[] targetPos;
    [SerializeField] private int maxBread;

    private bool[] slotOccupied;

    private bool inside = true;
    
    public Tutorial Tutorial => Tutorial.BasketTable;
    public Tutorial NextTutorial => Tutorial.POSTable;

    private void Awake()
    {
        slotOccupied = new bool[targetPos.Length];
    }

    private void Start()
    {
        AudioManager = AudioManager.Instance;
    }

    protected override void TriggerEnter(Collider other)
    {
        TutorialManager.Instance.SetPoints(Tutorial, NextTutorial);
        
        if (Breads.Count >= maxBread) return;

        inside = true;
        BreadHandler breadHandler = other.gameObject.GetComponent<BreadHandler>();
        StartCoroutine(PickupBreadCoroutine(breadHandler));
    }

    private IEnumerator PickupBreadCoroutine(BreadHandler breadHandler)
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (breadHandler != null && inside && !(Breads.Count >= maxBread))
        {
            yield return wait;
            if (breadHandler.BreadStack.Count <= 0) continue;
            AudioManager.BreadPutDownSound();
            PutDown(breadHandler.GetBread());
        }
    }

    protected override void TriggerExit(Collider other)
    {
        inside = false;

        if (Breads.Count <= 0) return;
        
        var customer = InGameManager.Instance.PeekCustomer(InGameManager.BREAD_INDEX);
        if (customer != null && customer.CurrentState is CustomerWaitingState)
        {
            customer.ChangeState<CustomerPickingBreadState>();
        }
    }

    public Transform GetAvailableSlot()
    {
        for (int i = 0; i < slotOccupied.Length; i++)
        {
            if (!slotOccupied[i])
            {
                slotOccupied[i] = true;
                return targetPos[i];
            }
        }

        return null;
    }

    public void ReleaseSlot(Transform slot)
    {
        for (int i = 0; i < targetPos.Length; i++)
        {
            if (targetPos[i] == slot)
            {
                slotOccupied[i] = false;
                break;
            }
        }
    }

    public bool SlotCheck()
    {
        foreach (var slot in slotOccupied)
        {
            if (!slot) return true;
        }
        
        return false;
    }

    private Vector3 GetPutDownPos()
    {
        int count = Breads.Count;

        int perRow = 2;
        int perLayer = 8;

        int layer = count / perLayer;
        int rowInLayer = (count % perLayer) / perRow;
        int colInRow = count % perRow;

        Vector3 pos = breadTargetPos.position;
        pos.x += colInRow * xStep;
        pos.z += rowInLayer * zStep;
        pos.y += layer * yStep;

        return pos;
    }
    
    private void PutDown(Bread bread)
    {
        Vector3 pos = GetPutDownPos();

        bread.SetCurveMovement(pos, pos.y + 1f, -35f, transform, true);

        Breads.Push(bread);
    }

    public Bread PickUp()
    {
        if (Breads.Count == 0) return null;
        return Breads.Pop();
    }
}