using System;
using UnityEngine;
public class EnemyState : State
{
    protected EnemyCollisionSense Sense => (EnemyCollisionSense)enemy.Core.Senses;
    protected EnemyMovement Movement => (EnemyMovement)enemy.Core.Movement;
    protected EnemyEntity enemy => (EnemyEntity)entity;
    protected EnemyStatData Data => enemy.Data;
    public EnemyState(Entity entity, string animName) : base(entity, animName)
    {
    }

    public override void Check(){
        base.Check();
        if(!enemy.CanAttack) return;

        if(enemy.Player == null){
            DetectPlayer();
        }
        else{
            FollowPlayer();
        }
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!enemy.CanAttack || enemy.Player == null) return;

        if(enemy.Factory.AttackState.CanAttack){
            enemy.Factory.AttackState.ReadyToAttack();
        }       
    }
    protected Transform target;
    private void DetectPlayer()
    {
        target = Sense.CheckPlayerIsInRange().transform;
        if (target == null || !target.TryGetComponent<Player>(out var player)) return;

        enemy.AttachPlayer(player);
        
        var distance = Movement.GetDistance(enemy.Player.GetPosition());
        if(enemy.Data.AttackRange < distance && distance < Sense.enemyCollisionData.ObservationRange - 1f){
            stateMachine.ChangeState(enemy.Factory.ApproachState);
            return;
        }
    }
    private void FollowPlayer()
    {
        if(enemy.Player.IsInvisible){
            enemy.DettachPlayer();
            //stateMachine.ChangeState(enemy.Factory.IdleState);
            return;
        }
        var distance = Movement.GetDistance(enemy.Player.GetPosition());
        if (distance > Sense.enemyCollisionData.ObservationRange)
        {
            enemy.DettachPlayer();
            //stateMachine.ChangeState(enemy.Factory.IdleState);
            return;
        }
        
    }

    protected virtual void OnStateFinished(){}
}