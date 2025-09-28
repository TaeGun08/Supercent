using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BreadHandler : MonoBehaviour
{
    protected InGameManager InGameManager;
    protected AudioManager AudioManager;
    
    public Stack<Bread> BreadStack { get; private set; } = new Stack<Bread>();

    [Header("BreadHandler Settings")]
    [SerializeField] private Transform handTrs;
    public Transform HandTrs => handTrs;
    [SerializeField] private float yStep = 0.4f;
    [SerializeField] private int maxBread;

    public bool MaxBread => maxBread <= BreadStack.Count;

    protected virtual void Start()
    {
        AudioManager = AudioManager.Instance;
        InGameManager = InGameManager.Instance;
    }

    public void PickupBread(Bread bread)
    {
        BreadStack.Push(bread);
        int index = BreadStack.Count - 1;
        float yOffset = index * yStep;
        
        AudioManager.BreadPickUpSound();
        bread.SetCurveMovement(handTrs, yOffset, 90f, 5f, handTrs);
    }

    public Bread GetBread()
    {
        return BreadStack.Count == 0 ?  null : BreadStack.Pop();
    }
}
