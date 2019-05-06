using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPoint : MonoBehaviour
{

    private SpriteRenderer mGridRenderer;
    public GridState mGridState;
    public GridIndex mGridIndex;

    private Sprite mGridSprite;
    private Sprite mMonsterSprite; 
    private GameObject[] mGridItemPrefabArray;
    private GameObject mCurItem;

    private string mItemPath = "Map/Item/";

    private void Awake()
    {
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
            string path = mItemPath + GameController.Instance.mBigId.ToString() + mGridState.itemId.ToString();
            GameController.Instance.PushItem(path, mCurItem);
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
            createPos += new Vector3(GameController.Instance.mMapMaker.GridWidth / 2, -GameController.Instance.mMapMaker.GridHeight / 2);
        }
        else if (mGridState.itemType == ItemType.Two)
        {
            createPos += new Vector3(GameController.Instance.mMapMaker.GridWidth / 2, 0);
        }
        string path = mItemPath + GameController.Instance.mBigId.ToString() + mGridState.itemId.ToString();
        mCurItem = GameController.Instance.GetItem(path); 
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
            mGridState.canBuild = true;
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