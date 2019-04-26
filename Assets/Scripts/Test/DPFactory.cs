using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPFactory : MonoBehaviour
{ 
    void Start()
    {
        IPhoneFactory iFactory = new IPhoneFactory();
        iFactory.CreateIPhone();
    }
     
}

//抽象工厂模式
public class IPhoneFactory
{
    public void CreateIPhone()
    {
        IPhone8Factory i8Factory = new IPhone8Factory();
        i8Factory.CreateIPhone8();
        i8Factory.CreateIPhone8Charger();
        IPhoneXFactory ixFactory = new IPhoneXFactory();
        ixFactory.CreateIPhoneX();
        ixFactory.CreateIPhoneXCharger();
    }
}
public class IPhone8Factory
{
    public void CreateIPhone8()
    {
        IPhone8 i8 = new IPhone8();
        i8.Init();
    }
    public void CreateIPhone8Charger() {
        IPhone8Charger i8Charger = new IPhone8Charger();
        i8Charger.Init();
    }
}
public class IPhoneXFactory
{
    public void CreateIPhoneX()
    {
        IPhoneX ix = new IPhoneX();
        ix.Init();
    }

    public void CreateIPhoneXCharger() {
        IPhoneXCharger ixCharger = new IPhoneXCharger();
        ixCharger.Init();
    }
}
public class IPhone8
{
    public void Init()
    {
        Debug.Log("I AM IPhone8");
    }
}
public class IPhoneX
{
    public void Init()
    {
        Debug.Log("I AM IPhoneX");
    }
} 
public class IPhone8Charger {
    public void Init() {
        Debug.Log("I AM IPhone8Charger");
    }
} 
public class IPhoneXCharger
{
    public void Init()
    {
        Debug.Log("I AM IPhoneXCharger");
    }
}




//普通工厂模式
//public class IPhoneFactory{ 
//    public void CreateIPhone() {
//        IPhone8Factory i8Factory = new IPhone8Factory();
//        i8Factory.CreateIPhone8();
//        IPhoneXFactory ixFactory = new IPhoneXFactory();
//        ixFactory.CreateIPhoneX();
//    }
//}
//public class IPhone8Factory {
//    public void CreateIPhone8() {
//        IPhone8 i8 = new IPhone8();
//        i8.Init();
//    }
//}
//public class IPhoneXFactory
//{
//    public void CreateIPhoneX()
//    {
//        IPhoneX ix = new IPhoneX();
//        ix.Init();
//    }
//}
//public class IPhone8 {
//    public void Init() {
//        Debug.Log("I AM IPhone8");
//    }
//}
//public class IPhoneX {
//    public void Init()
//    {
//        Debug.Log("I AM IPhoneX");
//    }
//}


