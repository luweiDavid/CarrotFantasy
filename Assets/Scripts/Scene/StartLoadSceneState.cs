using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLoadSceneState : BaseSceneState
{
    public StartLoadSceneState(UIFacade uifacade) : base(uifacade) {
        mSceneName = NameConfig.SceneName_StartLoad;
    }

    public override void EnterScene()
    {
        base.EnterScene(); 
        mUIFacade.AddUIPanelGo(NameConfig.PanelName_StartLoad);
        mUIFacade.InitUIPanelClassDic();

        mUIFacade.OpenPanel(NameConfig.PanelName_StartLoad);
    }

    public override void ExitScene()
    {
        base.ExitScene();
    }

}
