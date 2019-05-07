using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 关卡管理类，管理Round
/// </summary>
public class Level  
{
    public int mRoundCount;
    public int mCurRoundIndex;
    public Round[] mRoundArray;

    public Level(int count, List<RoundInfo> infoList) {
        mRoundCount = count;
        mRoundArray = new Round[count];
        for (int i = 0; i < count; i++)
        {
            mRoundArray[i] = new Round(infoList[i],i, this);
        }

        for (int i = 0; i < mRoundCount; i++)
        {
            if (i >= mRoundCount - 1)
                break;
            mRoundArray[i].SetNextRound(mRoundArray[i + 1]);
        }
    }

    public void HandleRound() {
        if (mCurRoundIndex >= mRoundCount)
        {
            //游戏胜利
        }
        else if (mCurRoundIndex == mRoundCount - 1)
        {
            //最后一波怪
            Debug.Log("最后一波怪物");
            HandleLastRound();
        }
        else {
            mRoundArray[mCurRoundIndex].Handle(mCurRoundIndex);
        }  
    }

    public void HandleLastRound() {
        mRoundArray[mCurRoundIndex].Handle(mCurRoundIndex);
    }

    public void AddUpRoundIndex() {
        mCurRoundIndex++;
    }



}
