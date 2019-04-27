using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLoadPanel : BaseUIPanel
{
    public override void Awake()
    {
        base.Awake();     }
    
    public override void __Init()
    {
        base.__Init();
        
        Invoke("LoadNextScene", 0.5f);
    }
    public override void __Enter()
    {
        base.__Enter();
    }

    public override void __Update()
    {
        base.__Update();
    }

    public override void __Close()
    {
        base.__Close();
    }

    public override void __Exit()
    {
        base.__Exit();
    }

    public void LoadNextScene()
    {
        mUIFacade.ChangeSceneState(new MainSceneState(mUIFacade));
    }
}
