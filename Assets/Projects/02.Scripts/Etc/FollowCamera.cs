using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : SingletonBehaviour<FollowCamera>
{
    private Player player;
    private Vector3 offset;

    [Header("FollowCamera")] [SerializeField]
    private float duration;
    
    private void Start()
    {
        player = Player.Instance;
        offset = transform.position;
    }

    private void LateUpdate()
    {
        if (player == null) return;
        transform.position = player.transform.position + offset;
    }

    public void SetCameraTarget(Vector3 targetPos)
    {
        StartCoroutine(TargetMoveCoroutine(player.transform.position, targetPos));
    }

    public void ResetCamera()
    {
        StartCoroutine(TargetMoveCoroutine(transform.position, player.transform.position));
    }
    
    private IEnumerator TargetMoveCoroutine(Vector3 startPos, Vector3 endPos)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        float time = 0f;
        
        while (true)
        {
            time += Time.deltaTime;
            float t = time / duration;
            
            transform.position = Vector3.Lerp(startPos, endPos, t);
            
            yield return wait;
        }
    }
}
