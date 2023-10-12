using UnityEngine;
public class FocusState : PlayerState
{
    protected Vector2 input => InputHandler.Movement;
    protected ITargetable<Player> FocusingTarget;
    private int EnemyDirection;
    public FocusState(Player other, string otherAnimName)
    : base(other, otherAnimName) { }

    public override void Enter()
    {
        base.Enter();
        FocusingTarget = player.Core.VisibilityComponent.GetClosestEnemy();
    }
    public override void Exit()
    {
        base.Exit();
        player.Core.VisibilityComponent.GiveUpEnemy();
        FocusingTarget = null;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isStateForceChanged) return;

        Movement.SetVelocityX(data.walkSpeed * input.x);
        player.Animator.SetFloat("xVelocity", (int)input.x * EnemyDirection + 1);

        if (!InputHandler.isFocus)
        {
            stateMachine.ChangeState(player.Factory.IdleState);
            return;
        }
        if (InputHandler.IsJump)
        {
            stateMachine.ChangeState(player.Factory.JumpState);
            return;
        }
        if (CombatInput.Attacking)
        {
            stateMachine.ChangeState(player.Factory.AttackState);
            return;
        }
        if (AbilityInputHandler.IsAbilityPressed && player.Factory.SpecialAbilityState.CanDoAbility())
        {
            stateMachine.ChangeState(player.Factory.SpecialAbilityState);
        }
    }

    public override void Check()
    {
        FocusingTarget = player.Core.VisibilityComponent.GetClosestEnemy();

        if (FocusingTarget == null)
        {
            EnemyDirection = Movement.FacingDirection;
            player.Core.VisibilityComponent.GiveUpEnemy();
            return;
        }
        EnemyDirection = FocusingTarget.GetPosition().x - player.GetPosition().x > 0 ? 1 : -1;
        Movement.CheckIfShouldFlip(EnemyDirection);
        player.Core.VisibilityComponent.FocusEnemy();
    }
}