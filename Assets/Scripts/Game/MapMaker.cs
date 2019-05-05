using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
#if Tool
    private static MapMaker _instance;
    public static MapMaker Instance
    {
        get
        {
            return _instance;
        }
    }
#endif
    private float mMapWidth;
    private float mMapHeight; 
    private float mGridWidth;
    public float GridWidth {
        get {
            return mGridWidth;
        }
    }
    private float mGridHeight;
    public float GridHeight {
        get {
            return mGridHeight;
        }
    }
    private int mColumn;
    private int mRow;
    //private bool mDrawLine = true; 
    private GameObject mGridPrefab; 

    private SpriteRenderer mBgRenderer;
    private SpriteRenderer mRoadRenderer; 
    [HideInInspector]
    public int mBigLevelId; 
    [HideInInspector]
    public int mLevelId; 
    [HideInInspector]
    public List<GridIndex> mMonsterPointList; 
    private List<Vector2> mMonsterPosList; 
    [HideInInspector]
    public GridPoint[,] mGridPointArray;
    [HideInInspector]
    public List<RoundInfo> mRoundInfoList;

    private GameController mGameCtrl;
    public Carrot mCarrot;

    private void Awake()
    {
#if Tool
        _instance = this;
        mGridPrefab = Resources.Load<GameObject>("Prefabs/Game/Map/Grid");
        InitMap();
        mDrawLine = true;
#endif
    }

    public void InitMap() {
        mGameCtrl = GameController.Instance;
        mColumn = 12;
        mRow = 8; 
        mMonsterPointList = new List<GridIndex>();
        mMonsterPosList = new List<Vector2>();
        mBgRenderer = transform.Find("BG").GetComponent<SpriteRenderer>();
        mRoadRenderer = transform.Find("Road").GetComponent<SpriteRenderer>();

        CalcMapSize();
        InitMapGrid();
    }

    public void InitMapGrid() { 
        mGridPointArray = new GridPoint[mColumn, mRow];

        for (int x = 0; x < mColumn; x++)
        {
            for (int y = 0; y < mRow; y++)
            {
#if Tool
                GameObject grid = GameObject.Instantiate(mGridPrefab);
#endif
#if Game
                GameObject grid = GameController.Instance.GetItem("Map/Grid");
#endif
                grid.transform.SetParent(transform);
                grid.transform.localPosition = GetPos(x * mGridWidth, y * mGridHeight); 
                mGridPointArray[x, y] = grid.GetComponent<GridPoint>();
                mGridPointArray[x, y].mGridIndex.x = x;
                mGridPointArray[x, y].mGridIndex.y = y;
            }
        }
    }

    private Vector2 GetPos(float x, float y) {
        return new Vector2(x - mMapWidth / 2 + mGridWidth / 2, y - mMapHeight / 2 + mGridHeight / 2);
    }
    /// <summary>
    /// 将视口坐标的起点和终点转换为世界坐标
    /// </summary>
    private void CalcMapSize() { 
        Vector2 zeroPos = new Vector2(0, 0);
        Vector2 onePos = new Vector2(1, 1);
        Vector2 startPos = Camera.main.ViewportToWorldPoint(zeroPos);
        Vector2 endPos = Camera.main.ViewportToWorldPoint(onePos); 
            
        mMapWidth = endPos.x - startPos.x;
        mMapHeight = endPos.y - startPos.y;

        mGridWidth = mMapWidth / mColumn;
        mGridHeight = mMapHeight / mRow; 
    }
#if Tool
    private void OnDrawGizmos()
    {
        if (!mDrawLine) { return; }
        CalcMapSize(); 
        for (int i = 0; i <= mRow; i++)
        { 
            Vector2 pos1 = new Vector2(-mMapWidth / 2, (-mMapHeight / 2) + mGridHeight * i);
            Vector2 pos2 = new Vector2(mMapWidth / 2, (-mMapHeight / 2) + mGridHeight * i);
            Gizmos.DrawLine(pos1, pos2);
        }

        for (int i = 0; i <= mColumn; i++)
        { 
            Vector2 pos1 = new Vector2(-mMapWidth / 2 + mGridWidth * i, -mMapHeight / 2);
            Vector2 pos2 = new Vector2(-mMapWidth / 2 + mGridWidth * i, mMapHeight / 2);
            Gizmos.DrawLine(pos1, pos2);
        } 
    }
