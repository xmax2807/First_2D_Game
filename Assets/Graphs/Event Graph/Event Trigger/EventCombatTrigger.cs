using UnityEngine;
public class EventCombatTrigger : EventStartTrigger{
    public CombatInputComponent CombatInput => Core.CombatInput;
    [SerializeField] public Core Core;

    void Awake(){
        Core = GetComponentInParent<Core>();
    }
}