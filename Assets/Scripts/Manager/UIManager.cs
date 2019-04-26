using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public GameManager mGameMgr;
    public UIFacade mUIFacade; 
    
    //实例化出来的panel物体
    public Dictionary<string, GameManager> panelGoDic = new Dictionary<string, GameManager>();

    public UIManager() {
        mGameMgr = GameManager.Instance;
        mUIFacade = new UIFacade(this);
    }
}