#endif

    /// <summary>
    /// 清除怪物路径点
    /// </summary>
    public void ClearMonsterPath() {
        mMonsterPosList.Clear();
    }

    /// <summary>
    /// 重置所有格子的状态
    /// </summary>
    public void ResetGrid()
    {
        mMonsterPosList.Clear();
        for (int x = 0; x < mColumn; x++)
        {
            for (int y = 0; y < mRow; y++)
            {
                mGridPointArray[x, y].Init();
            }
        }
    }

    /// <summary>
    /// 设置地图默认状态
    /// </summary>
    public void ResetMap() {
        mBigLevelId = 0;
        mLevelId = 0;
        ResetGrid(); 
        mRoundInfoList.Clear();

        //这里不能直接将renderer的sprite设置为null，
        mBgRenderer.sprite = Resources.Load<Sprite>("Pictures/NormalMordel/Game/1/BG1");
        mRoadRenderer.sprite = Resources.Load<Sprite>("Pictures/NormalMordel/Game/1/Road4");
    }


    public void LoadMap(int bigId, int levelid) {
        mBigLevelId = bigId;
        mLevelId = levelid;

        string jsonName = bigId.ToString() + "_" + levelid.ToString()+".json";
        LevelInfo info = LoadJson(jsonName);
        ReadLevelInfo(info);
        mMonsterPosList = new List<Vector2>();
        for (int i = 0; i < mMonsterPointList.Count; i++)
        {
            Vector2 pos = mGridPointArray[mMonsterPointList[i].x, mMonsterPointList[i].y].transform.position;
            mMonsterPosList.Add(pos);
        }

        GameObject carrotGo = GameController.Instance.GetItem("Carrot");
        mCarrot = carrotGo.GetComponent<Carrot>();
        if (mCarrot == null) {
            mCarrot = carrotGo.AddComponent<Carrot>();
        }
        carrotGo.transform.localPosition = mMonsterPosList[mMonsterPosList.Count - 1];


        GameObject startPointGo = GameController.Instance.GetItem("StartPoint");
        startPointGo.transform.localPosition = mMonsterPosList[0];

    }


#if Tool
    /// <summary>
    /// 根据当前的地图信息创建LevelInfo（关卡信息中间类）
    /// </summary>
    /// <returns></returns>
    private LevelInfo CreateLevelInfo() {
        LevelInfo levelInfo = new LevelInfo();
        levelInfo.BigLevelId = this.mBigLevelId;
        levelInfo.LevelId = this.mLevelId;
        levelInfo.GridStateList = new List<GridState>();
        for (int x = 0; x < mColumn; x++)
        {
            for (int y = 0; y < mRow; y++)
            {
                levelInfo.GridStateList.Add(this.mGridPointArray[x, y].mGridState);
            }
        }
        levelInfo.GridIndexList = new List<GridIndex>();
        for (int i = 0; i < mMonsterPointList.Count; i++)
        {
            levelInfo.GridIndexList.Add(mMonsterPointList[i]);
        }
        levelInfo.RoundInfoList = new List<RoundInfo>();
        for (int i = 0; i < mRoundInfoList.Count; i++)
        {
            levelInfo.RoundInfoList.Add(mRoundInfoList[i]);
        }

        return levelInfo;
    }

    /// <summary>
    /// 将编辑好的关卡信息保存成json文件，命名规则是： Level+大关卡id+_+小关卡id
    /// </summary>
    public void SaveToJson()
    {
        LevelInfo levelInfo = CreateLevelInfo();
        string path = Application.dataPath + "/Resources/Json/Level/";
        path += levelInfo.BigLevelId.ToString() + "_" + levelInfo.LevelId.ToString()+".json";
        string jsonStr = JsonMapper.ToJson(levelInfo);
        StreamWriter sw = new StreamWriter(path);
        sw.Write(jsonStr);
        sw.Close();

        Debug.Log("保存关卡信息的json文件完成， 路径：" + path);
    } 

#endif
    /// <summary>
    /// 加载关卡的json文件
    /// </summary>
    /// <param name="jsonName"></param>
    /// <returns></returns>
    public LevelInfo LoadJson(string jsonName) {
        LevelInfo levelInfo = null;
        string path = Application.dataPath + "/Resources/Json/Level/" + jsonName;
        if (File.Exists(path)) {
            StreamReader sr = new StreamReader(path);
            string jsonStr = sr.ReadToEnd();
            levelInfo = JsonMapper.ToObject<LevelInfo>(jsonStr);
            sr.Close();
            return levelInfo;
        }

        Debug.LogError("加载json文件失败，路径：" + path);
        return null;
    }

    /// <summary>
    /// 读取关卡信息，设置地图
    /// </summary>
    /// <param name="levelInfo"></param>
    public void ReadLevelInfo(LevelInfo levelInfo) {
        if (levelInfo == null)
        {
            Debug.LogError("levelInfo is null");
            return;
        } 
        mBigLevelId = levelInfo.BigLevelId;
        mLevelId = levelInfo.LevelId;

        for (int x = 0; x < mColumn; x++)
        {
            for (int y = 0; y < mRow; y++)
            {
                mGridPointArray[x, y].mGridState = levelInfo.GridStateList[y+x*mRow];
                mGridPointArray[x, y].UpdateInfo();
            }
        }

        mMonsterPointList.Clear();
        for (int i = 0; i < levelInfo.GridIndexList.Count; i++)
        {
            mMonsterPointList.Add(levelInfo.GridIndexList[i]);
        }

        mRoundInfoList = new List<RoundInfo>();
        for (int i = 0; i < levelInfo.RoundInfoList.Count; i++)
        {
            mRoundInfoList.Add(levelInfo.RoundInfoList[i]);
        }

        //设置bg和road
        mBgRenderer.sprite = Resources.Load<Sprite>("Pictures/NormalMordel/Game/" + mBigLevelId.ToString() + "/BG" +
            (mLevelId / 3).ToString());
        mRoadRenderer.sprite = Resources.Load<Sprite>("Pictures/NormalMordel/Game/" + mBigLevelId.ToString() + "/Road" +
            mLevelId.ToString());
    }



}
