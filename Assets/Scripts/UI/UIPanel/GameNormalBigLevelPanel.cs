using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNormalBigLevelPanel : BaseUIPanel
{
    private PlayerManager mPlayMgr;
    private ScrollRectExtension mScrollRectEx;
    private int mTotalChildCount = 0;
    private Transform mContent;
    private Transform[] mContentChilds;
     

    public override void Awake()
    {
        base.Awake();
    }

    public override void __Init()
    {
        base.__Init();
        mPlayMgr = mUIFacade.mPlayerMgr;

        mScrollRectEx = mPanelGo.transform.Find("ScrollView").GetComponent<ScrollRectExtension>();
        mContent = mPanelGo.transform.Find("ScrollView/Viewport/Content").GetComponent<Transform>();
        mTotalChildCount = mScrollRectEx.ItemCount;
        mContentChilds = new Transform[mTotalChildCount];
        for (int i = 0; i < mTotalChildCount; i++)
        {
            mContentChilds[i] = mContent.GetChild(i);
        } 
    }
    private void SetPageStatus(bool isLocked,Transform itemTr,int curTotalCount) {
        Transform lockTr = itemTr.Find("Img_Lock").transform;
        Transform pageTr = itemTr.Find("Img_Page").transform;
        Button itemBtn = itemTr.GetComponent<Button>();
        Text pageTxt = itemTr.Find("Img_Page/Tex_Page").GetComponent<Text>();

        lockTr.gameObject.SetActive(isLocked);
        pageTr.gameObject.SetActive(!isLocked);
        itemBtn.interactable = !isLocked;

        itemBtn.onClick.AddListener(() => {
            CloseSelf();
            GameNormalOptionPanel optionPanel = mUIFacade.GetUIPanelClass(NameConfig.PanelName_GameNormalOption) as GameNormalOptionPanel;
            optionPanel.IsInBigLevelPanel = false;
            GameNormalLevelPanel levelPanel = mUIFacade.GetUIPanelClass(NameConfig.PanelName_GameNormalLevel) as GameNormalLevelPanel;
           

            mUIFacade.OpenPanel(NameConfig.PanelName_GameNormalLevel);
        });
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
