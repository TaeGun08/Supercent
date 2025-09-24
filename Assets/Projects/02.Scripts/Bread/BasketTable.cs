using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class BasketTable : OnTriggerInteraction
{
    private Stack<Bread> breads = new Stack<Bread>();
    
    [Space]
    [Header("BasketTable Settings")]
    [SerializeField] private Vector3 startPos;
    [SerializeField] private float xStep = 0.5f;
    [SerializeField] private float zStep = -1f;
    [SerializeField] private float yStep = 0.5f;
    [Space]
    [SerializeField] private Transform[] targetPos;

    private bool[] slotOccupied;

    private void Awake()
    {
        slotOccupied = new bool[targetPos.Length];
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

    public Vector3 GetPutDownPos()
    {
        int count = breads.Count;

        int perRow = 3;
        int perLayer = 6;

        int layer = count / perLayer;
        int row = (count % perLayer) / perRow;
        int col = count % perRow;

        Vector3 pos = startPos;
        pos.x += col * xStep;
        pos.z += row * zStep;
        pos.y += layer * yStep;

        return pos;
    }

    public void PickUpPos()
    {
        if (breads.Count == 0) return;

        int count = breads.Count - 1;
        int perRow = 3;
        int perLayer = 6;

        int layer = count / perLayer;
        int row = (count % perLayer) / perRow;
        int col = count % perRow;

        Vector3 pos = startPos;
        pos.x += col * xStep;
        pos.z += row * zStep;
        pos.y += layer * yStep;
    }

    public void PutDown(Bread bread)
    {
        breads.Push(bread);
        bread.transform.position = GetPutDownPos();
    }

    public Bread PickUp()
    {
        if (breads.Count == 0) return null;
        PickUpPos();
        return breads.Pop();
    }
}