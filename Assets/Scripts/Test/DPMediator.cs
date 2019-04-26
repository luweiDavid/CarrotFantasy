using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPMediator : MonoBehaviour{ 
    void Start(){
        //Man _man = new Man(43, 1000000);
        //Woman _woman = new Woman(35, 80000);

        //Mediator mMediator = new Mediator();
        //mMediator.SendMsg(_man, _woman);
        //mMediator.SendMsg(_woman, _man);

        //Debug.Log("男方好感度：" + _man.Favor);
        //Debug.Log("女方好感度：" + _woman.Favor);
    } 
} 
public class Mediator { 
    public void SendMsg(Matchmaker sender, Matchmaker receiver) {
        receiver.Favor += -sender.Age + sender.Money;
    }
}
public abstract class Matchmaker {
    public int Age { get; set; } 
    public int Money { get; set; } 
    public int Favor { get; set; } 
    public Matchmaker(int age, int money) {
        this.Age = age;
        this.Money = money;
    }  
}   
public class Man : Matchmaker
{
    public Man(int age, int money) : base(age, money) { }  
} 
public class Woman : Matchmaker{
    public Woman(int age, int money) : base(age, money) { } 
}


