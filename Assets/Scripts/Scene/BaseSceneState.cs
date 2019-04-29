
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 场景状态基类
/// </summary>
public class BaseSceneState : IBaseSceneState
{
    protected UIFacade mUIFacade;
    public string mSceneName { get; set; }

    public BaseSceneState(UIFacade uif) { 
        mUIFacade = uif;
    }
    public virtual void EnterScene()
    {

    } 
    public virtual void ExitScene()
    {
        mUIFacade.ClearUIPanelGo();
    }
}
