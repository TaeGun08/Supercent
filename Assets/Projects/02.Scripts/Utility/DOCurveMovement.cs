using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class DOCurveMovement : MonoBehaviour
{
    protected Vector3 curTargetPos;
    protected float curYStep;
    protected float curRotate;

    protected Rigidbody rigid;
    protected Collider coll;

    [Header("DOCurveMovement")]
    [SerializeField] protected float duration;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    /// <summary>
    /// 실시간으로 변경되는 위치에 포물선 이동을 위한 함수
    /// </summary>
    /// <param name="targetTrs"></param>
    /// <param name="yStep"></param>
    /// <param name="rotate"></param>
    /// <param name="parent"></param>
    public virtual void SetCurveMovement(Transform targetTrs, float yStep, float rotate, float height, Transform parent)
    {
        rigid.isKinematic = true;
        coll.isTrigger = true;

        transform.SetParent(parent);

        Vector3 startPos = transform.position;
        Vector3 targetPos = targetTrs.position + new Vector3(0, yStep, 0);
        
        Vector3 midPoint = (startPos + targetPos) / 2f;
        midPoint.y += height;

        Vector3 p1 = Vector3.Lerp(startPos, midPoint, 0.5f);
        Vector3 p2 = Vector3.Lerp(midPoint, targetPos, 0.5f);

        float t = 0f;
        DOTween.To(() => t, x => t = x, 1f, duration)
            .SetEase(Ease.OutCubic)
            .SetUpdate(true)
            .OnUpdate(() =>
            {
                targetPos = targetTrs.position + new Vector3(0, yStep, 0);
                transform.position = Bezier.Cubic(startPos, p1, p2, targetPos, t);

                transform.rotation = Quaternion.Euler(
                    0,
                    Mathf.LerpAngle(transform.rotation.eulerAngles.y, targetTrs.rotation.eulerAngles.y + rotate, t),
                    0
                );
            })
            .OnComplete(() =>
            {
                transform.position = targetPos;
                transform.rotation = targetTrs.rotation * Quaternion.Euler(0, rotate, 0);
                ResetTransform();
            });
    }

    /// <summary>
    /// 고정된 값 위치로 포물선 이동을 위한 함수
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="yStep"></param>
    /// <param name="rotate"></param>
    /// <param name="parent"></param>
    public virtual void SetCurveMovement(Vector3 targetPos, float hegiht, float rotate, Transform parent, bool active)
    {
        rigid.isKinematic = true;
        coll.isTrigger = true;

        transform.SetParent(parent);
        
        transform.DOJump(targetPos, hegiht, 1, duration)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                transform.position = targetPos;
                gameObject.SetActive(active);
                ResetVector();
            });

        transform.DORotate(new Vector3(0f, rotate, 0f), duration);
    }

    protected virtual void ResetTransform()
    {
        
    }
    
    protected virtual void ResetVector()
    {
        
    }
}