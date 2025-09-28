using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int MOVE = Animator.StringToHash("Move");
    private static readonly int IS_STACK = Animator.StringToHash("IsStack");
    private Player player;
    private CharacterController characterController;
    private Animator animator;

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
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        joyStickController = InGameManager.Instance.JoyStickController;
    }

    private void Update()
    {
        UpdateMovementAndRotation();
        UpdateGravity();
        UpdateAnimation();
    }

    /// <summary>
    /// 조이스틱 입력에 따른 이동 및 회전 처리
    /// </summary>
    private void UpdateMovementAndRotation()
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
    private void UpdateGravity()
    {
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f, groundMask);

        if (isGrounded && velocityY < 0f)
        {
            velocityY = -2f;
        }

        velocityY += player.PlayerStatus.Gravity * Time.deltaTime;
        characterController.Move(Vector3.up * (velocityY * Time.deltaTime));
    }

    /// <summary>
    /// 애니메이션 갱신 함수
    /// </summary>
    private void UpdateAnimation()
    {
        Vector2 inputVec = joyStickController.DragDirection();
        inputVec.Normalize();

        animator.SetFloat(MOVE, inputVec == Vector2.zero ? 0 : 1);
        
        animator.SetBool(IS_STACK, player.BreadStack.Count > 0);
    }
}