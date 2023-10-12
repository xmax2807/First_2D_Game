public abstract class EventCondition : EventItemDataNode{
    public abstract bool CheckIsSatisfied(EventTrigger trigger);
}

public abstract class EventInteger : EventItemDataNode{
    public abstract int CheckOutputPort(EventTrigger trigger);
}