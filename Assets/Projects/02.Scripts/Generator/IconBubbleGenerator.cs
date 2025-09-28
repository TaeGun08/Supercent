using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconBubbleGenerator : MonoBehaviour
{
    [Header("IconBubbleGenerator Settings")]
    [SerializeField] private IconBubble iconBubblePrefab;
    [SerializeField] private int initialSize;
    [SerializeField] private Transform parent;

    private GenericPool<IconBubble> iconBubblePool;

    private void Awake()
    {
        iconBubblePool = new GenericPool<IconBubble>(iconBubblePrefab, initialSize, parent);
    }

    public IconBubble GetGenerate()
    {
        IconBubble iconBubble = iconBubblePool.Get(Vector3.zero, Quaternion.identity);
        iconBubble.transform.SetAsFirstSibling();
        return iconBubble;
    }

    public void Return(IconBubble iconBubble)
    {
        iconBubblePool.Return(iconBubble);
    }
}
