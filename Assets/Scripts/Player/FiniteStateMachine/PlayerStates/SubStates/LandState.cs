public class LandState : GroundedState{
    public LandState(Player other, string otherAnimName)
    : base(other, otherAnimName){}

    public override void Enter()
    {
        base.Enter();
        Movement.SetVectorZero();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isStateForceChanged) return;
        // is moving 
        if(input.x != 0){
            stateMachine.ChangeState(player.Factory.MovementState);
        }
        else if(isAnimationFinish){
            stateMachine.ChangeState(player.Factory.IdleState);
        }
    }
}