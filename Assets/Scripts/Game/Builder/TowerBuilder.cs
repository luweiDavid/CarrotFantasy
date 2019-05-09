using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 塔的建造者
/// </summary>
public class TowerBuilder : IBuilder<Tower>
{
    public int mTowerId;
    public GameObject mTowerGo; 
    public int mTowerLevel;

    public GameObject GetProduct()
    {
        GameObject towerGo = GameController.Instance.GetItem("Tower/ID" + mTowerId.ToString() + 
            "/TowerSet/" + mTowerLevel.ToString());
        Tower towerClass = towerGo.GetComponent<Tower>();
        if (towerClass == null) {
            towerClass = towerGo.AddComponent<Tower>();
        }
        SetData(towerClass);
        GetPeculiar(towerClass);
        return towerGo;
    }

    public void SetData(Tower towerClass)
    { 

    }

    public void GetPeculiar(Tower towerClass)
    {
         
    }
}
