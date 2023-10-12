public class EnemyPlayerDetectState : EnemyState{
    public EnemyPlayerDetectState(EnemyEntity other, string animName) : base(other, animName){}

    public override void Enter()
    {
        base.Enter();
        Movement.SetVectorZero();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(enemy.Player == null){
            stateMachine.ChangeState(enemy.Factory.MovementState);
            return;
        }
        Movement.LookAtTarget(enemy.Player.GetPosition());
        stateMachine.ChangeState(enemy.Factory.AttackState);
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        Movement.SetVectorZero();
    }
}