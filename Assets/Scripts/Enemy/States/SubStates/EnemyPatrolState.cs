using UnityEngine;
using System;
public class EnemyPatrolState : EnemyMovementState{
    public EnemyPatrolState(EnemyEntity other, string animName) : base(other, animName){}
    private int MoveCount = 2;
    private float MoveTime;
    public override void Enter()
    {
        base.Enter();
        MoveTime = Data.distance / Data.speed + startTime;
    }
    private bool IsTimeOut = false;
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isStateForceChanged) return;

        if(IsTimeOut){
            MoveCount--;
            stateMachine.ChangeState(enemy.Factory.IdleState);
        }
    }
    public override void Check()
    {
        base.Check();
        IsTimeOut = Time.time > MoveTime || isTouchingWall || isReachedLedge;
        if(MoveCount <= 0){
            Movement.Flip();
            MoveCount = 2;
            return;
        }
        if(isTouchingWall || isReachedLedge) Movement.Flip();
    }
}