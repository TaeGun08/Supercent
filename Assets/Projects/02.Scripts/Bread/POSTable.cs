using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POSTable : MonoBehaviour
{
    private InGameManager InGameManager;
    
    [SerializeField] private Vector3[] startPos;
    [SerializeField] private float zStep;

    private void Start()
    {
        InGameManager = InGameManager.Instance;
    }
    
    public Vector3 GetPos(int posIndex, int index)
    {
        Vector3 pos = startPos[posIndex];
        pos.z += index * zStep;
        return pos;
    }
}
