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

    //当前关卡所有怪物的动画控制器
    public RuntimeAnimatorController[] mMonsterAnimatorArray; 
    //当前目标
    public Transform mCurTargetTr; 
    //当前清理道具的数量
    public int mCurClearItemNum; 
    //当前选择的鸽子
    public GridPoint mCurSelectGrid; 
    //key:塔id， value：价格
    public Dictionary<int, int> mTowerPriceDic; 
    //建造塔界面
    private GameObject mTowerBuildGo; 
    //升级塔界面
    private GameObject mTowerLevelUpGo;
    private Transform mTowerLevelUpBtnTr;
    private Transform mTowerLevelUpSellBtnTr;
    private Vector3 mLevelUpBtnInitPos;
    private Vector3 mSellBtnInitPos;
    //当前指定目标的信号
    public GameObject mCurTargetSignalGo;


    //是否继续生成怪物
    public bool mIsCreatingMonster; 
    //游戏是否结束
    public bool mIsGameOver;
    //游戏是否暂停
    public bool mIsGamePause;
    //当前游戏速度
    public int mCurGameSpeed;
    //萝卜的血量
    public int mCarrotHP;
    //当前杀死的怪物数量
    public int mCurKillMonsterNum;
    //当前玩家的金币
    public int mCurCoinNum; 
    //玩家的所有杀怪记录
    public int mTotalKillMonsterNum; 

    public MonsterBuilder mMonsterBuilder;
    public TowerBuilder mTowerBuilder;

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
        mCurStage = new Stage(3, false, 1, 1, 1, true);

        mTowerPriceDic = new Dictionary<int, int>
        {
            { 1, 100},
            { 2, 120},
            { 3, 140},
            { 4, 160},
            { 5, 180},
        };

        mMonsterAnimatorArray = new RuntimeAnimatorController[mGameMgr.playerMgr.curMaxMonsterNum];
        for (int i = 0; i < mMonsterAnimatorArray.Length; i++)
        {
            mMonsterAnimatorArray[i] = GetRuntimeAnimCtrl("Monster/" + mBigId.ToString() + "/" + (i + 1).ToString());
        }

        mTowerBuildGo = transform.Find("TowerList").gameObject;
        mTowerLevelUpGo = transform.Find("HandleTowerCanvas").gameObject;
        mTowerLevelUpBtnTr = mTowerLevelUpGo.transform.Find("Btn_UpLevel");
        mTowerLevelUpSellBtnTr = mTowerLevelUpGo.transform.Find("Btn_SellTower");
        mLevelUpBtnInitPos = mTowerLevelUpBtnTr.localPosition;
        mSellBtnInitPos = mTowerLevelUpSellBtnTr.localPosition;

        mCurTargetSignalGo = transform.Find("TargetSignal").gameObject; 

        mIsGamePause = false;
        mCurGameSpeed = 1;
        mMonsterBuilder = new MonsterBuilder();
        mTowerBuilder = new TowerBuilder();
        mCurLevel = new Level(mMapMaker.mRoundInfoList.Count, mMapMaker.mRoundInfoList);
        mCurLevel.HandleRound();


        BuildTower();
