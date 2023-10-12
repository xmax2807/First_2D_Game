public abstract class BoolCondition : EventCondition{
    public bool IsTrue;
    public override string Name => "Bool COndition";
    public override bool CheckIsSatisfied(EventTrigger trigger) => true;
}

