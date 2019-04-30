using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    private UIFacade mUIFacade;
    private GameObject imgBgGo;
    private Image imgMonster;

    private GameObject imgLockGo;
    private GameObject imgAllClearGo;
    private Image imgCarrot;

    private void Awake()
    {
        imgBgGo = transform.Find("Img_BG").gameObject;
        imgMonster = transform.Find("Img_BG/Img_Monster").GetComponent<Image>();

        imgLockGo = transform.Find("Img_Lock").gameObject;
        imgAllClearGo = transform.Find("Img_AllClear").gameObject;
        imgCarrot = transform.Find("Img_Carrot").GetComponent<Image>();
    }

    public void UpdateData(Stage _stage, UIFacade facade) {
        if (_stage.mLevelID <= 0) {
            return;
        }
        mUIFacade = facade;

        imgCarrot.gameObject.SetActive(false);
        imgAllClearGo.SetActive(false);
        if (_stage.unLocked)
        {
            if (_stage.mAllClear) {
                imgAllClearGo.SetActive(true); 
            }
            if (_stage.mCarrotState > 0) {
                imgCarrot.gameObject.SetActive(true);
                imgCarrot.sprite = mUIFacade.GetSprite(PathConfig.Sprite_GameOption_Normal_Level + "Carrot_" + _stage.mCarrotState);
            }
            imgLockGo.SetActive(false);
            imgBgGo.SetActive(false);
        }
        else {
            if (_stage.mIsRewardLevel)
            {
                imgLockGo.SetActive(false);
                imgBgGo.SetActive(true);
                imgMonster.sprite = mUIFacade.GetSprite(PathConfig.Sprite_MonsterNest_Monster_Baby + _stage.mBigLevelID.ToString());
                imgMonster.SetNativeSize();
                imgMonster.transform.localScale = new Vector3(2, 2, 2);
            }
            else {
                imgLockGo.SetActive(true);
                imgBgGo.SetActive(false);
            }
        }
    }
}