#endif
    }


    private void Update()
    {
#if Game
        if (!mIsGamePause)
        {
            if (mCurKillMonsterNum >= mCurMonsterIdList.Count)
            {
                //当前波次怪物全部消灭了，进行下一波 
                AddRound();
            }
            else {
                if (!mIsCreatingMonster) {
                    mIsCreatingMonster = true;
                    CreateMonster();
                }
            } 
        }
        else {
            mIsCreatingMonster = false;
            StopCreateMonster();
        } 
#endif
    }

    public void ChangeCoin(int coin) {
        mCurCoinNum += coin;
    }

    #region
    public void ShowSignal()
    {
        mCurTargetSignalGo.transform.position = mCurTargetTr.position + new Vector3(0, mMapMaker.GridHeight / 2, 0);
        mCurTargetSignalGo.transform.SetParent(mCurTargetTr);
        mCurTargetSignalGo.SetActive(true);
    }

    public void HideSignal()
    {
        mCurTargetSignalGo.gameObject.SetActive(false);
        mCurTargetTr = null;
    }
    #endregion

    #region  建塔处理
    public void BuildTower() {
        for (int i = 0; i < mCurStage.mTowerIDList.Length; i++)
        {
            GameObject btnTowerGo = mGameMgr.uiMgr.mUIFacade.GetItem(FactoryType.UI, "Btn_Tower", mTowerBuildGo.transform);
            BtnTower btnTower = btnTowerGo.GetComponent<BtnTower>();
            if (btnTower == null) {
                btnTower = btnTowerGo.AddComponent<BtnTower>();
            }
            btnTower.mTowerID = mCurStage.mTowerIDList[i]; 

            //todo
        }
    }
    public void SetTowerBuildGoState(GridPoint grid, bool isActive) {
        mTowerBuildGo.transform.localPosition = AdjustTowerBuildGoPos(grid);
        mTowerBuildGo.SetActive(isActive);
    }

    public void ChangeTowerBuildGoActiveState(bool isActive) {
        mTowerBuildGo.SetActive(isActive);
    }

    public void SetTowerLevelUpGoState(GridPoint grid, bool isActive) {
        mTowerLevelUpGo.transform.localPosition = grid.transform.position;
        AdjustTowerLevelUpGoPos(grid);
        mTowerLevelUpGo.SetActive(isActive);
    }

    public void ChangeLevelUpGoActiveState(bool isActive) {
        mTowerLevelUpGo.SetActive(isActive);
    }

    private Vector3 AdjustTowerBuildGoPos(GridPoint grid) {
        Vector3 adjustPos = grid.transform.localPosition;
        if (grid.mGridIndex.x <= 3 && grid.mGridIndex.x >= 0)
        { 
            adjustPos += new Vector3(mMapMaker.GridWidth, 0, 0);
        }
        else if (grid.mGridIndex.x <= 11 && grid.mGridIndex.x >= 8)
        {
            adjustPos -= new Vector3(mMapMaker.GridWidth, 0, 0);
        }
        if (grid.mGridIndex.y <= 3 && grid.mGridIndex.y >= 0)
        {
            adjustPos += new Vector3(0, mMapMaker.GridHeight, 0);
        }
        else if (grid.mGridIndex.y <= 7 && grid.mGridIndex.y >= 4)
        {
            adjustPos -= new Vector3(0, mMapMaker.GridHeight, 0);
        }
        adjustPos += transform.position;
        return adjustPos;
    }
    private void AdjustTowerLevelUpGoPos(GridPoint grid) {
        mTowerLevelUpBtnTr.localPosition = Vector3.zero;
        mTowerLevelUpSellBtnTr.localPosition = Vector3.zero;
        if (grid.mGridIndex.y <= 0)
        {
            if (grid.mGridIndex.x == 0)
            {
                mTowerLevelUpSellBtnTr.position += new Vector3(mMapMaker.GridWidth * 3 / 4, 0, 0);
            }
            else
            {
                mTowerLevelUpSellBtnTr.position -= new Vector3(mMapMaker.GridWidth * 3 / 4, 0, 0);
            }
            mTowerLevelUpBtnTr.localPosition = mLevelUpBtnInitPos;
        }
        else if (grid.mGridIndex.y >= 6)
        {
            if (grid.mGridIndex.x == 0)
            {
                mTowerLevelUpBtnTr.position += new Vector3(mMapMaker.GridWidth * 3 / 4, 0, 0);
            }
            else
            {
                mTowerLevelUpBtnTr.position -= new Vector3(mMapMaker.GridWidth * 3 / 4, 0, 0);
            }
            mTowerLevelUpSellBtnTr.localPosition = mSellBtnInitPos;
        }
        else
        {
            mTowerLevelUpBtnTr.localPosition = mLevelUpBtnInitPos;
            mTowerLevelUpSellBtnTr.localPosition = mSellBtnInitPos;
        }
    }
    #endregion

    #region   格子的处理
    public void HandleGrid(GridPoint grid)//当前选择的格子
    {
        if (grid.mGridState.canBuild)
        {
            if (mCurSelectGrid == null)//没有上一个格子
            {
                mCurSelectGrid = grid;
                mCurSelectGrid.ShowGrid();

            }
            else if (grid == mCurSelectGrid)//选中同一个格子
            {
                grid.HideGrid();
                mCurSelectGrid = null;
            }
            else if (grid != mCurSelectGrid)//选中不同格子
            {
                mCurSelectGrid.HideGrid();
                mCurSelectGrid = grid;
                mCurSelectGrid.ShowGrid();
            }
        }
        else
        {
            grid.HideGrid();
            grid.CanNotBuildTower();
            if (mCurSelectGrid != null)
            {
                mCurSelectGrid.HideGrid();
            }
        }
    }
    #endregion

    #region 有关怪物的逻辑 
    /// <summary>
    /// 生产怪物
    /// </summary>
    public void CreateMonster() {
        mIsCreatingMonster = true;
        InvokeRepeating("InstantiateMonsterGo", 1f / mCurGameSpeed, 1f / mCurGameSpeed);
    }

    private void InstantiateMonsterGo() {
        //生成特效
        GameObject effectGO = GetItem("CreateEffect");
        effectGO.transform.SetParent(this.transform);
        effectGO.transform.localPosition = mMapMaker.mMonsterPosList[0];

        if (mCurMonsterIndex < mCurMonsterIdList.Count) {
            //生成当前关卡的当前波次的怪物列表中的一个，具体是mCurMonsterIndex对应的那个
            RoundInfo _rInfo = mCurLevel.mRoundArray[mCurLevel.mCurRoundIndex].mRoundInfo;
            mMonsterBuilder.mMonsterId = _rInfo.MonsterIdList[mCurMonsterIndex];
        }

        GameObject monsterGo = mMonsterBuilder.GetProduct();
        monsterGo.transform.SetParent(transform);
        monsterGo.transform.localPosition = mMapMaker.mMonsterPosList[0];
        mCurMonsterIndex++;
        if (mCurMonsterIndex >= mCurMonsterIdList.Count) {
            StopCreateMonster();
        } 
    }

    private void StopCreateMonster() {
        CancelInvoke();
    }

    private void AddRound() {
        mCurKillMonsterNum = 0;
        mCurMonsterIndex = 0;
        mCurLevel.AddUpRoundIndex();
        mCurLevel.HandleRound();
    }

    #endregion

    #region  资源获取
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
    #endregion
}
