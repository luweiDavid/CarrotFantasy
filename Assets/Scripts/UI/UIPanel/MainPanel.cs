using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BaseUIPanel
{
    private Transform mImgCloudTr;
    private Button mBtnNormal;
    private Button mBtnBoss;
    private Button mBtnMonsterNest;
    private Button mBtnSet;
    private Button mBtnHelp;
    private Button mBtnExitGame;

    private Animator mCarrotAnim; 

    public override void Awake()
    {
        base.Awake();
        mImgCloudTr = mPanelGo.transform.Find("Img_Cloud").GetComponent<Transform>();
        mBtnNormal = mPanelGo.transform.Find("Btn_Normal").GetComponent<Button>();
        mBtnBoss = mPanelGo.transform.Find("Btn_Boss").GetComponent<Button>();
        mBtnMonsterNest = mPanelGo.transform.Find("Btn_MonsterNest").GetComponent<Button>();
        mBtnSet = mPanelGo.transform.Find("Btn_Set").GetComponent<Button>();
        mBtnHelp = mPanelGo.transform.Find("Btn_Help").GetComponent<Button>();
        mBtnExitGame = mPanelGo.transform.Find("Btn_ExitGame").GetComponent<Button>();
        AddBtnsListener();

        mCarrotAnim = mPanelGo.transform.Find("Emp_Carrot").GetComponent<Animator>(); 
    }
    public override void __Init()
    {
        base.__Init();
        mCarrotAnim.Play("CarrotGrow");
    }
    public override void __Enter()
    {
        base.__Enter(); 
    }

    public override void __Update()
    {
        base.__Update();
    }

    private void AddBtnsListener() {
        mBtnNormal.onClick.AddListener(OnBtnNormal);
        mBtnBoss.onClick.AddListener(OnBtnBoss);
        mBtnMonsterNest.onClick.AddListener(OnBtnMonsterNest);
        mBtnSet.onClick.AddListener(OnBtnSet);
        mBtnHelp.onClick.AddListener(OnBtnHelp);
        mBtnExitGame.onClick.AddListener(OnBtnExitGame);
    }

    private void OnBtnNormal() {
        if (mUIFacade.mCurScene.GetType() != typeof(GameNormalOptionSceneState)) {
            mUIFacade.ChangeSceneState(new GameNormalOptionSceneState(mUIFacade));
        }
    }

    private void OnBtnBoss()
    {
        if (mUIFacade.mCurScene.GetType() != typeof(GameBossOptionSceneState)){
            mUIFacade.ChangeSceneState(new GameBossOptionSceneState(mUIFacade));
        }
    }

    private void OnBtnMonsterNest() {


    }

    private void OnBtnSet() {
        CloseSelf();
        mUIFacade.OpenPanel(NameConfig.PanelName_Set);
    }
    private void OnBtnHelp() {
        CloseSelf();
        mUIFacade.OpenPanel(NameConfig.PanelName_Help);

    } 

    private void OnBtnExitGame() {
#if UNITY_EDITOR
        Application.Quit();
#endif
    } 

    public override void __Close()
    {
        base.__Close();
    }
}
