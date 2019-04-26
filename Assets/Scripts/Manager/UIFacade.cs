using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFacade
{
    public GameManager mGameMgr;
    public UIManager mUIMgr;
    public PlayerManager mPlayerMgr;
    public AudioManager mAudioMgr;

    public GameObject mUIRoot;
    public GameObject mMaskGo;

    public Dictionary<string, IBaseUIPanel> uiPanelDic = new Dictionary<string, IBaseUIPanel>();

    public UIFacade(UIManager uiMgr) {
        mGameMgr = GameManager.Instance;
        mUIMgr = uiMgr;

        mUIRoot = mGameMgr.ThisGo().transform.Find("UIRoot").GetComponent<GameObject>();
        Debug.Log(mUIRoot);

        mMaskGo = mGameMgr.GetUIGo("Img_Mask");
        mMaskGo.transform.SetParent(mUIRoot.transform);
    } 


}
