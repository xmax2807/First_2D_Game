using UnityEngine;

public class AutoCounter : Defense
{
    public AutoCounter(BaseAbility root,Core core, DefenseAbilityData data) : base(root,core, data)
    {
    }
    private float stateValue;
    public override void AbilityEnter()
    {
        core.Player.IsInvincible = true;
        core.Player.BeforeBeingAttacked += CounterEnemy;
        stateValue = 0f;
        root.SetFloat("transitionValue", stateValue);
        core.Movement.FreezeContraint(RigidbodyConstraints2D.FreezePositionX);
        base.AbilityEnter();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if(CanBeCanceled && core.InputHandler.IsAnyKeyPressed){
            OnFinishCallback?.Invoke();
        }
    }
    public override void ChangeState()
    {
        stateValue += 0.5f;
        CanBeCanceled = true;
        root.SetFloat("transitionValue", stateValue);
    }
    public override void AbilityExit()
    {
        base.AbilityExit();
        core.Player.IsInvincible = false;
        core.Player.BeforeBeingAttacked -= CounterEnemy;
        core.Movement.DefaultConstraints();
    }

    public void CounterEnemy(EnemyEntity attacker){
        root.SetFloat("transitionValue", 1f);
        attacker.Damage(core.Player,100f);
        //OnFinishCallback?.Invoke();
    }
}