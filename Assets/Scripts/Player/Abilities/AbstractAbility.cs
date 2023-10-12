using System;

public abstract class AbstractAbility : IAbility{
    protected Core core;
    protected bool CanBeCanceled;
    public System.Action OnFinishCallback;
    protected BaseAbility root;
    public AbstractAbility(BaseAbility root,Core core){
        this.core = core;
        this.root = root;
    }
    public virtual void AbilityEnter(){CanBeCanceled = false;}

    public virtual void AbilityExit(){}

    public virtual bool CheckCanEnter() =>true;

    public virtual void FixedUpdateState(){}

    public virtual void ResetState(){}

    public virtual void UpdateState(){}
    public virtual void ChangeState(){}
}
public abstract class Speed : AbstractAbility
{
    protected SpeedAbilityData data;
    public Speed(BaseAbility root,Core core, SpeedAbilityData data) : base(root,core){
        this.data = data;
    }
}
public abstract class Defense : AbstractAbility
{
    protected DefenseAbilityData data;
    protected PlayerCombatInput combatInputState;
    protected Defense(BaseAbility root,Core core,DefenseAbilityData data) : base(root, core)
    {
        this.data = data;
    }
    public override void AbilityEnter()
    {
        base.AbilityEnter();
        combatInputState = new(UnityEngine.Time.time, false);
    }
    public override void UpdateState()
    {
        base.UpdateState();
        combatInputState.UpdateHoldTime(UnityEngine.Time.time);
        if(!core.AbilityInputHandler.IsAbilityPressed){
            combatInputState.Release();
            return;
        }
    }
}

public abstract class Magic : AbstractAbility
{
    protected MagicAbilityData data;
    protected float CurrentMana => core.UIBarComponent.GetCurrentValue(UIBarComponent.UIType.Mana);
    protected PlayerCombatInput combatInputState;
    protected Magic(BaseAbility root,Core core,MagicAbilityData data) : base(root, core)
    {
        this.data = data;
    }
    public override void AbilityEnter()
    {
        base.AbilityEnter();
        combatInputState = new(UnityEngine.Time.time, false);
    }
    public override void UpdateState()
    {
        base.UpdateState();
        combatInputState.UpdateHoldTime(UnityEngine.Time.time);
        if(!core.AbilityInputHandler.IsAbilityPressed){
            combatInputState.Release();
            return;
        }
    }
}