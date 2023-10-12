public class EnemyDeathState : EnemyState
{
    public EnemyDeathState(EnemyEntity entity, string animName) : base(entity, animName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        enemy.Core.Movement.SetVectorZero();
        enemy.IsDead = true;
    }
    public override void LogicUpdate()
    {
        //base.LogicUpdate();
        if(isAnimationFinish){
            enemy.DestroySelf();
            Exit();
            return;
        }
        enemy.Core.Movement.SetVectorZero();
    }
}