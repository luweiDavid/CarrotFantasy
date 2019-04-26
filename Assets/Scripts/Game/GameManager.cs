using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [HideInInspector]
    public Transform GameObjectPoolTr;

    public PlayerManager playerMgr;
    public FactoryManager factoryMgr;
    public AudioManager audioMgr;
    public UIManager uiMgr;

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        GameObjectPoolTr = transform.Find("GameObjectPool").GetComponent<Transform>();

        playerMgr = new PlayerManager();
        factoryMgr = new FactoryManager();
        audioMgr = new AudioManager();
        uiMgr = new UIManager();  
    }

    public Sprite GetSprite(string spritePath) {
        return factoryMgr.mSpriteFactory.GetRes(spritePath);
    }

    public GameObject GetUIPanelGo(string path) {
        UIPanelFactory panelFac = factoryMgr.mFactoryDic[FactoryType.UIPanel] as UIPanelFactory;
        return panelFac.GetItem(path);
    }

    public GameObject GetUIGo(string path) {  
        UIFactory uiFac = factoryMgr.mFactoryDic[FactoryType.UI] as UIFactory; 
        return uiFac.GetItem(path);
    }

    public GameObject ThisGo() {
        return this.gameObject;
    }
}
