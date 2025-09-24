using System.Collections.Generic;
using UnityEngine;

public class GenericPool<T> where T : MonoBehaviour
{
    private readonly Queue<T> pool = new Queue<T>();
    private readonly T prefab;
    private readonly Transform parent;

    public GenericPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            T obj = CreatePoolObject();
            pool.Enqueue(obj);
            obj.gameObject.SetActive(false);
        }
    }

    private T CreatePoolObject()
    {
        T obj = Object.Instantiate(prefab, parent);
        obj.gameObject.SetActive(false);
        return obj;
    }

    public T Get(Vector3 position, Quaternion rotation)
    {
        T obj = pool.Count > 0 ? pool.Dequeue() : CreatePoolObject();
        obj.transform.SetPositionAndRotation(position, rotation);
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(parent);
        pool.Enqueue(obj);
    }
}