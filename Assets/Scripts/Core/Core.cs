
using UnityEngine;
public class Core : BaseCore{
    public CombatInputComponent CombatInput {get;private set;}
    public PlayerInputHandler InputHandler {get;private set;}
    public UIBarComponent UIBarComponent {get;private set;}
    public PoolingComponent PoolingComponent {get;private set;}
    public PlayerAbilityInputHandler AbilityInputHandler => AbilityHandler.InputHandler;
    public AbilityHandler AbilityHandler => abilityHandler;
    //[SerializeField] public CollisionSenseData CollisionData;
    [SerializeField] private AbilityHandler abilityHandler;
    public ScriptablePlayerLevelData PlayerGameData;
    public VisibilityComponent VisibilityComponent;
    public Player Player {get;private set;}
    
    [SerializeField] protected PlayerEntityData Data;
    public PlayerStat Stats { get=>Data.Stat; set=>Data.Stat = value;}
    protected override void Awake(){
        base.Awake();
        InputHandler = GetComponentInChildren<PlayerInputHandler>();
        // InputHandler = _playerInputHandler;
        CombatInput = GetComponentInChildren<CombatInputComponent>();
        PoolingComponent = GetComponentInChildren<PoolingComponent>();
        Player = GetComponentInParent<Player>();
        UIBarComponent = GetComponentInChildren<UIBarComponent>();
        VisibilityComponent = GetComponentInChildren<VisibilityComponent>();
        if(Movement == null){
           Debug.Log("Missing component(s) in the code");
        }
    }
}