using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;

public class AI : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
}