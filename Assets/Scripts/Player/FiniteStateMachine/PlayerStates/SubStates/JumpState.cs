public class JumpState : AbilityState{
    private int jumpCountLeft;
    public JumpState(Player other, string otherAnimName) 
    : base(other, otherAnimName){
        jumpCountLeft = other.Data.jumpCount;
    }

    public override void Enter()
    {
        base.Enter();
        Movement.SetVelocityY(data.jumpForce);
        isAbilityDone = true;
        UseJumpEnergy();
    }

    public bool CanJump() => jumpCountLeft > 0;

    public void ResetJumpCount() => jumpCountLeft = data.jumpCount;
    public void UseJumpEnergy() => --jumpCountLeft;
}