using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperBag : DOCurveMovement
{
    private static readonly int CLOSE = Animator.StringToHash("Close");
    private static readonly int APPEAR = Animator.StringToHash("Appear");
    
    private Animator animator;
    
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        AppearAnimationPlay();
    }

    private void ResetAllTrigger()
    {
        animator.ResetTrigger(APPEAR);
        animator.ResetTrigger(CLOSE);
    }

    public void AppearAnimationPlay()
    {
        ResetAllTrigger();
        animator.SetTrigger(APPEAR);
    }

    public void CloseAnimationPlay()
    {
        ResetAllTrigger();
        animator.SetTrigger(CLOSE);
    }
    
    public void Return()
    {
        gameObject.SetActive(false);
        InGameManager.Instance.PaperBagGenerator.Return(this);
    }
}
