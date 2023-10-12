using UnityEngine;

public class DeathState : PlayerState
{
    public DeathState(Player other, string otherAnimName) : base(other, otherAnimName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        InputManager.Controls.GamePlay.Disable();
        Movement.SetVectorZero();
        Movement.AddForce(new Vector2(data.movementSpeed * -Movement.FacingDirection,data.movementSpeed) ,ForceMode2D.Impulse);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isAnimationFinish){
            Movement.SetVectorZero();
            TimeManager.Instance.WaitForSeconds(1f,GameManager.Instance.GameOver);
            isAnimationFinish = false;
        }
    }
}