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

    #region
    /// <summary>
    /// 脚本刚创建时，用于组件获取，事件监听，资源获取保存等
    /// 侧重一次获取
    /// </summary>
    public virtual void Awake()
    {
        mUIFacade = GameManager.Instance.uiMgr.mUIFacade;
        mPanelGo = this.gameObject;
    }

    /// <summary>
    /// 每次添加面板到某个场景时，需要初始化的数据
    /// 有些架构是在OnDisable中重置数据
    /// </summary>
    public virtual void __Init() {  }

    /// <summary>
    /// 进入面板，显示面板 
    /// </summary>
    public virtual void __Enter()
    {
        mPanelGo.SetActive(true);
        //最好设置在动画播放完毕后才可以点击面板，不然可能带来未知的bug  TODO   
        transform.localScale = Vector3.zero;
        DOTween.To(() => transform.localScale, s => transform.localScale = s, Vector3.one, mTweenTime);
    }
    /// <summary>
    /// 面板更新
    /// </summary>
    public virtual void __Update(){ }
      
    /// <summary>
    /// 面板关闭时
    /// </summary>
    public virtual void __Close() { 
        DOTween.To(() => transform.localScale, s => transform.localScale = s, Vector3.zero, mTweenTime).
           OnComplete(() =>
           {
               mPanelGo.SetActive(false);
           });
    }

    public virtual void __Destroy()
    {
        
    }
    #endregion

    #region  一些封装方法 
    public virtual void CloseSelf() {
        __Close(); 
    }

    #endregion


}
