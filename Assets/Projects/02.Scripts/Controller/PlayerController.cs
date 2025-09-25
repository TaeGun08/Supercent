using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private CharacterController characterController;

    private JoyStickController joyStickController;

    [Header("Movement Settings")] [SerializeField]
    private float smoothTime;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundCheckDistance = 0.1f;

    private float turnCalmVelocity;
    private float velocityY;

    private void Awake()
    {
        player = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        joyStickController = InGameManager.Instance.JoyStickController;
    }

    private void Update()
    {
        MovementAndRotation();
        Gravity();
    }

    /// <summary>
    /// 조이스틱 입력에 따른 이동 및 회전 처리
    /// </summary>
    private void MovementAndRotation()
    {
        Vector2 inputVec = joyStickController.DragDirection();
        
        if (inputVec.sqrMagnitude < 0.01f) return;
        
        inputVec.Normalize();
        
        float targetAngle = Mathf.Atan2(inputVec.x, inputVec.y) * Mathf.Rad2Deg;

        if (float.IsNaN(targetAngle) || float.IsInfinity(targetAngle))
            return;

        float angle = Mathf.SmoothDampAngle(
            transform.eulerAngles.y,
            targetAngle,
            ref turnCalmVelocity,
            smoothTime
        );

        if (!float.IsNaN(angle) && !float.IsInfinity(angle))
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        
        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        characterController.Move(moveDirection.normalized * (player.PlayerStatus.MoveSpeed * Time.deltaTime));
    }
    
    /// <summary>
    /// 중력 적용 함수
    /// </summary>
    private void Gravity()
    {
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f, groundMask);

        if (isGrounded && velocityY < 0f)
        {
            velocityY = -2f;
        }

        velocityY += player.PlayerStatus.Gravity * Time.deltaTime;
        characterController.Move(Vector3.up * (velocityY * Time.deltaTime));
    }
}