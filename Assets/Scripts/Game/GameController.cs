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
    [HideInInspector]
    public int mBigId;
    [HideInInspector]
    public int mLevelId;

    public Level mCurLevel;
    public List<int> mCurMonsterIdList;
    public int mCurMonsterIndex;
    public RuntimeAnimatorController[] mMonsterAnimatorArray;




    public int curKillMonsterNum;
    public int mCarrotHP;
    public int mCurGameSpeed;
    public bool mIsGamePause;

    public Transform mCurTargetTr;
    public GameObject mCurTargetSignal;
    public int mCurClearItemNum;

    public MonsterBuilder mMonsterBuilder;
    public GridPoint mCurSelectGrid;

    //key:塔id， value：价格
    public Dictionary<int, int> mTowerPriceDic;

    public GameObject mTowerBuildGo;

    public GameObject mTowerLevelUpGo;

    public bool mIsCreatingMonster;
    public bool mIsGameOver;

     

    private void Awake()
    {
#if Game
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

        mMonsterAnimatorArray = new RuntimeAnimatorController[mGameMgr.playerMgr.curMaxMonsterNum];
        for (int i = 0; i < mMonsterAnimatorArray.Length; i++)
        {
            mMonsterAnimatorArray[i] = GetRuntimeAnimCtrl("Monster/" + mBigId.ToString() + "/" + (i + 1).ToString());
        }
#endif
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
