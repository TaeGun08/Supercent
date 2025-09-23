using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasketTable : MonoBehaviour
{
    [SerializeField] private Transform[] targetsPos;
    [SerializeField] private int maxActiveAI = 3;

    private AIGenerator aiGenerator;
    private Queue<AI> aiQueue = new Queue<AI>();
    
    private void Start()
    {
        aiGenerator = AIGenerator.Instance;
    }

    [SerializeField] private int maxCustomerCount = 3;
    
    public void StartTable()
    {
        
    }

    public void LeaveCustomer()
    {
        aiQueue.Dequeue();
    }
    
    private void LoadCustomer()
    {
        for (int i = aiQueue.Count; i < maxCustomerCount; i++)
        {
            aiQueue.Enqueue(aiGenerator.GetAI());
        }
    }
}