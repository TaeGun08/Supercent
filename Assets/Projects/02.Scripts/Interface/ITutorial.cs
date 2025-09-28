using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tutorial
{
    Oven,
    BasketTable,
    POSTable,
    ContentA,
    ContentB,
}

public interface ITutorial
{
    public Tutorial Tutorial { get; }
    public Tutorial NextTutorial { get; }
}
