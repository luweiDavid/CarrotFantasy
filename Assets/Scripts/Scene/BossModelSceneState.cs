using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossModelSceneState : BaseSceneState
{
    public BossModelSceneState(UIFacade uif) : base(uif)
    {

        mSceneName = NameConfig.SceneName_BossModel;
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
