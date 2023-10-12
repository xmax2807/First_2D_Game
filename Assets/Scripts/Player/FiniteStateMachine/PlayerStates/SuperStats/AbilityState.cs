using UnityEngine;
public class AbilityState : PlayerState{
    protected bool isAbilityDone;
    protected bool isGrounded;
    public AbilityState(Entity other, string otherAnimName) 
    : base(other, otherAnimName){}

    public override void Check()
    {
        base.Check();
        isGrounded = Sense.CheckIfTouchingGround();
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isStateForceChanged || !isAbilityDone) return;
    

        if(isGrounded && Movement.CurrentVelocity.y < 0.01f){
            stateMachine.ChangeState(player.Factory.IdleState);
        }
        else{
            stateMachine.ChangeState(player.Factory.InAirState);
        }
    }

    public virtual bool CanDoAbility() => true;
}