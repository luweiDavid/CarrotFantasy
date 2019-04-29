using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNormalOptionSceneState : BaseSceneState
{
    public GameNormalOptionSceneState(UIFacade uif) : base(uif)
    {
        mSceneName = NameConfig.SceneName_GameNormalOption;
    }

    public override void EnterScene()
    {
        base.EnterScene();
        mUIFacade.AddUIPanelGo(NameConfig.PanelName_GameNormalOption);
        mUIFacade.AddUIPanelGo(NameConfig.PanelName_GameNormalBigLevel);
        mUIFacade.AddUIPanelGo(NameConfig.PanelName_GameNormalLevel); 

        mUIFacade.InitUIPanelClassDic();

        mUIFacade.OpenPanel(NameConfig.PanelName_GameNormalOption);
        mUIFacade.OpenPanel(NameConfig.PanelName_GameNormalBigLevel);
    }

    public override void ExitScene()
    {
        base.ExitScene();
    }
}
