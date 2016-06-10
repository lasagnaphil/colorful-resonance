﻿using System;
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
            if (instances.Length != 1)
            {
                Debug.LogError("Singleton count is wrong " + instances.Length);
            }

            Instance = FindObjectOfType<T>();

            if (Instance == null)
            {
                Debug.LogError("Cannot find singleton");
            }
        }
        else
        {
            Debug.LogError("Singleton is duplicated");
        }
    }

    void OnDestroy()
    {
        if (Instance == null)
        {
            Debug.LogError("Sigleton is not instantiated");
        }
        else
        {
            Instance = null;
        }
    }
}
