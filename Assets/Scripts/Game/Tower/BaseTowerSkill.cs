using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTowerSkill : MonoBehaviour
{
    public Tower mTower;

    public GameObject mBulletGo;

    private GameController mGameCtrl;

    private void Start()
    {
        mTower = GetComponent<Tower>();
        mGameCtrl = GameController.Instance;
    }



    public void ResetData() {
        mTower = null;
    }
     
}
