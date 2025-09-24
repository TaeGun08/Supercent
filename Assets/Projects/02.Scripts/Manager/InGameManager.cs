using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : SingletonBehaviour<InGameManager>
{
    public Queue<CustomerController> Customers { get; private set; } = new Queue<CustomerController>();
}