using UnityEngine;
public class SpecialAbilityState : AbilityState{
    protected BaseAbility currentAbility;
    BaseAbility ability => player.Core.AbilityHandler.ActiveAbility;
    public SpecialAbilityState(Entity other, string animName) : base(other, animName){}
    public override void LogicUpdate(){
        base.LogicUpdate();
        currentAbility.UpdateState();

        // if(currentAbility != ability && !isAbilityDone){
        //     return;
        // }
        // //currentAbility = ability;
    }
    public override void Enter()
    {
        base.Enter();
        currentAbility = ability;        
        currentAbility.OnFinishCallback += AnimationFinishTrigger;
        currentAbility.AbilityEnter();
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        currentAbility.FixedUpdateState();
    }
    public override void Exit()
    {
        base.Exit();
        currentAbility.OnFinishCallback -= AnimationFinishTrigger;
        currentAbility.AbilityExit();
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }
    public override bool CanDoAbility() => ability.CheckCanEnter();
}