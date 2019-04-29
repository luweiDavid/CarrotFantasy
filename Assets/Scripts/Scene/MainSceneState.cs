using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneState : BaseSceneState
{
    public MainSceneState(UIFacade uif) : base(uif) {

        mSceneName = NameConfig.SceneName_Main;
    }
    public override void EnterScene()
    {
        base.EnterScene();
        //加载当前场景的所有panel
        mUIFacade.AddUIPanelGo(NameConfig.PanelName_Main);
        mUIFacade.AddUIPanelGo(NameConfig.PanelName_NormalModel);
        mUIFacade.AddUIPanelGo(NameConfig.PanelName_BossModel);
        mUIFacade.AddUIPanelGo(NameConfig.PanelName_Help);
        mUIFacade.AddUIPanelGo(NameConfig.PanelName_Set);
        mUIFacade.AddUIPanelGo(NameConfig.PanelName_GameLoad); 
        mUIFacade.InitUIPanelClassDic(); 
        mUIFacade.OpenPanel(NameConfig.PanelName_Main);
    }

    public override void ExitScene()
    {
        base.ExitScene();  
    }

}
