using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T :MonoBehaviour,new()
{
    private static T _instance;
    public static T Instance {
        get { 
            return _instance;
        }
    }
    public virtual void Awake() {
        _instance = this as T;
    }
}
