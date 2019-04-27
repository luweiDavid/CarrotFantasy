using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNestSceneState : BaseSceneState
{
    public MonsterNestSceneState(UIFacade uif) : base(uif)
    {
        mSceneName = NameConfig.SceneName_MonsterNest;
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
    