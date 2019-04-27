using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalModelSceneState : BaseSceneState
{
    public NormalModelSceneState(UIFacade uif) : base(uif)
    {
        mSceneName = NameConfig.SceneName_NormalModel;
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
