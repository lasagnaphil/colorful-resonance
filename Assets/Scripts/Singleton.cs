using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    public virtual void Awake()
    {
        if (Instance == null)
        {
            T[] instances = FindObjectsOfType<T>() as T[];
            if (instances.Length == 0)
            {
                Debug.LogError("Cannot find singleton; in type " + typeof(T));
            }
            else if (instances.Length > 1)
            {
                Debug.LogError("Singleton count is wrong: " + instances.Length + "; in type " + typeof(T));
            }
            else
            {
                Instance = instances[0];
            }
        }
        else
        {
            Debug.LogError("Singleton is duplicated; in type " + typeof(T));
        }
    }

    void OnDestroy()
    {
        if (Instance == null)
        {
            Debug.LogError("Singleton is not instantiated; in type " + typeof(T));
        }
        else
        {
            Instance = null;
        }
    }
}
