using UnityEngine;
using System.Linq;

public class SpeedAbility : BaseAbility{
    [SerializeField] protected SpeedAbilityData data;
    private AbstractAbility[] _variants;

    protected override AbstractAbility[] Variants => _variants;

    public override string GetName => "Speed";

    protected override void Start()
    {
        base.Start();
        _variants = new Speed[]{
            new Evade(this,core, data),
            new Dash(this,core, data),
            new Teleport(this,core, data),
        };
    }
    public override void AbilityEnter()
    {
        core.Player.OnDamage += PerfectDodge;
        core.Player.IsInvincible = true;
        base.AbilityEnter();
    }
    public override void AbilityExit()
    {
        base.AbilityExit();
        core.Player.IsInvincible = false;
        core.Player.OnDamage -= PerfectDodge;
    }

    private void PerfectDodge(float amount){
        if(Time.time - combatInputState.startTime <= data.DodgeTiming){
            TimeManager.Instance.SlowMotion(0.05f, 0.5f);
        }
    }
}