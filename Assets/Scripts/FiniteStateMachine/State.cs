using UnityEngine;
public class State{
    protected Entity entity;
    protected FiniteStateMachine stateMachine => entity.StateMachine;
    protected float startTime;
    public bool isAnimationFinish {get; protected set;}
    private string animationBoolName;

    protected bool isStateForceChanged = false; // when need to cancel all updates in current state => set to true

    public State(Entity other, string otherAnimName){
        entity = other;
        animationBoolName = otherAnimName;
    }

    public virtual void Enter(){
        Check();
        entity.Animator.SetBool(animationBoolName, true);
        startTime = Time.time;
        isAnimationFinish = false;
        isStateForceChanged = false;
    }

    public virtual void Exit(){
        entity.Animator.SetBool(animationBoolName, false);
        isStateForceChanged = true;
    }

    public virtual void LogicUpdate(){}

    public virtual void PhysicUpdate(){
        Check();
    }

    //Physical check (used for Enter and Physic Update methods)
    public virtual void Check(){}

    public virtual void AnimationTrigger(){}

    public virtual void AnimationFinishTrigger() => isAnimationFinish = true;
}