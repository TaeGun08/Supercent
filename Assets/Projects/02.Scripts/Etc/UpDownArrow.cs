using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UpDownArrow : MonoBehaviour
{
    [Header("UpDownArrow Settings")] 
    [SerializeField] private float amplitude;
    [SerializeField] private float duration;
    [SerializeField] private Ease ease;
    [SerializeField] private LoopType loopType;

    Vector3 initialLocalPos;
    Tween moveTween;
    
    private void OnDisable()
    {
        StopAndReset();
    }

    public void Set(Transform target)
    {
        initialLocalPos = target.position;
        transform.localPosition = initialLocalPos;
        StopAndReset();
        Play();
    }

    public void Play()
    {
        moveTween?.Kill();

        moveTween = transform.DOLocalMoveY(initialLocalPos.y + amplitude, duration)
            .SetEase(ease)
            .SetLoops(-1, loopType)
            .SetUpdate(true);
    }

    public void StopAndReset()
    {
        moveTween?.Kill();
        moveTween = null;
    }
}
