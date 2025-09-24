using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private Rigidbody rigid;
    
    private JoyStickController joyStickController;

    [Header("Movement Settings")]
    [SerializeField] private float smoothTime;
    private float turnCalmVelocity;
    
    private void Awake()
    {
        player = GetComponent<Player>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        joyStickController = JoyStickController.Instance;
    }

    private void Update()
    {
        ApplyMovementAndRotation();
    }

    /// <summary>
    /// 조이스틱 입력에 따른 이동 및 회전 처리
    /// </summary>
    private void ApplyMovementAndRotation()
    {
        Vector2 inputVec = joyStickController.DragDirection();
        
        if (inputVec.sqrMagnitude < 0.01f) return;
        
        float targetAngle = Mathf.Atan2(inputVec.x, inputVec.y) * Mathf.Rad2Deg;
        
        float angle = Mathf.SmoothDampAngle(
            transform.eulerAngles.y,
            targetAngle,
            ref turnCalmVelocity,
            smoothTime
        );
        
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        
        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        Vector3 movePosition = transform.position + moveDirection.normalized * (player.PlayerStatus.MoveSpeed * Time.deltaTime);

        rigid.MovePosition(movePosition);
    }
}
