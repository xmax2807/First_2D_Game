
public abstract class CoreBoolCondition : BoolCondition{
    public override bool CheckIsSatisfied(EventTrigger trigger)
    {
        if(trigger.GetType() != typeof(EventCombatTrigger)) {
           return false;
        }
        var combatTrigger = (EventCombatTrigger)trigger;
        return CheckCoreCondition(combatTrigger.Core);
    }
    protected abstract bool CheckCoreCondition(Core core);
}
