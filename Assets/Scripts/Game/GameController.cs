using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController :  MonoBehaviour
{
    private static GameController _instance;

    public static GameController Instance {
        get {
            return _instance;
        }
    }
    [HideInInspector]
    public GameManager mGameMgr;
    [HideInInspector]
    public MapMaker mMapMaker;

    private Stage mCurStage;
    public Stage CurStage {
        get {
            return mCurStage;
        }
        set {
            mCurStage = value;
        }
    }

    public int mBigId;
    public int mLevelId;

    private void Awake()
    { 
        _instance = this;
        mBigId = 2;
        mLevelId = 2;
        mGameMgr = GameManager.Instance;
        mMapMaker = transform.GetComponent<MapMaker>();
        mMapMaker.InitMap();
        //int bigId = mCurStage.mBigLevelID;
        //int levelId = mCurStage.mLevelID;
        //mMapMaker.LoadMap(bigId, levelId);
        mMapMaker.LoadMap(mBigId, mLevelId);
    }


    public Sprite GetSprite(string name) {
        return mGameMgr.GetSprite(name);
    }

    public AudioClip GetAudioClip(string name) {
        return mGameMgr.GetAudioClip(name);
    }

    public RuntimeAnimatorController GetRuntimeAnimCtrl(string name) {
        return mGameMgr.GetRuntimeAnimCtrl(name);
    }

    public GameObject GetItem(string name) {
        return mGameMgr.GetItem(FactoryType.Game, name);
    }

    public void PushItem(string name, GameObject go) {
        mGameMgr.PushItem(FactoryType.Game, name, go);
    }

}
