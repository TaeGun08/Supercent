using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketTable : OnTriggerInteraction
{
    public Stack<Bread> Breads { get; private set; } = new Stack<Bread>();

    [Space] [Header("BasketTable Settings")] [SerializeField]
    private Vector3 startPos;

    [SerializeField] private float xStep = 0.5f;
    [SerializeField] private float zStep = -1f;
    [SerializeField] private float yStep = 0.5f;
    [Space] [SerializeField] private Transform[] targetPos;
    [SerializeField] private int maxBread;

    private bool[] slotOccupied;

    private bool inside = true;

    private void Awake()
    {
        slotOccupied = new bool[targetPos.Length];
    }

    protected override void TriggerEnter(Collider other)
    {
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
            PutDown(breadHandler.GetBread());
        }
    }

    protected override void TriggerExit(Collider other)
    {
        inside = false;

        if (Breads.Count <= 0) return;

        CustomerController customer = InGameManager.Instance.FirstWaitingCustomer(0);
        
        if (customer != null && customer.CurrentState is CustomerWaitingBreadState)
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
                InGameManager.Instance.MaxCustomerCheckAndGenerate();
                break;
            }
        }
    }

    public bool SlotCheck()
    {
        foreach (var slot in slotOccupied)
        {
            if (slot) return true;
        }
        
        return false;
    }

    private Vector3 GetPutDownPos()
    {
        int count = Breads.Count;

        int perRow = 3;
        int perLayer = 6;

        int layer = count / perLayer;
        int rowInLayer = (count % perLayer) / perRow;
        int colInRow = count % perRow;

        Vector3 pos = startPos;
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