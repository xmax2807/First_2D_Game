using UnityEngine;
public class Player : Entity, ITargetable<EnemyEntity>, ISavableData
{   
    #region  Components
    public Core Core => (Core)_core;
    public PlayerStateFactory Factory {get;private set;}
    public PlayerData Data => (PlayerData) _data;
    public PlayerAudioPreset AudioPreset=> (PlayerAudioPreset)_audioPreset;
    public PlayerWeapon[] Weapons;
    public bool IsInvincible = false;
    public bool IsBlocked = false;
    public event System.Action<float> OnDamage;
    public event System.Action<EnemyEntity> BeforeBeingAttacked;
    public DamageReceiveManager<Player> DamageReceiveManager {get;private set;}
    #endregion
    protected override void Awake(){
        base.Awake();
        Animator = GetComponent<Animator>();
        Factory = new PlayerStateFactory(this);
        DamageReceiveManager = new (new PlayerStability(200,0.9f));

        var canvas = GetComponentInChildren<Canvas>(); 
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "Forward";
        canvas.sortingOrder = 50;
    }
    
    protected override void Start(){
        base.Start();
        StateMachine.Init(Factory.IdleState);
    }

    [SerializeField] public Transform MassCenter;
    public Vector2 GetPosition() => MassCenter.position;
    public Vector2 GroundPosition() => Core.Senses.GetGroundTransform().position;
    public void Damage(float amount){
        OnDamage?.Invoke(amount);
        if(IsInvincible) return;

        Core.UIBarComponent.UpdateValue(UIBarComponent.UIType.HP, -amount);
        if(Core.UIBarComponent.GetCurrentValue(UIBarComponent.UIType.HP) <= 0f){
            if(StateMachine.CurrentState != Factory.DeathState){
                StateMachine.ChangeState(Factory.DeathState);
            }
            return;
        }

        if(IsBlocked) return;
        
        if(!Factory.HurtState.CheckCanEnter()) return;

        SpawnSystem.Instance.SpawnBlood(this.transform);
        StateMachine.ChangeState(Factory.HurtState);
    }
    public void Damage(EnemyEntity enemy,float amount){
        // get effect
        Damage(DamageReceiveManager.EvaluateDamage(enemy, amount, isStackable: false));
    }
    public bool IsInvisible => false;

    public void LoadData(GameData data, bool isRestartLevel)
    {
        if(!isRestartLevel){
            this.transform.position = data.PlayerPosition;
        }
        Core.Stats = data.PlayerStats;
        Core.PlayerGameData.Data = data.PlayerInGameInfo;
        Core.UIBarComponent.LoadData();
    }

    public void SaveData(GameData data, bool isSceneLoad)
    {
        if(this == null) return;
        if(!isSceneLoad){
            data.PlayerPosition = this.transform.position;
        }

        Core.UIBarComponent.SaveData();
        data.PlayerInGameInfo = this.Core.PlayerGameData.Data;
        data.PlayerStats = Core.Stats;
    }

    public void OnBeforeTakingDamage(EnemyEntity attacker)
    {
        BeforeBeingAttacked?.Invoke(attacker);
    }
    public void BeforeBeingAttackedCollided(Collider2D collider){
        var weapon = collider.transform.parent;
        if(weapon == null || !weapon.TryGetComponent<EnemyWeapon>(out var enemyWeapon)) return;
        enemyWeapon.ReadyToAttack();
        
    }

    public void UpdateUIComponent(UIBarComponent.UIType type, float amount){
        Core.UIBarComponent.UpdateValue(type,amount);
    }
}
