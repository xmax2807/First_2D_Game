public class WallSlideState : WallState {
    public WallSlideState(Player other, string otherAnimName) 
    : base(other, otherAnimName){}

    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isStateForceChanged) return;

        if(InputHandler.IsJump){
            player.Factory.WallJumpState.DetermineJumpDirection(Sense.CheckIfTouchingWall());
            stateMachine.ChangeState(player.Factory.WallJumpState);
        }
        else{
            Movement.SetVelocityY(data.WallSlideVelocity * -1);// go down
        }
    }
    public override void Exit()
    {
        base.Exit();
        Movement.Flip();
    }
}