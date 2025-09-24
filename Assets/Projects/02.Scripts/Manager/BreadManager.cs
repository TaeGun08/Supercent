using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadManager : SingletonBehaviour<BreadManager>
{
    public int BreadCount = 20;
    
    [SerializeField] private BasketTable basketTable;
    public BasketTable BasketTable => basketTable;
    
    public event Action BreadEvent;
    public event Action OnBreadRestocked;

    private void Update()
    {
        BreadEvent?.Invoke();
    }

    public bool TakeBread()
    {
        if (BreadCount > 0)
        {
            BreadCount--;
            return true;
        }
        return false;
    }

    public void RestockBread(int amount)
    {
        BreadCount += amount;
        OnBreadRestocked?.Invoke();
    }
}
