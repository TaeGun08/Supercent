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
    private GenericPool<PaperBag> paperBagPool;
    
    public PaperBag PaperBag { get; set; }

    private void Awake()
    {
        paperBagPool = new GenericPool<PaperBag>(paperBagPrefab, initialSize, transform);
    }

    public void Generate()
    {
        if (PaperBag != null) return;
        PaperBag = paperBagPool.Get(paperBagTrs.position, Quaternion.Euler(0f, 90f, 0f));
        PaperBag.gameObject.SetActive(true);
    }

    public void Return(PaperBag paperBag)
    {
        paperBagPool.Return(paperBag);
    }
}
