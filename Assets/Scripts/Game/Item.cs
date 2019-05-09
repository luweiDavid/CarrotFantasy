using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 道具类
/// </summary>
public class Item : MonoBehaviour
{
    public GridPoint mGridPoint;
    public int mItemId;

    private GameController mGameCtrl;

    private void Start()
    {
        mGameCtrl = GameController.Instance;
    }

public void Init(GridPoint grid, int id) {
        mGridPoint = grid;
        mItemId = id;
    }


    private void OnMouseDown()
    {
        if (mGameCtrl.mCurTargetTr == null)
        {
            mGameCtrl.mCurTargetTr = transform;
            mGameCtrl.ShowSignal();
        }
        //转换新目标
        else if (mGameCtrl.mCurTargetTr != transform)
        {
            mGameCtrl.HideSignal();
            mGameCtrl.mCurTargetTr = transform;
            mGameCtrl.ShowSignal();
        }
        //两次点击的是同一个目标
        else if (mGameCtrl.mCurTargetTr == transform)
        {
            mGameCtrl.HideSignal();
        }
    }
}
