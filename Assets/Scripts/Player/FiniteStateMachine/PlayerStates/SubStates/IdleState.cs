public class IdleState : GroundedState{
    public IdleState(Player other, string otherAnimName)
    :base(other, otherAnimName){}
    public override void Enter()
    {
        base.Enter();
        Movement.SetVelocityX(0f);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isStateForceChanged) return;
        
        if(InputHandler.isFocus){
            stateMachine.ChangeState(player.Factory.FocusState);
            return;
        }
        if(input.x != 0f){
            stateMachine.ChangeState(player.Factory.MovementState);
        }
    }
}