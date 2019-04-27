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


        mUIFacade.InitUIPanelClassDic();
    }

    public override void ExitScene()
    {
        base.ExitScene();
    }
}
