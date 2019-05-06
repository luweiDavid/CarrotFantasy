using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager 
{
    //item的id从1开始

    public int adventrueModelNum; //冒险模式解锁的地图个数
    public int burriedLevelNum; //隐藏关卡解锁的地图个数
    public int bossModelNum;//boss模式KO的BOSS
    public int coin;//获得金币的总数
    public int killMonsterNum;//杀怪总数
    public int killBossNum;//杀掉BOSS的总数
    public int clearItemNum;//清理道具的总数

    public List<bool> bigLevelStatusList;//大关卡是否解锁列表
    public List<Stage> levelStageList;//所有的小关卡数据 
    public List<int> levelUnlockedNumList;//解锁的小关卡数量列表
    public List<int> levelTotalNumList;  //小关卡总数量列表

    //怪物窝
    public int cookies;
    public int milk;
    public int nest;
    public int diamands;
    //public List<MonsterPetData> monsterPetDataList;//宠物喂养信息
     
    public int maxBigLevel = 3;
    //大关卡id对应的小关卡的最大等级
    public Dictionary<int, int> bigIDLevelNumDic;

    public int maxTowerCount = 12;

    //大关卡下对应的怪物最大数
    public int curMaxMonsterNum = 12;

    public PlayerManager() {
        bigLevelStatusList = new List<bool>();
        levelStageList = new List<Stage>();
        levelUnlockedNumList = new List<int>();
        levelTotalNumList = new List<int>();

        bigIDLevelNumDic = new Dictionary<int, int>();  
        for (int i = 1; i <= maxBigLevel; i++)
        {
            bigIDLevelNumDic[i] = 5;
        }


        //测试代码
        bigLevelStatusList.Add(true);
        bigLevelStatusList.Add(false);
        bigLevelStatusList.Add(false);

        //int count,bool isAllClear,int state,int levelid,int bigid,bool unlocked
        Stage s1 = new Stage(3, false, 1, 1, 1, true);
        levelStageList.Add(s1);
        Stage s2 = new Stage(6, false, 2, 2, 1, false);
        levelStageList.Add(s2);
        Stage s3 = new Stage(7, false, 3, 3, 1, true);
        levelStageList.Add(s3);
        Stage s4 = new Stage(1, false, 1, 4, 1, false);
        levelStageList.Add(s4);
        Stage s5 = new Stage(9, false, 1, 5, 1, true);
        levelStageList.Add(s5);


        levelUnlockedNumList.Add(1);
        levelTotalNumList.Add(5);
        levelUnlockedNumList.Add(1);
        levelTotalNumList.Add(5);
        levelUnlockedNumList.Add(1);
        levelTotalNumList.Add(5);   


    }
}
