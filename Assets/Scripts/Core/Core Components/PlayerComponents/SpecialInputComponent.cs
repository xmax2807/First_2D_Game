using UnityEngine;
public class SpecialInputComponent: PlayerCoreComponent{
    private EventTrigger trigger;
    private PlayerInputHandler inputHandler => Core.InputHandler;
    public PlayerActionData CurrentData {get;private set;} = null;
    public bool Attacking {get;private set;}
    protected override void Awake(){
        trigger = GetComponent<EventStartTrigger>();
    }

    public override void OnEnable(){
        
        TimeManager.Instance.WaitUntil(()=>inputHandler != null,()=> 
        inputHandler.OnAttackPress += trigger.Trigger);
    }
    public override void OnDisable(){
        inputHandler.OnAttackPress -= trigger.Trigger;
    }

    public void AttachCurrentAttackData(PlayerActionData data) {
        CurrentData = data;
        Attacking = true;
    }
    public void DettachCurrentAttackData() { 
        CurrentData = null;
        Attacking = false;
    }
}