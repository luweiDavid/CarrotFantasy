using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPanel : BaseUIPanel
{
    #region  成员变量
    private GameObject mOptionPageGo;
    private GameObject mStatisticsPageGo;
    private GameObject mProducerPageGo;
    private GameObject mPanelResetGo;

    private Button mBtnReturn;
    private Button mBtnOption;
    private Button mBtnStatistic;
    private Button mBtnProducer; 
    private Button mBtnBGAudio;
    private Button mBtnEffectAudio;
    private Button mBtnReset;
    private Button mBtnClose;
    private Button mBtnCertain;

    private Image mBtnBGAudioImg;
    private Image mBtnEffectAudioImg;
    private Sprite[] mAudioSprites;

    private Text[] mStatisticTexts;

    private bool mIsOpenBgAudio = true;
    private bool mIsOpenEffectAudio = true;

    #endregion

    public override void Awake()
    {
        base.Awake();
    }

    public override void __Init()
    {
        base.__Init();

        mOptionPageGo = mPanelGo.transform.Find("OptionPage").gameObject;
        mStatisticsPageGo = mPanelGo.transform.Find("StatisticsPage").gameObject;
        mProducerPageGo = mPanelGo.transform.Find("ProducerPage").gameObject;
        mPanelResetGo = mPanelGo.transform.Find("Panel_Reset").gameObject;

        mBtnReturn = mPanelGo.transform.Find("Btn_Return").GetComponent<Button>();
        mBtnOption = mPanelGo.transform.Find("Btn_Option").GetComponent<Button>();
        mBtnStatistic = mPanelGo.transform.Find("Btn_Statistics").GetComponent<Button>();
        mBtnProducer = mPanelGo.transform.Find("Btn_Producer").GetComponent<Button>();
        mBtnBGAudio = mPanelGo.transform.Find("OptionPage/Btn_BGAudio").GetComponent<Button>();
        mBtnEffectAudio = mPanelGo.transform.Find("OptionPage/Btn_EffectAudio").GetComponent<Button>();
        mBtnReset = mPanelGo.transform.Find("OptionPage/Btn_Reset").GetComponent<Button>();
        mBtnClose = mPanelGo.transform.Find("Panel_Reset/Btn_Certain").GetComponent<Button>();
        mBtnCertain = mPanelGo.transform.Find("Panel_Reset/Btn_Close").GetComponent<Button>();

        mBtnBGAudioImg = mPanelGo.transform.Find("OptionPage/Btn_BGAudio").GetComponent<Image>();
        mBtnEffectAudioImg = mPanelGo.transform.Find("OptionPage/Btn_EffectAudio").GetComponent<Image>();

        AddBtnsClickListener();

        mAudioSprites = new Sprite[4];
        mAudioSprites[0] = mUIFacade.GetSprite(PathConfig.Sprite_SetPanel_OptionPage + "setting02-hd_15");
        mAudioSprites[1] = mUIFacade.GetSprite(PathConfig.Sprite_SetPanel_OptionPage + "setting02-hd_21");
        mAudioSprites[2] = mUIFacade.GetSprite(PathConfig.Sprite_SetPanel_OptionPage + "setting02-hd_6");
        mAudioSprites[3] = mUIFacade.GetSprite(PathConfig.Sprite_SetPanel_OptionPage + "setting02-hd_11");


        //天坑在此
        mStatisticTexts = new Text[7];
        for (int i = 1; i <= mStatisticTexts.Length; i++)
        {
            Debug.Log(i);
            string str = "StatisticsPage/Text" + i;
            mStatisticTexts[i] = mPanelGo.transform.Find(str).GetComponent<Text>(); 
            mStatisticTexts[i].text = "0";

            //Debug.Log(mStatisticTexts[i] + "   --");
        }

    }
    public override void __Enter()
    {
        base.__Enter();
        OnBtnOption();
    }

    public override void __Update()
    {
        base.__Update();
    }

    private void AddBtnsClickListener() {
        mBtnReturn.onClick.AddListener(OnBtnReturn);
        mBtnOption.onClick.AddListener(OnBtnOption);
        mBtnStatistic.onClick.AddListener(OnBtnStatistic);
        mBtnProducer.onClick.AddListener(OnBtnProducer);
        mBtnBGAudio.onClick.AddListener(OnBtnBGAudio);
        mBtnEffectAudio.onClick.AddListener(OnBtnEffectAudio);
        mBtnReset.onClick.AddListener(OnBtnReset);
        mBtnClose.onClick.AddListener(OnBtnReset);
        mBtnCertain.onClick.AddListener(OnBtnReset);
    }

    private void OnBtnReturn()
    {
        CloseSelf();
        //mUIFacade.OpenPanel(NameConfig.PanelName_Main);
    }
    private void OnBtnOption()
    {
        mOptionPageGo.SetActive(true);
        mStatisticsPageGo.SetActive(false);
        mProducerPageGo.SetActive(false);
    }
    private void OnBtnStatistic()
    {
        mOptionPageGo.SetActive(false);
        mStatisticsPageGo.SetActive(true);
        mProducerPageGo.SetActive(false);
    }
    private void OnBtnProducer()
    {
        mOptionPageGo.SetActive(false);
        mStatisticsPageGo.SetActive(false);
        mProducerPageGo.SetActive(true);
    }
    private void OnBtnBGAudio()
    {
        mIsOpenBgAudio = !mIsOpenBgAudio;
        mUIFacade.SetBgAudio(mIsOpenBgAudio);
        if (mIsOpenBgAudio) {
            mBtnBGAudioImg.sprite = mAudioSprites[0];
        }
        else {
            mBtnBGAudioImg.sprite = mAudioSprites[1];
        } 
    }
    private void OnBtnEffectAudio()
    {
        mIsOpenEffectAudio = !mIsOpenEffectAudio;
        mUIFacade.SetEffectAudio(mIsOpenEffectAudio);
        if (mIsOpenEffectAudio)
        {
            mBtnEffectAudioImg.sprite = mAudioSprites[2];
        }
        else
        {
            mBtnEffectAudioImg.sprite = mAudioSprites[3];
        }
    }
    private void OnBtnReset() {
        //mPanelResetGo.SetActive(true);

    }
    private void OnBtnClose()
    {
        //mPanelResetGo.SetActive(false);

    }
    private void OnBtnCertain()
    {
        

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
