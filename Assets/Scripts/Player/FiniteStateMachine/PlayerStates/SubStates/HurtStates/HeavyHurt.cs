using UnityEngine;

public class HeavyHurt : PlayerState
{
    public HeavyHurt(Player other, string otherAnimName) : base(other, otherAnimName)
    {
    }
    private float Force = 10f;
    public override void Enter()
    {
        base.Enter();
        Movement.AddForce(new Vector2(Force * -Movement.FacingDirection, Force),ForceMode2D.Impulse);
        player.IsInvincible = true;
    }
    public override void Exit()
    {
        base.Exit();
        player.IsInvincible = false;
    }
}