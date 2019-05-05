using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo 
{
    public int BigLevelId;
    public int LevelId;

    //当前关卡的所有格子
    public List<GridState> GridStateList;
    //怪物路径点集合
    public List<GridIndex> GridIndexList;
    //当前关卡的回合信息（包括要生产的怪物id列表等）
    public List<RoundInfo> RoundInfoList;
     
}
