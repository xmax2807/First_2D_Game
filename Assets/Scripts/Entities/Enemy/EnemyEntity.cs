using UnityEngine;
public abstract class EnemyEntity : Entity, ITargetable<Player>, IVisibleObject {
    #region Serialize Field
    public EnemyWeapon[] Weapons;
    public AttackCaution AttackCaution;
    [SerializeField] protected ScriptableDropItem dropItem;
    #endregion
    public abstract EnemyFactory Factory {get;}
    public abstract bool CanAttack {get;}
    public GameObject AliveGameObject {get;private set;}
    public EnemyStatData Data => (EnemyStatData)_data;
    public ITargetable<EnemyEntity> Player {get;private set;}
    public bool IsInvisible => false;
    [HideInInspector]public bool IsDead;
    public EnemyCore Core => (EnemyCore)_core;

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyFiniteStateMachine();
        Animator = GetComponent<Animator>();
    }
    protected override void Start()
    {
        base.Start();
        //AliveGameObject = transform.Find("Alive").gameObject;
        StateMachine.Init(Factory.IdleState);
        ToggleVisual(false);
    }

    public void AttachPlayer(ITargetable<EnemyEntity> target){
        Player = target.IsInvisible ? null : target;
    }
    public void DettachPlayer()=>Player = null;

    public Vector2 GetPosition() => this == null ? Vector2.zero : transform.position;
    

    public virtual void Damage(float amount)
    {
        Core.UIComponent.Damage(amount);
        if(StateMachine.CurrentState == Factory.DeathState) return;
        
        if(StateMachine.CurrentState == Factory.HurtState) return;
        Factory.HurtState.Condition(amount);
        StateMachine.ChangeState(Factory.HurtState);

    }

    public virtual void Damage(Player enemy, float amount)
    {
        if(IsDead) return;
        Damage(amount);
    }
    public void OnBeforeTakingDamage(Player attacker){}

    public void ToggleVisual(bool value)
    {
        Animator.enabled = value;
        Core.gameObject.SetActive(value);
        enabled = value;
    }
    public void DestroySelf(){
        //Drop Level
        if(dropItem != null){
            GameObject expObj = Instantiate(dropItem.Item, transform.position, Quaternion.identity); 
            if(expObj.TryGetComponent<ExpItem>(out var expItem)){
                expItem.Init(Core.EnemyStat.Exp);
            }
        }

        Destroy(this.gameObject);
    }
}