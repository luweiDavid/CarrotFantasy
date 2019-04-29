using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIPanel : MonoBehaviour
{
    public UIFacade mUIFacade; 
    private float mTweenTime = 0.5f;

    [HideInInspector]
    public GameObject mPanelGo { get; set; }

    public virtual void Awake()
    {
        mUIFacade = GameManager.Instance.uiMgr.mUIFacade;
    }
    public virtual void __Init()
    {
        mPanelGo = this.gameObject;
    }
    public virtual void __Update()
    {
    }
    public virtual void __Enter()
    {
        mPanelGo.SetActive(true);
        //最好设置在动画播放完毕后才可以点击面板，不然可能带来未知的bug  TODO   
        transform.localScale = Vector3.zero;
        DOTween.To(() => transform.localScale, s => transform.localScale = s, Vector3.one, mTweenTime);
    }
    public virtual void __Close() {
        mPanelGo.SetActive(false); 
    }

    public virtual void __Exit()
    {
        DOTween.To(() => transform.localScale, s => transform.localScale = s, Vector3.zero, mTweenTime).
           OnComplete(() =>
           {
               mPanelGo.SetActive(false);
           });
    }

    public virtual void CloseSelf() {
        __Close(); 
    }
     
   
}
