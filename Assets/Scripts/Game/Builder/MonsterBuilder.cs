using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBuilder : IBuilder<Monster>
{
    public GameObject mMonsterGo;
    public int mMonsterId;

    public GameObject GetProduct()
    {
        mMonsterGo = GameController.Instance.GetItem("Monster");
        Monster monsterClass = mMonsterGo.GetComponent<Monster>();
        if (monsterClass == null) {
            monsterClass = mMonsterGo.AddComponent<Monster>();
        }
        SetData(monsterClass);
        GetPeculiar(monsterClass);
        return mMonsterGo;
    }

    public void SetData(Monster _monster)
    {
        _monster.mId = mMonsterId;
        _monster.mHP = mMonsterId * 100;
        _monster.mMaxHp = mMonsterId * 100;
        _monster.mMoveSpeed = mMonsterId;
        _monster.mPrize = mMonsterId * 50;
    }

    public void GetPeculiar(Monster _monster)
    {
        _monster.SetPeculiar();
    }
}
