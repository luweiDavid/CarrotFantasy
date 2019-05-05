using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 

public class GameNormalLevelPanel : BaseUIPanel
{
    private int mBigId = 0;
    private int mTotalNum = 0; 

    private Image mImgLeft;
    private Image mImgRight;
    private Button mBtnStart; 
    private Transform mImgLockTr;
    private Transform mEmpTowerTr;
    private Text mWaveTxt;

    private ScrollRectExtension mScrollRectEx;
    private Transform mContentTr; 

    private Dictionary<string, GameObject> mItemGoDic;
    private Dictionary<string, GameObject> mTowerGoDic;

    private PlayerManager mPlayerMgr;
    private int mCurPageIndex;


    public override void Awake()
    {
        base.Awake();
        mImgLeft = mPanelGo.transform.Find("Img_BGLeft").GetComponent<Image>();
        mImgRight = mPanelGo.transform.Find("Img_BGRight").GetComponent<Image>();
        mBtnStart = mPanelGo.transform.Find("Btn_Start").GetComponent<Button>(); 
        mImgLockTr = mPanelGo.transform.Find("Img_LockBtn").GetComponent<Transform>();
        mEmpTowerTr = mPanelGo.transform.Find("Emp_Tower").GetComponent<Transform>();
        mWaveTxt = mPanelGo.transform.Find("Img_TotalWaves/Text").GetComponent<Text>();

        mScrollRectEx = mPanelGo.transform.Find("ScrollView").GetComponent<ScrollRectExtension>();
        mContentTr = mPanelGo.transform.Find("ScrollView/Viewport/Content").GetComponent<Transform>();

        mItemGoDic = new Dictionary<string, GameObject>();
        mTowerGoDic = new Dictionary<string, GameObject>();

        mPlayerMgr = mUIFacade.mPlayerMgr;
        PreloadRes();
        mScrollRectEx.AddScrollValueChangedAction(OnScrollValueChg);
        mCurPageIndex = 1;

        AddBtnsClickListener();
    }

    private void OnScrollValueChg(Vector2 vec2) {
        if (mCurPageIndex != mScrollRectEx.CurItemIndex) {
            mCurPageIndex = mScrollRectEx.CurItemIndex;
            StartCoroutine(UpdatePanelCoroutine());
        }
    }

    IEnumerator UpdatePanelCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
            
        UpdatePanel(mCurPageIndex);
    }

    private void PreloadRes() {
        mUIFacade.GetSprite(PathConfig.Sprite_GameOption_Normal_Level + "AllClear");
        for (int i = 1; i <= 3; i++)
        {
            mUIFacade.GetSprite(PathConfig.Sprite_GameOption_Normal_Level + "Carrot_" + i);
        }
        for (int i = 1; i <= mPlayerMgr.maxBigLevel; i++)
        {
            string levelSpritePath = PathConfig.Sprite_GameOption_Normal_Level + i.ToString() + "/";
            mUIFacade.GetSprite(levelSpritePath + "BG_Left");
            mUIFacade.GetSprite(levelSpritePath + "BG_Right");
            mUIFacade.GetSprite(levelSpritePath + "Level_" + i);
        }

        string towerPath = PathConfig.Sprite_GameOption_Normal_Level + "Tower/";
        for (int i = 1; i <= mPlayerMgr.maxTowerCount; i++)
        {
            mUIFacade.GetSprite(towerPath + "Tower_" + i);
        }
    }

    public void EnterPanel(int bigId, int totalnum) {
        this.mBigId = bigId;
        this.mTotalNum = totalnum;
        __Enter();
        UpdateItem();
        UpdatePanel(mCurPageIndex);
    } 

    public override void __Enter()
    {
        mPanelGo.SetActive(true);
    }

    /// <summary>
    /// 初始化更新item数据， 打开界面时调用，（或者item数据有变化时，但是本项目的item在进入界面后的数据都是固定的）
    /// </summary>
    private void UpdateItem() {
        if (mBigId <= 0) {
            return;
        }
        ClearItemGoList();
        int curLevelItemCount = mPlayerMgr.bigIDLevelNumDic[mBigId];
        for (int i = 1; i <= curLevelItemCount; i++)
        { 
            string levelSpritePath = PathConfig.Sprite_GameOption_Normal_Level + mBigId.ToString() + "/";
            mImgLeft.sprite = mUIFacade.GetSprite(levelSpritePath + "BG_Left");
            mImgRight.sprite = mUIFacade.GetSprite(levelSpritePath + "BG_Right");

            string itemPrefabName = "Img_Level";
            GameObject item = mUIFacade.GetItem(FactoryType.UI, itemPrefabName, mContentTr);
            if (item) {
                UILevelItem itemClass = item.GetComponent<UILevelItem>();
                if (itemClass == null) {
                    itemClass = item.AddComponent<UILevelItem>();
                }
                mItemGoDic.Add(itemPrefabName+i.ToString(), item);

                int stageIndex = (mBigId - 1) * curLevelItemCount + (i - 1);
                Stage _stage = mPlayerMgr.levelStageList[stageIndex]; 
                itemClass.UpdateData(_stage, mUIFacade);
            }  
        }

        //设置scroll的content长度
        mScrollRectEx.UpdateScrollView();
    }

    /// <summary>
    /// 回收item，每次打开界面时调用
    /// </summary>
    private void ClearItemGoList() {
        if (mItemGoDic.Count > 0) {
            foreach (string name in mItemGoDic.Keys)
            {
                GameObject go = mItemGoDic[name];
                mUIFacade.PushItem(FactoryType.UI, "Img_Level", go);
            }
            mItemGoDic.Clear();
        }
    }

    /// <summary>
    /// 滑动到不同页面时更新ui
    /// </summary>
    /// <param name="pageIndex">第几页</param>
    private void UpdatePanel(int pageIndex) {
        if (mTowerGoDic.Count > 0) {
            foreach (var name in mTowerGoDic.Keys)
            {
                GameObject go = mTowerGoDic[name];
                mUIFacade.PushItem(FactoryType.UI, "Img_Tower", go);
            }
            mTowerGoDic.Clear();
        }

        int stageIndex = (mBigId - 1) * mPlayerMgr.bigIDLevelNumDic[mBigId] + (pageIndex - 1);
        Stage _stage = mPlayerMgr.levelStageList[stageIndex];

        mWaveTxt.text = _stage.mTotalRound.ToString();
        int towerCount = _stage.mTowerIDListLength; 
        for (int j = 1; j <= towerCount; j++)
        {
            string towerPrefabName = "Img_Tower";
            GameObject towerItem = mUIFacade.GetItem(FactoryType.UI, towerPrefabName, mEmpTowerTr);
            if (towerItem)
            { 
                mTowerGoDic.Add(towerPrefabName + j.ToString(), towerItem);
                string towerImgPath = PathConfig.Sprite_GameOption_Normal_Level + "Tower/Tower_" + j.ToString();
                towerItem.GetComponent<Image>().sprite = mUIFacade.GetSprite(towerImgPath);
            }
        }
        if (_stage.unLocked)
        {
            mImgLockTr.gameObject.SetActive(false);
        }
        else
        {
            mImgLockTr.gameObject.SetActive(true);
        }
    }


    private void AddBtnsClickListener()
    {
        mBtnStart.onClick.AddListener(OnBtnStart); 
    }

    /// <summary>
    /// 点击开始按钮需要传递的数据
    /// </summary>
    private void OnBtnStart() {

    }


    public override void __Close()
    {
        mPanelGo.SetActive(false);
    } 
}
