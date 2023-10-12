using UnityEngine;

public class TeleportApproachState : EnemyApproachState
{
    public TeleportApproachState(EnemyEntity other, string animName) : base(other, animName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Movement.SetVectorZero();
        TimeManager.Instance.WaitForSeconds(0.55f,ChangeState);
    }
    protected void ChangeState(){
        if(player == null){
            stateMachine.ChangeState(enemy.Factory.IdleState);
            return;
        }
        enemy.transform.position = player.GetPosition() + Movement.FacingDirection*new Vector2(-1f,0);

    }
}