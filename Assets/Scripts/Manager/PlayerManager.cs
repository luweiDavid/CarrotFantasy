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
    }
}
