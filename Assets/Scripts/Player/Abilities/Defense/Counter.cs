using UnityEngine;

public class Counter : Defense
{
    private float enemyReadyAttackTime;
    private float currentSpeed, startTime;
    private EnemyEntity target;
    private AnimationCurve curve;
    public Counter(BaseAbility root,Core core, DefenseAbilityData data) : base(root,core, data)
    {
        core.Player.BeforeBeingAttacked += (EnemyEntity attacker)=>{
            enemyReadyAttackTime = Time.time;
            target = attacker;
        };
        float duration = data.CounterDistance/300f;
        curve = new AnimationCurve(new Keyframe(0,1),new Keyframe(0.1f * duration,1), new Keyframe(duration,0.01f));
    }
    public override bool CheckCanEnter()
    {
        return Time.time - enemyReadyAttackTime <= data.Timing;
    }
    public override void AbilityEnter()
    {
        startTime = 0;
        base.AbilityEnter();
        core.Player.IsInvincible = true;
    
        target.Damage(core.Player, data.PerfectCounterPlus + core.UIBarComponent.GetCurrentValue(UIBarComponent.UIType.Gaurd));
        core.UIBarComponent.SetValue(UIBarComponent.UIType.Gaurd, 0f);

        core.Movement.TemporaryDisableCollider(0.1f);
        core.Movement.ToggleBlockDirection(true);
        TimeManager.Instance.SlowMotion(0.04f, 0.1f);

        currentSpeed = 500f * core.Movement.FacingDirection;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        core.Movement.SetVelocityX(currentSpeed * curve.Evaluate(startTime + Time.deltaTime));
    }
    public override void AbilityExit()
    {
        base.AbilityExit();
        core.Player.IsInvincible = false;
        core.Movement.ToggleBlockDirection(false);
    }
}
