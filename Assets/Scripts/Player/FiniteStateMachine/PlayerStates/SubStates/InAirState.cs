using UnityEngine;
public class InAirState : PlayerState{
    protected bool isFullyGrounded;
    protected bool isGroundedAny;
    protected bool isTouchingWall;
    protected bool isInCoyoteTime;
    protected bool isTouchingLegde;
    protected bool isTouchingLowWall;
    private int xInput => (int)InputHandler.Movement.x;
    public InAirState(Player other, string otherAnimName) 
    : base(other, otherAnimName){}

    public override void Check()
    {
        base.Check();

        isFullyGrounded = Sense.CheckIfTouchingGround();
        isGroundedAny = Sense.CheckIfTouchingAnyGroundSide();
        isTouchingWall = Sense.CheckIfTouchingWall();
        isTouchingLowWall = Sense.CheckIfHalfPartTouchedWall();
        isTouchingLegde = Sense.CheckIfTouchingLedge();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isStateForceChanged) return;        

        // if attack button is pressed
        if(CombatInput.Attacking){
            stateMachine.ChangeState(player.Factory.AttackState);
            return;
        }

        // if ability button is pressed
        if(AbilityInputHandler.IsAbilityPressed && player.Factory.SpecialAbilityState.CanDoAbility()){
            stateMachine.ChangeState(player.Factory.SpecialAbilityState);
            return;
        }
        
        // if touching the ground => land or move
        if(!(isTouchingWall || isTouchingLowWall) && isFullyGrounded){
            if(Mathf.Abs(Movement.CurrentVelocity.x) > 0.1f) {
                stateMachine.ChangeState(player.Factory.MovementState);
                return;
            }
            if(Mathf.Abs(Movement.CurrentVelocity.y) < 0.01f){
                stateMachine.ChangeState(player.Factory.LandState);
                return;
            }
        }

        // if touching the ledge
        if(!isTouchingLegde){
            if(isTouchingWall) {
                stateMachine.ChangeState(player.Factory.LedgeClimbState);
                return;
            }
            // else if(isTouchingLowWall) {
            //     stateMachine.ChangeState(player.Factory.QuickLedgeClimbState);
            //     return;
            // }
        }

        // if(InputHandler.IsJump && isTouchingLowWall){
        //     player.Factory.WallJumpState.DetermineJumpDirection(Sense.CheckIfTouchingWall());
        //     stateMachine.ChangeState(player.Factory.WallJumpState);
        //     return;
        // }

        // if jump button is pressed and can perform double jump
        if(InputHandler.IsJump && player.Factory.JumpState.CanJump()){ // double jump
            stateMachine.ChangeState(player.Factory.JumpState);
            return;
        }
        
        // if touching wall and player face toward the wall
        if((isTouchingWall || isTouchingLowWall) && xInput == Movement.FacingDirection){
            stateMachine.ChangeState(player.Factory.WallSlideState);
            return;
        }
        
        // else while in air state
        Movement.CheckIfShouldFlip(xInput, InputHandler.isFocus);
        Movement.SetVelocityX(data.movementSpeed * xInput);

        player.Animator.SetFloat("yVelocity", Movement.CurrentVelocity.y);
        player.Animator.SetFloat("xVelocity", 
            InputHandler.isFocus ? 
            Movement.CurrentVelocity.x * Movement.FacingDirection : System.Math.Abs(Movement.CurrentVelocity.x));
    }

    public void StartCoyote(){
        isInCoyoteTime = true;
        TimeManager.Instance.TimeOut(startTime + data.coyoteTime, 
            ()=>{
                if(isInCoyoteTime){
                    isInCoyoteTime = false;
                    player.Factory.JumpState.UseJumpEnergy();
                }
            }
        );
    }

}