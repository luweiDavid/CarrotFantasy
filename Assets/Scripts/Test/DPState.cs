using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPState : MonoBehaviour
{
    public StateMgr mStateMgr;
    void Start()
    {
        mStateMgr = new StateMgr();
        mStateMgr.SetState(new IdleState(mStateMgr));
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.A)) {
        //    mStateMgr.SetState(new WalkState(mStateMgr));
        //}
        //if (Input.GetKeyDown(KeyCode.B)) {
        //    mStateMgr.SetState(new RunState(mStateMgr));
        //}
    } 
} 
public class StateMgr{
    public BaseState mCurState;
    public void SetState(BaseState state) {
        if (mCurState == state) {
            return;
        } 
        state.EnterState();
        mCurState = state;
    }
}
public abstract class BaseState {
    public StateMgr mStateMgr;
    public BaseState(StateMgr mgr) {
        mStateMgr = mgr;
    }
    public abstract void EnterState(); 
    public virtual void UpdateState() { } 
    public virtual void ExitState() { }
}
public class IdleState : BaseState{
    public IdleState(StateMgr mgr) : base(mgr){ }
    public override void EnterState()
    {
        Debug.Log("Enter IdleState");
    }
}
public class WalkState : BaseState{
    public WalkState(StateMgr mgr) : base(mgr) { }
    public override void EnterState()
    {
        Debug.Log("Enter WalkState");
    }
}
public class RunState : BaseState{
    public RunState(StateMgr mgr) : base(mgr) { }
    public override void EnterState()
    {
        Debug.Log("Enter RunState");
    }
}

