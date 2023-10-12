using UnityEngine;
public class PlayerState : State{
    protected PlayerData data => player.Data;
    protected Player player => (Player)entity;
    protected PlayerInputHandler InputHandler => player.Core.InputHandler;
    protected PlayerAbilityInputHandler AbilityInputHandler => player.Core.AbilityInputHandler;
    private PlayerCollisionSense _sense;
    protected PlayerCollisionSense Sense { 
        get {
            if(_sense == null) _sense =(PlayerCollisionSense)player.Core.Senses;
            return _sense;
        }
    }

    protected Movement Movement => player.Core.Movement;
    protected CombatInputComponent CombatInput => player.Core.CombatInput;

    //Constructor
    public PlayerState(Entity entity, string otherAnimName) : base(entity, otherAnimName){}

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isStateForceChanged) return;
        
    }

    public override void Check()
    {
        if(InputHandler.isFocus){
            player.Factory.FocusState.Check();
            return;
        }
        base.Check();
    }
}