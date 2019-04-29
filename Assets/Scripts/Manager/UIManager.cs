using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public GameManager mGameMgr;
    public UIFacade mUIFacade; 
    
    //实例化出来的panel物体
    public Dictionary<string, GameObject> panelGoDic = new Dictionary<string, GameObject>();

    public UIManager() {
        mGameMgr = GameManager.Instance;
        mUIFacade = new UIFacade(this);
    } 

    /// <summary>
    /// 添加一个实例化的panelGo
    /// </summary>
    public void AddPanel(string name,GameObject panelGo)
    {
        if (!panelGoDic.ContainsKey(name))
        {
            panelGoDic.Add(name, panelGo);
        }
    }
    /// <summary>
    /// 放回工厂
    /// </summary>
    public void ClearPanelGoDic() {
        foreach (string key in panelGoDic.Keys)
        { 
            GameObject go = panelGoDic[key]; 
            mGameMgr.PushItem(FactoryType.UIPanel, key, go);
        }
        panelGoDic.Clear();
    }

}
