using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNormalOptionPanel : BaseUIPanel
{
    private Button mBtnReturn;
    private Button mBtnHelp;

    private bool mIsInBigLevelPanel = true;
    public bool IsInBigLevelPanel {
        get {
            return mIsInBigLevelPanel;
        }
        set {
            mIsInBigLevelPanel = value;
        }
    }

    public override void Awake()
    {
        base.Awake(); 
    }

    public override void __Init()
    {
        base.__Init();

        mBtnReturn = mPanelGo.transform.Find("Btn_Return").GetComponent<Button>();
        mBtnHelp = mPanelGo.transform.Find("Btn_Help").GetComponent<Button>();

        AddBtnsClickListener();
    }
    public override void __Enter()
    {
        mPanelGo.SetActive(true);
    } 
    private void AddBtnsClickListener()
    {
        mBtnReturn.onClick.AddListener(OnBtnReturn);
        mBtnHelp.onClick.AddListener(OnBtnHelp);
    }
    private void OnBtnReturn()
    {
        if (mIsInBigLevelPanel)
        {
            mUIFacade.ChangeSceneState(new MainSceneState(mUIFacade));
        }
        else {
            mUIFacade.OpenPanel(NameConfig.PanelName_GameNormalBigLevel);
            mUIFacade.ClosePanel(NameConfig.PanelName_GameNormalLevel);
        }
    }

    private void OnBtnHelp()
    {

    } 
    public override void __Close()
    {
        mPanelGo.SetActive(false);
    } 
}
