using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tower : MonoBehaviour
{
    //塔管理塔的技能脚本， 技能脚本控制子弹


    public int mId;
    public int mLevel;
    //当前价格
    public int mPrice;
    //升级价格
    public int mUpgradePrice;
    //售价
    public int mSellPrice;

    //塔的攻击技能
    public BaseTowerSkill mTowerSkill;
    private GameController mGameCtrl;
     
    public Transform mTargetTr;

    //是否有攻击目标
    public bool mHasTarget;
    //优先攻击的目标
    public bool mHasSpecificTarget;

    public void ResetData() {
        mId = 0;
        mLevel = 0;
        mPrice = 0;
        mUpgradePrice = 0;
        mSellPrice = 0;
        mHasTarget = false;
        mHasSpecificTarget = false;

    }

    private void Start()
    {
        mUpgradePrice = (int)(mPrice * 1.5f);
        mSellPrice = mPrice / 2;
        mTowerSkill = GetComponent<BaseTowerSkill>();
        mGameCtrl = GameController.Instance;
    }

    private void Update()
    {
        Transform target = mGameCtrl.mCurTargetTr;
        if (target != null)
        {
            if (mTargetTr != target) {
                //当前有集火目标， 但是跟塔的攻击目标不一致时，需要重新调整塔的攻击目标为集火目标
                mHasSpecificTarget = false;
            }
        }
        else {
            if (mHasTarget) {
                //如果有目标，但不是集火目标，并且没有集火目标
                //这时要时时判断目标是否死亡
                
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D col = collision.collider;
        if (col.tag != "Monster" || col.tag != "Item"||mHasSpecificTarget){
            //如果检测到的transform不是怪物或者道具或者已经有了集火目标 则return；
            return;
        }

        Transform target = mGameCtrl.mCurTargetTr;
        if (target != null)
        {
            //设置了集火目标, 那么就需要把塔的目标设置成改目标，
            if (mTargetTr != target) {
                mTargetTr = target;
                mHasSpecificTarget = true;
                mHasTarget = true;
            }
        }
        else { 
            mHasSpecificTarget = false;
            //没有设置集火目标
            if (!mHasTarget)
            {
                mTargetTr = col.transform;
                mHasTarget = true;
            } 
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (mHasTarget) { 
            mTargetTr = null;
            mHasTarget = false;
            mHasSpecificTarget = false;
        }
    }
}
