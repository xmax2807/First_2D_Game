public interface IStateFactory{
    void CreateStates();
}

#region PlayerFactory
public class PlayerStateFactory : IStateFactory{
    #region State Variables
    public IdleState IdleState{get;private set;}
    public FocusState FocusState{get;private set;}
    public MovementState MovementState{get;private set;}
    public HurtState HurtState{get;private set;}
    public DeathState DeathState {get;private set;}
    public JumpState JumpState {get;private set;}
    public InAirState InAirState {get;private set;}
    public LandState LandState {get;private set;}
    public WallSlideState WallSlideState {get;private set;}
    public WallGrabState WallGrabState{get;private set;}
    public WallClimbState WallClimbState {get; private set;}
    public WallJumpState WallJumpState {get;private set;}
    public LedgeClimbState LedgeClimbState {get; private set;}
    //public QuickLedgeClimbState QuickLedgeClimbState {get; private set;}
    public AttackState AttackState {get;private set;}
    public SpecialAbilityState SpecialAbilityState {get;private set;}
    #endregion
    private readonly Player host;
    public PlayerStateFactory(Player host){
        this.host = host;
        CreateStates();
    }

    public void CreateStates()
    {
        // init States
        IdleState = new IdleState(host, "idle");
        FocusState = new FocusState(host, "focus");
        MovementState = new MovementState(host, "moving");
        HurtState = new HurtState(host, "hurt");
        DeathState = new DeathState(host, "die");
        JumpState = new JumpState(host, "inAir");
        InAirState = new InAirState(host, "inAir");
        LandState = new LandState(host, "land");
        WallSlideState = new WallSlideState(host, "wallSlide");
        WallGrabState = new WallGrabState(host, "wallGrab");
        WallClimbState = new WallClimbState(host, "wallClimb");
        WallJumpState = new WallJumpState(host, "inAir");
        LedgeClimbState = new LedgeClimbState(host, "ledgeClimbState");
        //QuickLedgeClimbState = new QuickLedgeClimbState(host, "ledgeClimbState");
        AttackState = new AttackState(host,host.Weapons[0], "attackState");
        SpecialAbilityState = new SpecialAbilityState(host, "attackState");
    }
}
#endregion

#region EnemyFactory
public abstract class EnemyFactory : IStateFactory
{
    #region State Variables
    public EnemyMovementState MovementState {get;protected set;}
    public EnemyIdleState IdleState {get;protected set;}
    public EnemyHurtState HurtState {get;protected set;}
    public EnemyApproachState ApproachState {get;protected set;}
    public EnemyAttackState AttackState {get;protected set;}
    public EnemyDeathState DeathState {get;protected set;}
    #endregion
    protected EnemyEntity host;
    public EnemyFactory(EnemyEntity host){
        this.host = host;
        CreateStates();
    }
    
    public virtual void CreateStates(){}
    
    public enum ApproachType{
        None, Teleport, Run
    }
    public void CreateApproachState(ApproachType type){
        switch(type){
            case ApproachType.Teleport: 
                ApproachState = new TeleportApproachState(host, "approach");
                break;
        }
    }
}
public class PatrolEnemyFactory : EnemyFactory
{
    public PatrolEnemyFactory(EnemyEntity host) : base(host)
    {
    }
    public override void CreateStates()
    {
        base.CreateStates();
        MovementState = new EnemyPatrolState(host, "moving");
        IdleState = new EnemyIdleState(host, "idle");
        HurtState = new EnemyHurtState(host, "hurt");
        DeathState = new EnemyDeathState(host, "die");
    }
}
public class AttackEnemyFactory : PatrolEnemyFactory{
    public AttackEnemyFactory(EnemyEntity host) : base(host)
    {
    }
    public override void CreateStates()
    {
        base.CreateStates();
        AttackState = new EnemyAttackState(host, "attack");
    }
}
#endregion