using UnityEngine;
public class WallJumpState : AbilityState{
    
    private int Direction;
    public WallJumpState(Player other, string otherAnimName)
    : base(other, otherAnimName){}

    public override void Enter(){
        base.Enter();
        
        player.Factory.JumpState.ResetJumpCount();
        Movement.SetVelocity(data.WallJumpVelocity,data.WallJumpAngle, Direction);
        Movement.CheckIfShouldFlip(Direction);
        player.Factory.JumpState.UseJumpEnergy();
    }

    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isStateForceChanged) return;
        
        player.Animator.SetFloat("yVelocity", Movement.CurrentVelocity.y);
        player.Animator.SetFloat("xVelocity", System.Math.Abs(Movement.CurrentVelocity.x));

        if(Time.time >= startTime + data.WallJumpTime){
            isAbilityDone = true;
        }
    }

    public void DetermineJumpDirection(bool isTouchingWall){
        Direction = isTouchingWall? Movement.FacingDirection*-1 : Movement.FacingDirection;
    }
}