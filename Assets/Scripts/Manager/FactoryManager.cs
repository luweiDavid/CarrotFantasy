using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager 
{
    public Dictionary<FactoryType, BaseFactory> mFactoryDic;

    public SpriteFactory mSpriteFactory;
    public AudioClipFactory mAudioClipFactory;
    public RuntimeAnimatorCtrlFactory mRuntimeAnimatorCtrlFactory;

    public FactoryManager()
    {

        mFactoryDic = new Dictionary<FactoryType, BaseFactory>();
        mFactoryDic.Add(FactoryType.UI, new UIFactory());
        mFactoryDic.Add(FactoryType.UIPanel, new UIPanelFactory());
        mFactoryDic.Add(FactoryType.Game, new GameFactory()); 

        mSpriteFactory = new SpriteFactory();
        mAudioClipFactory = new AudioClipFactory();
        mRuntimeAnimatorCtrlFactory = new RuntimeAnimatorCtrlFactory();
    }
}
