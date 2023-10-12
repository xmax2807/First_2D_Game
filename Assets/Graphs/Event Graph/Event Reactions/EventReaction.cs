using System.Collections;
public abstract class EventReaction : EventItemDataNode{
    public abstract IEnumerator React(EventTrigger trigger);
}