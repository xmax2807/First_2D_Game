using UnityEngine;
public class LedgeClimbState : PlayerState{
    private Vector2 cornerPosition;
    private Vector2 startPosition;
    private Vector2 stopPosition;
    private bool isHanging;
    private bool isClimbing;
    
    public LedgeClimbState(Player other, string otherAnimName) 
    : base(other, otherAnimName){}

    public override void Enter(){
        base.Enter();

        Movement.SetVectorZero();
        cornerPosition = Sense.DetermineCornerPosition();
        startPosition.Set(cornerPosition.x - (data.StartOffset.x * Movement.FacingDirection), cornerPosition.y - data.StartOffset.y);
    }

    public override void Exit(){
        base.Exit();
        isHanging = false;
        isClimbing = false;
    }

    public override void LogicUpdate(){
        base.LogicUpdate();
        if(isStateForceChanged) return;

        if(isAnimationFinish){
            stateMachine.ChangeState(player.Factory.IdleState);
            return;
        }
        
        if(!isClimbing){ // is holding the ledge
            Movement.SetVectorZero();
            player.transform.position = startPosition;
        }
        else{ // is climbing over the ledge
            player.Animator.SetFloat("yVelocity", Movement.CurrentVelocity.y *1.5f);
            Movement.SetVelocityX(2 * Movement.FacingDirection);
        }
        
        if(!isHanging || isClimbing) return; // decide to fall off or climb over the ledge

        if(InputHandler.Movement.x == Movement.FacingDirection){ // Command player to climb
            isClimbing = true;
            player.Animator.SetBool("ledgeClimb", true);
            Movement.SetVelocityY(data.jumpForce);
        }
        else if(InputHandler.IsJump){
            player.Factory.WallJumpState.DetermineJumpDirection(true);
            stateMachine.ChangeState(player.Factory.WallJumpState);
        }
        else if(InputHandler.Movement.y == -1 || InputHandler.Movement.x != 0){// command player to fall
            stateMachine.ChangeState(player.Factory.InAirState);
        }
    }

    public override void AnimationTrigger(){
        base.AnimationTrigger();
        isHanging = true;
    }

    public override void AnimationFinishTrigger(){
        base.AnimationFinishTrigger();
        player.Animator.SetBool("ledgeClimb", false);
    }
}