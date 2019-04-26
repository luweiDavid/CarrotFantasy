using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPFacade : MonoBehaviour{ 
    void Start(){
        Boss mBoss = new Boss();
        mBoss.SetReportOrder();
        mBoss.SetSeasonTarget();
    } 
} 
//外观模式： 上层管理不需要知道具体的功能实现，本质上是只需要知道外部调用接口就行
public class Boss {
    public SaleManager mSaleMgr = new SaleManager();
    public void SetReportOrder()
    {
        mSaleMgr.GetReport();
    }
    public void SetSeasonTarget() {
        mSaleMgr.Implement();
    }
} 
public class SaleManager {
    public SalesMan mSalesMan = new SalesMan();
    public SalesWowan mSalesWoman = new SalesWowan();
    public void GetReport() {
        mSalesMan.ReportPerformance();
        mSalesWoman.ReportPerformance();
    }
    public void Implement() {
        mSalesMan.Work();
        mSalesWoman.Work();
    }
}
public class SalesMan {
    public void ReportPerformance() {
        Debug.Log("SalesMan 汇报业绩");
    } 
    public void Work() {
        Debug.Log("SalesMan 努力工作中");
    }
}
public class SalesWowan {
    public void ReportPerformance(){
        Debug.Log("SalesWowan 汇报业绩");
    }
    public void Work(){
        Debug.Log("SalesWowan 努力工作中");
    }
}



