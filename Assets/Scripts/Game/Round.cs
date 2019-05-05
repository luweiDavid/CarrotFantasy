using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round 
{
    public RoundInfo mRoundInfo;
    private Level mLevel;

    public Round mNextRound;
    public int mRoundIndex;

    public Round(RoundInfo info,Level _level) {
        mRoundInfo = info;
        mLevel = _level;
    }

    public Round SetNextRound(Round round) {
        this.mNextRound = round;
        return round;
    }
    public void Handle(int index) {
        if (mRoundIndex < index)
        {
            if (mNextRound != null)
            {
                mNextRound.Handle(index);
            }
        }
        else {
            //生成怪物
        }
    }


}

[System.Serializable]
public struct RoundInfo {
    public List<int> MonsterIdList;
}

