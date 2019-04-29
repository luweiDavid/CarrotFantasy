using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanel : BaseUIPanel
{
    private GameObject mHelpPageGo;
    private GameObject mMonsterPageGo;
    private GameObject mTowerPageGo;

    private Button mBtnReturn;
    private Button mBtnHelp;
    private Button mBtnMonster;
    private Button mBtnTower;

    private ScrollRectExtension mhelpPageScroll;
    private Text mhelpPageTxt;

    private ScrollRectExtension mtowerPageScroll;
    private Text mtowerPageTxt;

    public override void Awake()
    {
        base.Awake();
    }

    public override void __Init()
    {
        base.__Init();

        mHelpPageGo = mPanelGo.transform.Find("HelpPage").gameObject;
        mMonsterPageGo = mPanelGo.transform.Find("MonsterPage").gameObject;
        mTowerPageGo = mPanelGo.transform.Find("TowerPage").gameObject;

        mBtnReturn = mPanelGo.transform.Find("Btn_Return").GetComponent<Button>();
        mBtnHelp = mPanelGo.transform.Find("Btn_Help").GetComponent<Button>();
        mBtnMonster = mPanelGo.transform.Find("Btn_Monster").GetComponent<Button>();
        mBtnTower = mPanelGo.transform.Find("Btn_Tower").GetComponent<Button>();

        mhelpPageScroll = mPanelGo.transform.Find("HelpPage/ScrollView").GetComponentInChildren<ScrollRectExtension>(true);
        mhelpPageScroll.AddScrollValueChangedAction(OnHelpPageScrollValueChg);
        mhelpPageTxt = mPanelGo.transform.Find("HelpPage/Img_Page/Text").GetComponent<Text>();

        mtowerPageScroll = mPanelGo.transform.Find("TowerPage/ScrollView").GetComponentInChildren<ScrollRectExtension>(true);
        mtowerPageScroll.AddScrollValueChangedAction(OnTowerPageScrollValueChg); 
        mtowerPageTxt = mPanelGo.transform.Find("TowerPage/Img_Page/Text").GetComponent<Text>(); 
        AddBtnsClickListener(); 
    }
    public override void __Enter()
    {
        base.__Enter();
        
        OnBtnHelp();
    }

    public override void __Update()
    {
        base.__Update();
    }

    private void AddBtnsClickListener()
    {
        mBtnReturn.onClick.AddListener(OnBtnReturn);
        mBtnHelp.onClick.AddListener(OnBtnHelp);
        mBtnMonster.onClick.AddListener(OnBtnMonster);
        mBtnTower.onClick.AddListener(OnBtnTower);
    }
    private void OnBtnReturn()
    {
        CloseSelf();
        mUIFacade.OpenPanel(NameConfig.PanelName_Main);
    }
    private void OnBtnHelp()
    {
        mHelpPageGo.SetActive(true);
        mMonsterPageGo.SetActive(false);
        mTowerPageGo.SetActive(false);
    }
    private void OnBtnMonster()
    {
        mHelpPageGo.SetActive(false);
        mMonsterPageGo.SetActive(true);
        mTowerPageGo.SetActive(false);
    }
    private void OnBtnTower()
    {
        mHelpPageGo.SetActive(false);
        mMonsterPageGo.SetActive(false);
        mTowerPageGo.SetActive(true);
    }

    private void OnHelpPageScrollValueChg(Vector2 vec2) {
        mhelpPageTxt.text = string.Format("{0}/{1}", mhelpPageScroll.CurItemIndex + 1, mhelpPageScroll.ItemCount);
    }

    private void OnTowerPageScrollValueChg(Vector2 vec2)
    {
        mtowerPageTxt.text = string.Format("{0}/{1}", mtowerPageScroll.CurItemIndex + 1, mtowerPageScroll.ItemCount);
    }


    public override void __Close()
    {
        base.__Close();
    }

    public override void __Exit()
    {
        base.__Exit();
    }
}
