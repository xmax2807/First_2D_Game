using UnityEngine;
public class EnemyIdleState : EnemyState{
    public EnemyIdleState(EnemyEntity other, string animName) : base(other, animName){}

    public bool isShouldFlip;
    private float IdleTime;
    public override void Enter()
    {
        base.Enter();
        Movement.FreezeContraint(RigidbodyConstraints2D.FreezePositionX);
        //Movement.SetVectorZero();
        IdleTime = Random.Range(Data.IdleWaitMin, Data.IdleWaitMax);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isStateForceChanged) return;
        if(Time.time >= startTime + IdleTime){
            OnStateFinished();
        }
    }
    public override void Exit()
    {
        base.Exit();
        Movement.DefaultConstraints();
    }
    protected override void OnStateFinished(){
        Movement.DefaultConstraints();
        stateMachine.ChangeState(enemy.Factory.MovementState);
    }
}