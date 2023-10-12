public class EnemyMovementState : EnemyState
{
    protected bool isTouchingWall;
    protected bool isReachedLedge;
    
    public EnemyMovementState(Entity entity, string animName) : base(entity, animName){
    }

    public override void Enter()
    {
        base.Enter();
        Movement.SetVelocityX(Data.speed * Movement.FacingDirection);
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        Movement.SetVelocityX(Data.speed * Movement.FacingDirection);
    }
    public override void Check()
    {
        base.Check();
        isTouchingWall = Sense.CheckIfTouchingWall();
        isReachedLedge = !Sense.CheckIfTouchingLedge();
    }
}