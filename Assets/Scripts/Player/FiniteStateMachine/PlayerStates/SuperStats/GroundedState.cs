using UnityEngine;

public class GroundedState : PlayerState{
    protected Vector2 input => InputHandler.Movement;
    private bool isGrounded;
    
    public GroundedState(Entity other, string otherAnimName)
    :base(other, otherAnimName){}

    public override void Check()
    {
        base.Check();
        isGrounded = Sense.CheckIfTouchingAnyGroundSide();
    }

    public override void Enter()
    {
        base.Enter();
        player.Factory.JumpState.ResetJumpCount();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isStateForceChanged) return;
        
        if(CombatInput.Attacking){
            stateMachine.ChangeState(player.Factory.AttackState);
            return;
        }
        if(AbilityInputHandler.IsAbilityPressed && player.Factory.SpecialAbilityState.CanDoAbility()){
            stateMachine.ChangeState(player.Factory.SpecialAbilityState);
            return;
        }
        
        if(InputHandler.IsJump && player.Factory.JumpState.CanJump()){
            InputHandler.JumpPressed();
            stateMachine.ChangeState(player.Factory.JumpState);
            return;
        }
        if(!isGrounded){
            player.Factory.InAirState.StartCoyote();
            stateMachine.ChangeState(player.Factory.InAirState);
            return;
        }
    }
}