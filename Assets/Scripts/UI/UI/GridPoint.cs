using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridPoint : MonoBehaviour
{

    private SpriteRenderer mGridRenderer;
    public GridState mGridState;
    public GridIndex mGridIndex;

    private Sprite mGridSprite;
    private Sprite mMonsterSprite;
    private Sprite mStartSprite;    //开始时格子的图片显示
    private Sprite mCantBuildSprite;     //禁止建塔

    private GameObject[] mGridItemPrefabArray;
    private GameObject mCurItem;

    private string mItemPath = "Map/Item/";
    public bool mHasTower;

    private GameController mGameCtrl;

    private void Awake()
    {
        mGameCtrl = GameController.Instance;
        mGridRenderer = GetComponent<SpriteRenderer>();

        mGridSprite = Resources.Load<Sprite>("Pictures/NormalMordel/Game/Grid");
        mMonsterSprite= Resources.Load<Sprite>("Pictures/NormalMordel/Game/startPoint");
#if Tool
        mGridItemPrefabArray = new GameObject[10];
        string tmpPath = "Prefabs/Game/Map/Item/";
        tmpPath += MapMaker.Instance.mBigLevelId.ToString();
        for (int i = 0; i < mGridItemPrefabArray.Length; i++)
        {
            mGridItemPrefabArray[i] = Resources.Load<GameObject>(tmpPath + i.ToString());
        }
#endif
#if Game
        mStartSprite = mGameCtrl.GetSprite("NormalMordel/Game/StartSprite");
        mCantBuildSprite = mGameCtrl.GetSprite("NormalMordel/Game/cantBuild");
        mGridRenderer.sprite = mStartSprite;
        Tween t = DOTween.To(() => mGridRenderer.color, toColor => mGridRenderer.color = toColor, new Color(1, 1, 1, 0.2f), 3);
        t.OnComplete(ResetGridSprite);
#endif

        Init();
    } 

    public void Init() { 
        mGridState.canBuild = true;
        mGridState.hasItem = false;
        mGridState.itemId = -1;
        mGridState.isMonsterPoint = false;
        mGridRenderer.enabled = true;
#if Tool 
        if (mCurItem) { Destroy(mCurItem); }
        mGridRenderer.sprite = mGridSprite; 
#endif
#if Game
        if (mCurItem) {
            string path = mItemPath + mGameCtrl.mBigId.ToString() + mGridState.itemId.ToString();
            mGameCtrl.PushItem(path, mCurItem);
        }
#endif
    }

#if Game
    public void UpdateInfo() {
        if (mGridState.canBuild)
        {
            mGridRenderer.enabled = true;
            mGridRenderer.sprite = mGridSprite;
            if (mGridState.hasItem)
            { 
                CreateItem();
            }
        }
        else {
            if (mGridState.isMonsterPoint)
            {
                mGridRenderer.sprite = mMonsterSprite;
            }
            else
            {
                mGridRenderer.enabled = false;
            }
        }
    }

    private void CreateItem()
    {
        Vector3 createPos = transform.position;
        if (mGridState.itemType == ItemType.Four)
        {
            createPos += new Vector3(mGameCtrl.mMapMaker.GridWidth / 2, -mGameCtrl.mMapMaker.GridHeight / 2);
        }
        else if (mGridState.itemType == ItemType.Two)
        {
            createPos += new Vector3(mGameCtrl.mMapMaker.GridWidth / 2, 0);
        }
        string path = mItemPath + mGameCtrl.mBigId.ToString() + mGridState.itemId.ToString();
        mCurItem = mGameCtrl.GetItem(path); 
        mCurItem.transform.localPosition = createPos;

        Item itemClass = mCurItem.GetComponent<Item>();
        if (itemClass == null) {
            itemClass = mCurItem.AddComponent<Item>();
        }
        itemClass.Init(this, mGridState.itemId); 
    }
#endif

#if Tool
    public void UpdateInfo()
    {
        if (mGridState.canBuild)
        {
            mGridRenderer.enabled = true;
            mGridRenderer.sprite = mGridSprite;
            if (mGridState.hasItem) {
                CreateItem();
            }
        }
        else
        {
            if (mGridState.isMonsterPoint)
            {
                mGridRenderer.sprite = mMonsterSprite;
            }
            else {
                mGridRenderer.enabled = false;
            } 
        }
    }

    //OnMouse系列方法只能检测鼠标左键， 右键和滑动是不会检测的
    private void OnMouseDown()
    { 
        if (Input.GetKey(KeyCode.P)) { //构建怪物的路径
            mGridState.canBuild = false;
            mGridState.isMonsterPoint = !mGridState.isMonsterPoint;
            if (mGridState.isMonsterPoint) {
                mGridRenderer.sprite = mMonsterSprite;
            }
            else {
                mGridRenderer.sprite = mGridSprite;
            }
            
            MapMaker.Instance.mMonsterIndexList.Add(mGridIndex);
        }
        else if (Input.GetKey(KeyCode.O)) {//生成道具
            mGridState.canBuild = false;
            mGridRenderer.sprite = mGridSprite; 

            mGridState.itemId++;
            mGridState.itemType = GetItemType(mGridState.itemId);
            if (mGridState.itemId >= mGridItemPrefabArray.Length) {
                mGridState.itemId = -1;
                mGridState.hasItem = false;
                Destroy(mCurItem);
                return;
            }
            if (mCurItem) { Destroy(mCurItem); }

            CreateItem();
            mGridState.hasItem = true;
        }
        else if (!mGridState.isMonsterPoint)//如果不是怪物路径点，控制是否可以建造item或塔
        {
            mGridState.canBuild = !mGridState.canBuild;
            if (mGridState.canBuild)
            {
                mGridRenderer.enabled = true;
            }
            else {
                mGridRenderer.enabled = false;
            }
        }
    }  

    private void CreateItem()
    {
        Vector3 createPos = transform.position;
        if (mGridState.itemType == ItemType.Four)
        {
            createPos += new Vector3(MapMaker.Instance.GridWidth / 2, -MapMaker.Instance.GridHeight / 2);
        }
        else if (mGridState.itemType == ItemType.Two)
        {
            createPos += new Vector3(MapMaker.Instance.GridWidth / 2, 0);
        } 

        mCurItem = GameObject.Instantiate(mGridItemPrefabArray[mGridState.itemId], createPos, Quaternion.identity);
    }

#endif

#if Game
    private void OnMouseDown()
    {
        //选择的是UI则不发生交互
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        

        mGameCtrl.HandleGrid(this);
    }

    public void ShowGrid()
    {
        if (!mHasTower)
        {
            mGridRenderer.enabled = true;
            //显示建塔列表
            mGameCtrl.SetTowerBuildGoState(this, true);
        }
        else
        {
            mGameCtrl.SetTowerLevelUpGoState(this, true);
        }
    }

    public void HideGrid()
    {
        if (!mHasTower)
        {
            //隐藏建塔列表
            mGameCtrl.SetTowerBuildGoState(this, false);
        }
        else
        {
            mGameCtrl.SetTowerLevelUpGoState(this, false);
        }
        mGridRenderer.enabled = false;
    }

    public void AfterBuildTower() {

    }

    //显示此格子不能够去建塔
    public void CanNotBuildTower()
    {
        mGridRenderer.enabled = true;
        Tween t = DOTween.To(() => mGridRenderer.color, toColor => mGridRenderer.color = toColor, new Color(1, 1, 1, 0), 2f);
        t.OnComplete(() =>
        {
            mGridRenderer.enabled = false;
            mGridRenderer.color = new Color(1, 1, 1, 1);
        });
    }

#endif

    private ItemType GetItemType(int itemId) {
        if (itemId <= 1)
        {
            return ItemType.Four;
        }
        else if (itemId <= 3)
        {
            return ItemType.Two;
        }
        else {
            return ItemType.One;
        }
    } 
    private void ResetGridSprite()
    {
        mGridRenderer.enabled = false;
        mGridRenderer.color = new Color(1, 1, 1, 1);

        if (mGridState.canBuild)
        {
            mGridRenderer.sprite = mGridSprite;
        }
        else
        {
            mGridRenderer.sprite = mCantBuildSprite;
        }
    }
}

public struct GridState {
    public bool canBuild;
    public bool isMonsterPoint;
    public bool hasItem;
    public int itemId;
    public ItemType itemType;
}
public struct GridIndex {
    public int x;
    public int y;
}

/// <summary>
/// 代表占据格子的数量
/// </summary>
public enum ItemType {
    One,
    Two,
    Three,
    Four,
}