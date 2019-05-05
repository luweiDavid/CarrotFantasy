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
        SetPageStatus(); 
    }

    private void SetPageStatus() {
        for (int i = 0; i < mTotalChildCount; i++)
        {
            SetPageStatus(mPlayMgr.bigLevelStatusList[i],
                          mPlayMgr.levelUnlockedNumList[i],
                          mPlayMgr.levelTotalNumList[i],
                          mContentChilds[i], i + 1);
        }
    }
    /// <summary>
    /// 设置大关卡数据
    /// </summary>
    /// <param name="isLocked">是否解锁</param>
    /// <param name="unlockedNum">解锁的小关卡数量</param>
    /// <param name="totalNum">小关卡总数</param>
    /// <param name="itemTr">大关卡item</param>
    /// <param name="bigId">大关卡id</param>
    private void SetPageStatus(bool unLocked,int unlockedNum,int totalNum, Transform itemTr,int bigId) {
        Transform lockTr = itemTr.Find("Img_Lock").transform;
        Transform pageTr = itemTr.Find("Img_Page").transform;
        Button itemBtn = itemTr.GetComponent<Button>();
        Text pageTxt = itemTr.Find("Img_Page/Tex_Page").GetComponent<Text>();

        lockTr.gameObject.SetActive(!unLocked);
        pageTr.gameObject.SetActive(unLocked);
        itemBtn.interactable = unLocked;
        pageTxt.text = string.Format("{0}/{1}", unlockedNum, totalNum);

        itemBtn.onClick.RemoveAllListeners();
        itemBtn.onClick.AddListener(() => {
            CloseSelf();
            GameNormalOptionPanel optionPanel = mUIFacade.GetUIPanelClass(NameConfig.PanelName_GameNormalOption) as GameNormalOptionPanel;
            optionPanel.IsInBigLevelPanel = false;
            GameNormalLevelPanel levelPanel = mUIFacade.GetUIPanelClass(NameConfig.PanelName_GameNormalLevel) as GameNormalLevelPanel;
            levelPanel.EnterPanel(bigId, totalNum);

            mUIFacade.OpenPanel(NameConfig.PanelName_GameNormalLevel);
        });
    }

    public override void __Enter()
    {
        SetPageStatus();
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
}
