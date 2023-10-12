using UnityEngine;

public class EnemyApproachState : EnemyState{
    protected ITargetable<EnemyEntity> player => enemy.Player;
    public EnemyApproachState(EnemyEntity other, string animName) : base(other, animName){}
    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isStateForceChanged) return;

        if(player == null){
            stateMachine.ChangeState(enemy.Factory.IdleState);
            isStateForceChanged = true;
            return;
        }

        if(isAnimationFinish && enemy.Factory.AttackState.CanAttack){
            enemy.Factory.AttackState.ReadyToAttack();
            isStateForceChanged = true;
            return;
        }

    }
    protected virtual void ApproachPlayer(){}
}