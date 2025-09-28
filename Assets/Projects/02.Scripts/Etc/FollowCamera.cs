using UnityEngine;
using DG.Tweening;

public class FollowCamera : SingletonBehaviour<FollowCamera>
{
    private Vector3 offset;

    [Header("FollowCamera Settings")]
    [SerializeField] private float duration;
    [SerializeField] private float waitTime;
    [SerializeField] private Player player;

    private Tween moveTween;
    public bool CamMovement { get; private set; }

    private void Start()
    {
        if (player != null)
            offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        if (player == null || CamMovement) return;
        transform.position = player.transform.position + offset;
    }
    
    public void MoveToAndReturn(Vector3 targetPos)
    {
        if (player == null) return;
        
        moveTween?.Kill();

        CamMovement = true;

        float currentY = transform.position.y;

        Vector3 targetCamPos = targetPos + offset;
        targetCamPos.y = currentY;

        moveTween = DOTween.Sequence()
            .Append(transform.DOMove(targetCamPos, duration).SetEase(Ease.OutCubic))
            .AppendInterval(waitTime)
            .Append(transform.DOMove(player.transform.position + offset, duration).SetEase(Ease.OutCubic))
            .OnComplete(() => CamMovement = false);
    }
}