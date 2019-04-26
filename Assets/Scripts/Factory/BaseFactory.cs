using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFactory : IBaseFactory
{
    //游戏物体资源字典（也就是没有实例化的prefab）
    public Dictionary<string, GameObject> goResDic = new Dictionary<string, GameObject>();

    //实例化对象池字典
    public Dictionary<string, Stack<GameObject>> goPoolDic = new Dictionary<string, Stack<GameObject>>();

    public string prefabPath;

    public BaseFactory() {
        prefabPath = "Prefabs/";
    }

    public void PushItem(string itemName, GameObject item)
    { 
        if (goPoolDic.ContainsKey(itemName))
        {
            item.SetActive(false);
            item.transform.SetParent(GameManager.Instance.GameObjectPoolTr);
            goPoolDic[itemName].Push(item); 
        }
        else {
            Debug.LogError("不存在" + itemName+"对应的栈");
        }
    }

    public GameObject GetItem(string itemName)
    {
        GameObject itemGo;
        if (goPoolDic.ContainsKey(itemName))
        {
            if (goPoolDic[itemName].Count <= 0) {
                itemGo = GetGameObjRes(itemName);
            }
            else
            {
                itemGo = goPoolDic[itemName].Pop(); 
            }
            if (itemGo == null) {
                Debug.LogError("资源获取失败！");
            } 
            itemGo.SetActive(true); 
        }
        else {
            goPoolDic[itemName] = new Stack<GameObject>();
            itemGo = GetGameObjRes(itemName);
        }
        return itemGo;
    }

    private GameObject GetGameObjRes(string itemName) {
        GameObject itemGo = null;

        string goLoadPath = prefabPath + itemName;
        if (goResDic.ContainsKey(itemName))
        {
            itemGo = goResDic[itemName];
        }
        else { 
            itemGo = Resources.Load<GameObject>(goLoadPath);
        }
        if (itemGo == null) {
            Debug.LogError("资源加载失败，路径：" + goLoadPath); 
        }

        return itemGo;
    }
}



/// <summary>
/// 游戏对象工厂的类型
/// </summary>
public enum FactoryType {
    UI,
    UIPanel,
    Game,
}