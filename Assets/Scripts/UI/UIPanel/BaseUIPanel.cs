using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUIPanel : MonoBehaviour
{
    public UIFacade mUIFacade;
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

    }
    public virtual void __Close() {
        mPanelGo.SetActive(false); 
    }

    public virtual void __Exit()
    { 
    }

    public virtual void CloseSelf() {
        __Close(); 
    }
     
   
}
