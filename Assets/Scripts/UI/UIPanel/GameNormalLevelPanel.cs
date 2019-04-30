using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNormalLevelPanel : BaseUIPanel
{
    private int mBigId = 0;
    private int mTotalNum = 0;
    private int mLevelId = 0;

    private Image mImgLeft;
    private Image mImgRight;
    private Button mBtnStart; 
    private Transform mImgLockTr;
    private Transform mEmpTowerTr;
    private Text mWaveTxt;

    private ScrollRectExtension mScrollRectEx;
    private Transform mContentTr;

    private List<GameObject> mItemGoList;
    private List<GameObject> mTowerGoList;

    private PlayerManager mPlayerMgr;


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

        mItemGoList = new List<GameObject>();
        mTowerGoList = new List<GameObject>();
        mPlayerMgr = mUIFacade.mPlayerMgr;
        PreloadRes();

    }

    private void PreloadRes() {
        mUIFacade.GetSprite(PathConfig.Sprite_GameOption_Normal_Level + "AllClear");
        for (int i = 1; i < 3; i++)
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
    } 

    public override void __Enter()
    {
        mPanelGo.SetActive(true);

    }

    private void UpdateItem() {
        if (mBigId <= 0) {
            return;
        }
        for (int i = 0; i < mPlayerMgr.bigIDLevelNumDic[mBigId]; i++)
        { 
            string levelSpritePath = PathConfig.Sprite_GameOption_Normal_Level + mBigId.ToString() + "/";
            mImgLeft.sprite = mUIFacade.GetSprite(levelSpritePath + "BG_Left");
            mImgRight.sprite = mUIFacade.GetSprite(levelSpritePath + "BG_Right");

            GameObject item = mUIFacade.GetItem(FactoryType.UI, "Img_Level", mContentTr);
            if (item) { 
                LevelItem itemClass = item.GetComponent<LevelItem>();
                if (itemClass == null) {
                    item.AddComponent<LevelItem>(); 
                }
                mItemGoList.Add(item);

                int stageIndex = (mBigId - 1) * mPlayerMgr.bigIDLevelNumDic[mBigId] + i;
                Stage _stage = mPlayerMgr.levelStageList[stageIndex];

                mWaveTxt.text = _stage.mTotalRound.ToString();
                int towerCount = _stage.mTowerIDListLength;
                for (int j = 0; j < towerCount; j++)
                {
                    GameObject towerItem = mUIFacade.GetItem(FactoryType.UI, "Img_Tower", mEmpTowerTr);
                    if (towerItem) {
                        mTowerGoList.Add(towerItem);
                        //更新tower   TODO
                    }
                }

                if (_stage.unLocked)
                {
                    mImgLockTr.gameObject.SetActive(false);
                }
                else {
                    mImgLockTr.gameObject.SetActive(true);
                }

                itemClass.UpdateData(_stage, mUIFacade);
            }  
        }
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
