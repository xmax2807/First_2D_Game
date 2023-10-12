using UnityEngine;
public class CombatInputComponent: PlayerCoreComponent{
    private EventTrigger trigger;
    private PlayerInputHandler inputHandler => Core?.InputHandler;
    public PlayerActionData currentData {get;private set;} = null;
    public bool Attacking {get;private set;}
    protected override void Awake(){
        base.Awake();
        trigger = GetComponent<EventStartTrigger>();
    }
    // private void Start(){
    //     inputHandler.OnAttackPress += trigger.Trigger;
    // }
    public override void OnEnable(){
        inputHandler.OnAttackPress += trigger.Trigger;
    }
    public override void OnDisable(){
        if(inputHandler == null) return;
        inputHandler.OnAttackPress -= trigger.Trigger;
    }
    public void AttachCurrentAttackData(PlayerActionData data) {
        currentData = data;
        Attacking = true;
    }
    public void DettachCurrentAttackData() { 
        currentData = null;
        Attacking = false;
    }
}