using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IconBubble : MonoBehaviour
{
    private Camera mainCam;
    private Transform target;
    private Vector3 offset;
    private RectTransform rectTr;

    [Header("IconBubble Settings")] 
    [SerializeField] private GameObject hideObj;
    [SerializeField] private GameObject breadIcon;
    [SerializeField] private GameObject pOSIcon;
    [SerializeField] private GameObject eatIcon;
    [SerializeField] private TMP_Text breadText;
    
    private void Awake()
    {
        rectTr = GetComponent<RectTransform>();
        mainCam = Camera.main;
    }

    public void SetTarget(Transform newTarget, Vector3 newOffset)
    {
        target = newTarget;
        offset = newOffset;
        
        FollowUIController.FollowEvent += UpdatePosition;

        gameObject.SetActive(true);
        hideObj.SetActive(true);
    }

    public void Release()
    {
        FollowUIController.FollowEvent -= UpdatePosition;

        target = null;
        gameObject.SetActive(false);
        InGameManager.Instance.IconBubbleGenerator.Return(this);
    }

    public void UpdateBreadIcon(int count)
    {
        pOSIcon.SetActive(false);
        eatIcon.SetActive(false);
        
        breadIcon.SetActive(true);
        breadText.text = count.ToString();
    }

    public void UpdatePOSIcon()
    {
        breadIcon.SetActive(false);
        eatIcon.SetActive(false);
        
        pOSIcon.SetActive(true);
    }

    public void UpdateEatIcon()
    {
        breadIcon.SetActive(false);
        pOSIcon.SetActive(false);
        
        eatIcon.SetActive(true);
    }

    private void UpdatePosition()
    {
        if (target == null) return;

        Vector3 screenPos = mainCam.WorldToScreenPoint(target.position + offset);

        
        bool inFront = screenPos.z > 0;
        bool inScreenX = screenPos.x >= 0 && screenPos.x <= Screen.width;
        bool inScreenY = screenPos.y >= 0 && screenPos.y <= Screen.height;

        bool isVisible = inFront && inScreenX && inScreenY;

        rectTr.position = screenPos;
        hideObj.SetActive(isVisible);
    }
}
