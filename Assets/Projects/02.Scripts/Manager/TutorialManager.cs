using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TutorialManager : SingletonBehaviour<TutorialManager>
{
    private FollowCamera followCamera;
    
    [Header("Tutorial Settings")]
    [SerializeField] private UpDownArrow upDownArrow;
    [SerializeField] private Transform[] points;
    [SerializeField] private Transform lookArrow;

    [Header("Arrow Move/Rotate Settings")]
    [SerializeField] private Vector3 moveOffset;
    [SerializeField] private float moveDuration;
    [SerializeField] private float rotateDuration;

    private Transform target;
    private Tween moveTween;
    private Tween rotateTween;
    private Tutorial curTutorial = Tutorial.Oven;

    private void Start()
    {
        followCamera = Camera.main.GetComponent<FollowCamera>();
        target = FindObjectOfType<Player>().transform;
        upDownArrow.Set(points[0]);
    }

    public void SetPoints(Tutorial getTutorial, Tutorial nextTutorial)
    {
        if (curTutorial != getTutorial) return;
        curTutorial = nextTutorial;
        followCamera.MoveToAndReturn(points[(int)curTutorial].position + new Vector3(0, 0, -5));
        upDownArrow.Set(points[(int)curTutorial]);
    }
    
    private void Update()
    {
        if (target == null) return;
        
        Vector3 desiredPos = target.position + moveOffset;
        lookArrow.position = desiredPos;
        
        Vector3 dir = upDownArrow.transform.position - lookArrow.position;
        dir.y = 0f;
        if (dir.sqrMagnitude <= 0.001f) return;
        Quaternion targetRot = Quaternion.LookRotation(dir.normalized, Vector3.up);
        lookArrow.rotation = targetRot;
    }
}
