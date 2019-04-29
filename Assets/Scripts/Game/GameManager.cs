using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public Transform GameObjectPoolTr;

    public PlayerManager playerMgr;
    public FactoryManager factoryMgr;
    public AudioManager audioMgr;
    public UIManager uiMgr;

    public void Awake()
    {
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
        GameObjectPoolTr = transform.Find("GameObjectPool").GetComponent<Transform>();

        playerMgr = new PlayerManager();
        factoryMgr = new FactoryManager();
        audioMgr = new AudioManager();
        uiMgr = new UIManager();

        audioMgr.PlayBgAudio(GetAudioClip(PathConfig.Audio_Main));
    }

    
    /// <summary>
    /// 获取未实例化的prefab资源
    /// </summary> 
    public GameObject GetItem(FactoryType type, string path) {
        BaseFactory factory = factoryMgr.mFactoryDic[type];
        return factory.GetItem(path);
    }

    public void PushItem(FactoryType type, string path, GameObject item) {
        BaseFactory factory = factoryMgr.mFactoryDic[type];
        if (item != null) {
            factory.PushItem(path, item);
        }
    }


    public Sprite GetSprite(string spritePath)
    {
        return factoryMgr.mSpriteFactory.GetRes(spritePath);
    }

    public AudioClip GetAudioClip(string audioPath)
    {
        return factoryMgr.mAudioClipFactory.GetRes(audioPath);
    }

    public RuntimeAnimatorController GetRuntimeAnimCtrl(string animPath)
    {
        return factoryMgr.mRuntimeAnimatorCtrlFactory.GetRes(animPath);
    }

    public void SetBgAudio(bool isOpen) {
        audioMgr.SetBgAudio(isOpen);
    }

    public void SetEffectAudio(bool isOpen) {
        audioMgr.SetEffectAudio(isOpen);
    }

}
