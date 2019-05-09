using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 建塔的按钮
/// </summary>
public class BtnTower : MonoBehaviour
{
    public int mTowerID;
    private int mPrice;
    private Button mBtn;
    private Image mImg;
    private Sprite mEnableSprite;
    private Sprite mDisableSprite;

    private GameController mGameCtrl;


    private void OnEnable()
    {
        if (mPrice <= 0)
            return;
        UpdateIcon();
    }
    private void Start()
    {
        mGameCtrl = GameController.Instance;
        mImg = GetComponent<Image>();
        mBtn = GetComponent<Button>();
        mEnableSprite = mGameCtrl.GetSprite("NormalMordel/Game/Tower/" + mTowerID.ToString() + "/CanClick1");
        mDisableSprite = mGameCtrl.GetSprite("NormalMordel/Game/Tower/" + mTowerID.ToString() + "/CanClick0");
        UpdateIcon();
        mPrice = mGameCtrl.mTowerPriceDic[mTowerID];
        mBtn.onClick.AddListener(BuildTower);
    }

    public void BuildTower() {
        //由建塔者去建造新塔
        mGameCtrl.mTowerBuilder.mTowerId = mTowerID;
        mGameCtrl.mTowerBuilder.mTowerLevel = 1;
        GameObject towerGo = mGameCtrl.mTowerBuilder.GetProduct();
        towerGo.transform.SetParent(mGameCtrl.mCurSelectGrid.transform);
        towerGo.transform.position = mGameCtrl.mCurSelectGrid.transform.position;
        //产生特效
        GameObject effectGo = mGameCtrl.GetItem("BuildEffect");
        effectGo.transform.SetParent(mGameCtrl.transform);
        effectGo.transform.position = mGameCtrl.mCurSelectGrid.transform.position;
        //处理格子
        mGameCtrl.mCurSelectGrid.AfterBuildTower();
        mGameCtrl.mCurSelectGrid.HideGrid();
        mGameCtrl.mCurSelectGrid.mHasTower = true;
        mGameCtrl.ChangeCoin(-mPrice);
        //不滞空会出现建完塔直接点击同一个格子不会显示按钮的情况
        mGameCtrl.mCurSelectGrid = null;
        //让操控画布先隐藏一次进行数值切换
        mGameCtrl.ChangeLevelUpGoActiveState(false);
    }

    private void UpdateIcon()
    {
        if (mGameCtrl.mCurCoinNum > mPrice)
        {
            mImg.sprite = mEnableSprite;
            mBtn.interactable = true;
        }
        else
        {
            mImg.sprite = mDisableSprite;
            mBtn.interactable = false;
        }
    }
}
