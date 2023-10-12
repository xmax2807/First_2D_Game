using UnityEngine;
public class QuickLedgeClimbState : PlayerState{
    public QuickLedgeClimbState(Player other, string otherAnimName)
    : base(other, otherAnimName){}

    public override void Enter(){
        base.Enter();

        var cornerPosition = Sense.DetermineCornerPositionLow();
        player.transform.position = new Vector2(cornerPosition.x - (Movement.FacingDirection * data.StartOffset.x), cornerPosition.y - data.StartOffset.y * 0.8f);
        
        player.Animator.SetBool("ledgeClimbQuick",true); 
    }
    public override void LogicUpdate(){
        base.LogicUpdate();
        
        if(isStateForceChanged) return;

        if(isAnimationFinish) {
            stateMachine.ChangeState(player.Factory.IdleState);
            return;
        }
        Movement.SetVelocityY(data.QuickClimbForceMultiplier*Time.deltaTime);
        player.Animator.SetFloat("yVelocity", Movement.CurrentVelocity.y * 3f);
        Movement.SetVelocityX(2f*Movement.FacingDirection);
    }

    public override void AnimationFinishTrigger(){
        base.AnimationFinishTrigger();
        player.Animator.SetBool("ledgeClimbQuick",false);
        
    }
}