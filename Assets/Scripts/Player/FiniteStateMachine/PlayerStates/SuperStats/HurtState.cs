using UnityEngine;
public class HurtState : PlayerState{

    protected PlayerState[] HurtStates;
    public HurtState(Player other, string otherAnimName)
    :base(other,otherAnimName){
        HurtStates = new PlayerState[]{
            new LightHurt(other, "lightHurt"),
            new HeavyHurt(other, "heavyHurt")
        };
    }

    protected int currentHurtState;
    public bool CheckCanEnter() => EvaluateDamage() >= 0;
    public override void Enter()
    {
        base.Enter();
        Movement.SetVectorZero();
        HurtStates[currentHurtState].Enter();
    }

    private int EvaluateDamage(){
        StabilityState stabilityState = player.DamageReceiveManager.GetInterruptState();
        switch(stabilityState){
            case StabilityState.Broken: currentHurtState = 1; break;
            case StabilityState.Interrupted: currentHurtState = 0; break;
            case StabilityState.Stable: currentHurtState = -1; break; 
        }
        return currentHurtState;
    }

    public override void LogicUpdate(){
        if(isAnimationFinish){
            stateMachine.ChangeState(player.Factory.IdleState);
            return;
        }
        HurtStates[currentHurtState].LogicUpdate();
    }
    public override void PhysicUpdate()
    {
        HurtStates[currentHurtState].PhysicUpdate();
    }
    public override void Exit()
    {
        base.Exit();
        HurtStates[currentHurtState].Exit();
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        HurtStates[currentHurtState].AnimationFinishTrigger();
    }
}