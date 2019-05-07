using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    private GameController mGameCtrl;
    private List<Vector3> mMonsterIdList;

    public Animator mAnimator;
    public int mId;
    public float mHP;
    public float mMaxHp;
    public float mMoveSpeed;
    public float mInitMoveSpeed;
    public int mPrize;

    public GameObject mTshitGo;
    public float mDecreaseSpeedTime;
    public float mDecreaseSpeedInterval;
    public bool mIsDecreasing;
    public bool mIsReachGoal;
    public int mCurReachIndex;

    public Slider mBloodSlider; 
    private Vector3 mLeftDirection;
    private Vector3 mRightDirection;
    private bool mHadAdjustDirection;
     

    private void Awake()
    {
        mAnimator = GetComponent<Animator>();
        mBloodSlider = transform.Find("MonsterCanvas/HPSlider").GetComponent<Slider>();  
        mGameCtrl = GameController.Instance;
        mMonsterIdList = GameController.Instance.mMapMaker.mMonsterPosList;

        mLeftDirection = new Vector3(0, 180, 0);
        mRightDirection = new Vector3(0, 0, 0);
        mHadAdjustDirection = false;
        mCurReachIndex = 1;
        mBloodSlider.gameObject.SetActive(false);
        mBloodSlider.value = 1;
        AdjustDirection(true); 
    }

    //重置数据
    private void ResetData()
    {
        mId = 0;
        mHP = 0;
        mMaxHp = 0;
        mMaxHp = 0;
        mMoveSpeed = 0;
        mInitMoveSpeed = 0;
        mPrize = 0;
        mDecreaseSpeedTime = 0;
        mDecreaseSpeedInterval = 0;
        mIsDecreasing = false;
        mIsReachGoal = false;
        mCurReachIndex = 1;
        mLeftDirection = new Vector3(0, 180, 0);
        mRightDirection = new Vector3(0, 0, 0);
        mHadAdjustDirection = false;
        mBloodSlider.gameObject.SetActive(false);
        mBloodSlider.value = 1;

        AdjustDirection(true);
        ClearBuff();
    }

    private void ClearBuff() {

    }

    private void Update()
    {
        if (mGameCtrl.mIsGamePause) {
            return;
        }

        if (mIsReachGoal)
        {
            DestroySelf();
        }
        else {
            if (mIsDecreasing) {
                //减速buff生效期间

            }
            Vector3 nextPos = mGameCtrl.mMapMaker.mMonsterPosList[mCurReachIndex]; 
            int gameSpeed = mGameCtrl.mCurGameSpeed;
            float dis = Vector3.Distance(transform.position, nextPos); 
            transform.position = Vector3.Lerp(transform.position, nextPos, mMoveSpeed * gameSpeed * Time.deltaTime * 1 / dis);
            if (!mHadAdjustDirection) {
                mHadAdjustDirection = true;
                AdjustDirection(false);
            }
            if (dis < 0.01) {
                mHadAdjustDirection = false;
                mCurReachIndex++;
            }
            if (mCurReachIndex >= mMonsterIdList.Count)
            {
                mIsReachGoal = true;
            }
        }
    }

    public void TakeDamage(float damage) {
        mHP -= damage;
        if (mHP <= 0) {
            //播放死亡音效
           
            DestroySelf();
        }
        mBloodSlider.value = mHP / mMaxHp;
    }

    private void DestroySelf() { 
        if (!mIsReachGoal)  //到达终点的销毁
        {
            mGameCtrl.mCarrotHP--;
        }
        else
        {  //被玩家杀死的销毁 

            //生成金币，增加玩家金币，销毁特效
            GameObject coinGo = mGameCtrl.GetItem("CoinCanvas");
            coinGo.transform.Find("Emp_Coin").GetComponent<Coin>().prize = mPrize;
            coinGo.transform.SetParent(mGameCtrl.transform);
            coinGo.transform.localPosition = transform.position;

            GameObject destroyEffectGo = mGameCtrl.GetItem("DestroyEffect");
            destroyEffectGo.transform.SetParent(mGameCtrl.transform);
            destroyEffectGo.transform.localPosition = transform.position;

            //获取特效的播放时间，按照这个播放时间去销毁特效  TODO
            //Animator anim = destroyEffectGo.GetComponent<Animator>();
            //RuntimeAnimatorController animCtrl = anim.runtimeAnimatorController;
            //AnimationClip[] animations = animCtrl.animationClips;
            //animations[0].



            mGameCtrl.ChangeCoin(mPrize);  
            mGameCtrl.mCurKillMonsterNum++;
            mGameCtrl.mTotalKillMonsterNum++;
        }
        ResetData();
        mGameCtrl.PushItem("Monster", gameObject);
    } 

    //调整怪物的转向
    private void AdjustDirection(bool isStartPoint) {
        if (mCurReachIndex + 1 < mMonsterIdList.Count) {
            int curIndex = isStartPoint ? 0 : mCurReachIndex;
            int nextIndex = isStartPoint ? 1 : mCurReachIndex + 1;

            float value = mMonsterIdList[curIndex].x - mMonsterIdList[nextIndex].x;
            if (value > 0)
            {
                transform.eulerAngles = mLeftDirection;
                mBloodSlider.transform.eulerAngles = mLeftDirection;
            }
            else {
                transform.eulerAngles = mRightDirection;
                mBloodSlider.transform.eulerAngles = mRightDirection;
            } 
        }
    }

    public void SetPeculiar() {
        mAnimator.runtimeAnimatorController = mGameCtrl.mMonsterAnimatorArray[mId - 1];
    }


}
