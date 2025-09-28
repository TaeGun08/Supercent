using System;
using UnityEngine;

public class EatingHole : MonoBehaviour
{
    [Header("EatingHole Settings")] 
    [SerializeField] private Table[] tables;
    
    public void SetCustomerEmptyTable(Customer customer)
    {
        foreach (var table in tables)
        {
            if (!table.EatingCustomerEmpty() || !table.gameObject.activeSelf) continue;
            table.SetCustomer(customer);
            break;
        }
    }
}
