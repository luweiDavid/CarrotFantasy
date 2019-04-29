using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNormalLevelPanel : BaseUIPanel
{
   

    public override void Awake()
    {
        base.Awake();
    }

    public override void __Init()
    {
        base.__Init();
       
    }
    public override void __Enter()
    {
        mPanelGo.SetActive(true);
    }

    public override void __Update()
    {
        base.__Update();
    } 

    public override void __Close()
    {
        mPanelGo.SetActive(false);
    }

    public override void __Exit()
    {
        base.__Exit();
    }
}
