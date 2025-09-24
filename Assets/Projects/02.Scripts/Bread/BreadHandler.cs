using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BreadHandler : MonoBehaviour
{
    public Stack<Bread> BreadStack { get; private set; } = new Stack<Bread>();

    [Header("BreadHandler Settings")]
    [SerializeField] private Transform handPos;
    [SerializeField] private float yStep = 0.4f;

    public void PickupBread(Bread bread)
    {
        BreadStack.Push(bread);
        int index = BreadStack.Count - 1;
        float yOffset = index * yStep;
        
        bread.SetBread(handPos, yOffset);
    }

    public Bread PutDownBread()
    {
        return BreadStack.Count == 0 ? null : BreadStack.Pop();
    }
}
