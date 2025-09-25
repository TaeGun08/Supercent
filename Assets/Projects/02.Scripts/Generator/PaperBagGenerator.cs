using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBagGenerator : MonoBehaviour
{
    [Header("PaperBagGenerator Settings")]
    [SerializeField] private PaperBag paperBagPrefab;
    [SerializeField] private int initialSize = 10;
    [SerializeField] private Transform paperBagTrs;
    public Transform PaperBagTrs => paperBagTrs;
    public GenericPool<PaperBag> PaperBagPool { get; private set; }
    
    public PaperBag PaperBag { get; set; }

    private void Awake()
    {
        PaperBagPool = new GenericPool<PaperBag>(paperBagPrefab, initialSize, transform);
    }

    public void Generate()
    {
        PaperBag = PaperBagPool.Get(paperBagTrs.position, Quaternion.Euler(0f, 90f, 0f));
        PaperBag.gameObject.SetActive(true);
    }
}
