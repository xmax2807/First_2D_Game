public class WallState : PlayerState{

    protected bool isGrounded;
    protected bool isTouchingLowWall;
    protected bool isTouchingWall;

    public WallState(Player other, string otherAnimName) 
    : base(other, otherAnimName){}

    public override void Check(){
        base.Check();
        isTouchingLowWall = Sense.CheckIfHalfPartTouchedWall();
        isTouchingWall = Sense.CheckIfTouchingWall();
        isGrounded = Sense.CheckIfTouchingGround();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        var xInput = InputHandler.Movement.x;
        if(isGrounded && !(isTouchingLowWall|| isTouchingWall)){
            stateMachine.ChangeState(player.Factory.IdleState);
            return;
        }

        if(!(isTouchingLowWall|| isTouchingWall) || xInput != Movement.FacingDirection){
            stateMachine.ChangeState(player.Factory.InAirState);
        }
    }

    public override void Exit(){
        base.Exit();
    }
}