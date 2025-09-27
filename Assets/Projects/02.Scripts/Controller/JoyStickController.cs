using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStickController : MonoBehaviour
{
    [Header("JoyController")]
    [SerializeField] private RectTransform background;  
    [SerializeField] private RectTransform handle;      
    [SerializeField] private RectTransform touchPoint;
    [Space]
    [SerializeField] private float handleSpeed;
    
    private float radius;
    
    private Vector3 startPos;
    private Vector3 dragDirection;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            StartStick();

        if (Input.GetMouseButton(0))
            DragStick();

        if (Input.GetMouseButtonUp(0))
            EndStick();
    }
    
    /// <summary>
    /// 클릭을 시작했을 때
    /// </summary>
    private void StartStick()
    {
        startPos = Input.mousePosition;
        background.gameObject.SetActive(true);

        background.position = startPos;
        handle.position = startPos;
        touchPoint.position = startPos;
        
        radius = background.sizeDelta.x * 0.216f;
    }
    
    /// <summary>
    /// 드래그 중일 때
    /// </summary>
    private void DragStick()
    {
        Vector3 direction = Input.mousePosition - startPos;
        
        direction = Vector3.ClampMagnitude(direction, radius);

        handle.position = Vector3.Lerp(
            handle.position,
            startPos + direction,
            Time.deltaTime * handleSpeed
        );

        dragDirection = handle.position - startPos;
        touchPoint.position = Input.mousePosition;
    }
    
    /// <summary>
    /// 화면에 손을 땠을 때
    /// </summary>
    private void EndStick()
    {
        background.gameObject.SetActive(false);
        dragDirection = Vector3.zero;
    }

    public Vector2 DragDirection()
    {
        if (radius <= 0f)
            return Vector2.zero;
    
        Vector2 normalized = new Vector2(dragDirection.x, dragDirection.y) / radius;
        return Vector2.ClampMagnitude(normalized, 1f);
    }
}


