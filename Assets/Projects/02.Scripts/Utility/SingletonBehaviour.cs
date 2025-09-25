using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    protected static bool IsQuitting = false;
    
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                if (!Application.isPlaying || IsQuitting) 
                    return null;
                
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }
                    
                DontDestroyOnLoad(instance);
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this as T;
        DontDestroyOnLoad(gameObject);
    }
    
    protected virtual void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
    
    protected virtual void OnApplicationQuit()
    {
        IsQuitting = true;
    }
}
