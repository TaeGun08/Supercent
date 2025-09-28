using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadMaxUI : MonoBehaviour
{
    private Camera mainCam;
    private Transform target;
    private Vector3 offset;
    private RectTransform rectTr;
    
    private BreadHandler breadHandler;
    
    [Header("BreadMaxUI Settings")] 
    [SerializeField] private GameObject hideObj;
    
    private void Awake()
    {
        rectTr = GetComponent<RectTransform>();
        mainCam = Camera.main;
    }

    public void SetTarget(BreadHandler handler, Vector3 newOffset)
    {
        breadHandler = handler;
        target = handler.transform;
        offset = newOffset;
        
        FollowUIController.FollowEvent += UpdatePosition;
    }
    
    private void UpdatePosition()
    {
        if (target == null) return;

        Vector3 screenPos = mainCam.WorldToScreenPoint(target.position + offset);
        
        rectTr.position = screenPos;
        hideObj.SetActive(breadHandler.MaxBread);
    }
}
