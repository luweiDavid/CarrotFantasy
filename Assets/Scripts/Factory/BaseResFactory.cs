using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源工厂：音频，图片等
/// </summary> 
public class BaseResFactory<T>
{
    public string LoadPath = "";
    public BaseResFactory() {
        LoadPath = "AudioClips/";
    }

    public virtual T GetRes(string name) {
        return default(T);
    }
}
