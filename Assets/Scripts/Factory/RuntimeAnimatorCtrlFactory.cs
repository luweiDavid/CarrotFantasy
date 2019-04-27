using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeAnimatorCtrlFactory : BaseResFactory<RuntimeAnimatorController>
{
    public Dictionary<string, RuntimeAnimatorController> animatorCtrlDic = new Dictionary<string, RuntimeAnimatorController>();


    public RuntimeAnimatorCtrlFactory() {
        LoadPath = "Animator/AnimatorController/";
    }

    public override RuntimeAnimatorController GetRes(string name)
    {
        RuntimeAnimatorController animatorCtrl = null;
        string path = LoadPath + name;
        if (animatorCtrlDic.ContainsKey(name))
        {
            animatorCtrl = animatorCtrlDic[name];
        }
        else {
            animatorCtrl = Resources.Load<RuntimeAnimatorController>(path);
            if (animatorCtrl != null) {
                animatorCtrlDic.Add(name, animatorCtrl);
            }
        }
        if (animatorCtrl == null) {
            Debug.LogError("资源加载失败，路径：" + path);
        }
        return animatorCtrl;
    }
}
