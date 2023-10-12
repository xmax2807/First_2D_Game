using MilkShake;
using UnityEngine;

public class Block : Defense
{
    public Block(BaseAbility root,Core core, DefenseAbilityData data) : base(root,core, data)
    {
    }

    public override void AbilityEnter()
    {
        base.AbilityEnter();
        core.Movement.SetVectorZero();
        core.Player.IsBlocked = true;
        core.Player.DamageReceiveManager.OnDamageHandler += EvaluateDamage;
    }

    public override void AbilityExit()
    {
        base.AbilityExit();
        core.Player.IsBlocked = false;
        core.Player.DamageReceiveManager.OnDamageHandler -= EvaluateDamage;
    }
    public override void UpdateState()
    {
        root.SetFloat("transitionValue", Mathf.Clamp(combatInputState.HoldTime * 10,0f, 1f));
        base.UpdateState();
        if(combatInputState.GetInputState() == PlayerCombatInput.InputType.Release){
            OnFinishCallback?.Invoke();
        }
    }

    private float EvaluateDamage(ITargetable<Player> enemy, float amount){
        // Shaker.GlobalShakers[0].Shake(shakePreset);
        // TimeManager.Instance.SlowMotion(0.05f, 0.2f);
        AudioManager.Instance.PlayOneShot(data.blockSoundEffect);
        var heldTime = Time.time - combatInputState.startTime;
        if(heldTime <= data.Timing){
            
            Shaker.GlobalShakers[0].Shake(data.ShakeEffect);
            TimeManager.Instance.SlowMotion(0.5f, 0.2f);
            SpawnSystem.Instance.SpawnObject(data.DefenseEffect, core.Player.transform, true);

            core.UIBarComponent.UpdateValue(UIBarComponent.UIType.Gaurd, amount);
            
            return data.DamageReduction[0] * amount;
        }
        if(heldTime <= data.Timing + 0.25f){
            core.UIBarComponent.UpdateValue(UIBarComponent.UIType.Gaurd, amount * 0.5f);
            return data.DamageReduction[1] * amount;
        }
        else{
            core.UIBarComponent.UpdateValue(UIBarComponent.UIType.Gaurd, amount * 0.2f);
            return data.DamageReduction[2] * amount;
        }
    }
}
