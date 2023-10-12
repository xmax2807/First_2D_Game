using UnityEngine;

public class EnemyHurtState : EnemyState
{
    private EnemyState[] Variants;
    private int currentIndex;    
    public EnemyHurtState(EnemyEntity entity, string animName) : base(entity, animName)
    {
        Variants = new EnemyState[]{
            new EnemyLightHurt(entity, "lightHurt"),
            new EnemyHeavyHurt(entity,"heavyHurt"),
        };
    }

    public void Condition(float damageAmount){
        if(damageAmount > 5){
            currentIndex = 1;
        }
        else {
            currentIndex = 0;
        }
    }

    public override void Enter()
    {
        base.Enter();
        Variants[currentIndex].Enter();
    }

    public override void LogicUpdate()
    {
        if(isAnimationFinish){
            stateMachine.ChangeState(enemy.Factory.MovementState);
            return;
        }
        Variants[currentIndex].LogicUpdate();
    }
    public override void PhysicUpdate()
    {
        Variants[currentIndex].PhysicUpdate();
    }

    public override void Exit()
    {
        currentIndex = 0;
        Variants[currentIndex].Exit();
        base.Exit();
    }
}

public class EnemyLightHurt : EnemyState
{
    public EnemyLightHurt(EnemyEntity entity, string animName) : base(entity, animName)
    {
    }
}

public class EnemyHeavyHurt : EnemyState
{
    public EnemyHeavyHurt(EnemyEntity entity, string animName) : base(entity, animName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        //Movement.AddForce(25f/Time.fixedDeltaTime * Vector2.up);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isAnimationFinish){
            enemy.Factory.HurtState.Condition(0);
            stateMachine.ChangeState(enemy.Factory.HurtState);
            return;
        }
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        isAnimationFinish = Movement.CurrentVelocity.y < 0 && Sense.CheckIfTouchingGround();
    }
}