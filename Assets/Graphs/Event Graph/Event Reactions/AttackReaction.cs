using System.Collections;
using UnityEngine;
public class AttackReaction : EventReaction{
    [SerializeField]PlayerActionData ActionData;
    [SerializeField] public string _name = "";
    public override string Name => _name;

    public override IEnumerator React(EventTrigger trigger)
    {
        var attackTrigger =  trigger as EventCombatTrigger;
        if(attackTrigger != null) {
            attackTrigger.CombatInput.AttachCurrentAttackData(ActionData);
        }

        yield break;
    }
}