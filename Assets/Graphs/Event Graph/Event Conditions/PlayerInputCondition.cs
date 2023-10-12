using System;
using UnityEngine;
public class PlayerInputCondition : EventInteger{
    public enum DirectionX {
        Backward = 0,  None = 1, Forward = 2
    }
    [SerializeField] private DirectionX directionX = DirectionX.None;

    public override string Name => "Player Input";
    public override string[] CustomPortNames => Enum.GetNames(typeof(DirectionX));

    public override int CheckOutputPort(EventTrigger trigger){
        if(trigger.GetType() != typeof(EventCombatTrigger)) {
            throw new System.TypeLoadException();
        }
        var combatTrigger = (EventCombatTrigger)trigger;
        int result = combatTrigger.Core.Movement.FacingDirection * (int)combatTrigger.Core.InputHandler.Movement.x + 1;
        return result;
    }
}