using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    #region 饿汉式
    //private static Singleton _instance; 
    //public static Singleton Instance {
    //    get {
    //        return _instance;
    //    }
    //} 
    //private void Awake()
    //{
    //    _instance = this;
    //}
    #endregion

    #region 懒汉式
    private static Singleton _instance;
    public static Singleton Instance {
        get {
            if (_instance == null) {
                _instance = new Singleton();
            }
            return _instance;
        }
    }
    #endregion
}
/// <summary>
/// 单利模板
/// </summary> 
public class SingletonTemplate<T>:MonoBehaviour where T:MonoBehaviour {
    private T _instance;
    public T Instance {
        get {
            if (_instance == null) {
                _instance = this as T;
            }
            return _instance;
        }
    }
}
