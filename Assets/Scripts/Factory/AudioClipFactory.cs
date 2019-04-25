using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipFactory : BaseResFactory<AudioClip>
{ 
    public Dictionary<string, AudioClip> clipDic = new Dictionary<string, AudioClip>();
     

    public override AudioClip GetRes(string name)
    {
        AudioClip clip = null;
        LoadPath += name;
        if (clipDic.ContainsKey(name))
        {
            clip = clipDic[name];
        }
        else {
            clip = Resources.Load<AudioClip>(LoadPath);
            if (clip != null) {
                clipDic.Add(name, clip);
            }
        }
        if (clip == null) {
            Debug.LogError("资源加载失败，路径：" + LoadPath);
        }
        return clip;
    }

}
