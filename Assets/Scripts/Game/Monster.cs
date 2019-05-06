using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Animator mAnimator;
    public int mId;
    public int mHP;
    public int mMaxHp;
    public float mMoveSpeed;
    public float mInitMoveSpeed;
    public int mPrize;

    public void SetPeculiar() {
        mAnimator.runtimeAnimatorController = GameController.Instance.mMonsterAnimatorArray[mId - 1];
    }


}
