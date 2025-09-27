using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DOAnimation : MonoBehaviour
{
    [Header("Rotation Animation Settings")] [SerializeField]
    private float rotateDuration;

    [SerializeField] private float rotateX, rotateY, rotateZ;

    [Space] [Header("Scale Animation Settings")] [SerializeField]
    private float scaleDuration;

    [SerializeField] private float scaleX, scaleY, scaleZ;
    [Space] [SerializeField] private bool onEnableRotateAnimPlay;
    [SerializeField] private bool onEnableScaleAnimPlay;

    private void Start()
    {
        if (onEnableRotateAnimPlay) transform.rotation = Quaternion.identity;
        if (onEnableScaleAnimPlay) transform.localScale = Vector3.zero;
    }

    private void OnEnable()
    {
        if (onEnableRotateAnimPlay) RotateAnimation();
        if (onEnableScaleAnimPlay) ScaleAnimation();
    }

    public void RotateAnimation()
    {
        transform.DORotate(new Vector3(rotateX, rotateY, rotateZ), rotateDuration);
    }

    public void SetRotateAnimation(Vector3 rotation, float duration)
    {
        transform.DORotate(rotation, duration);
    }

    public void ScaleAnimation()
    {
        transform.DOScale(new Vector3(scaleX, scaleY, scaleZ), scaleDuration);
    }

    public void SetScaleAnimation(Vector3 scale, float duration)
    {
        transform.DOScale(scale, duration);
    }
}
