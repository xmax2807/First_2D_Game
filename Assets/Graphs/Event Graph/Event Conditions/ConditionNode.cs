public class ConditionNode : EventNodeData<EventCondition>{

    public const string TruePort = "True";
    public const string FalsePort = "False";
    private string[] _portNames = new string[]{TruePort, FalsePort};
    public override string[] portNames => _portNames;
    private bool CheckIsSatisfiedAll(EventTrigger trigger) => items.TrueForAll(condition => condition.CheckIsSatisfied(trigger));
    public override void Execute(EventTrigger trigger){
        var portName = CheckIsSatisfiedAll(trigger) ? TruePort : FalsePort;
        var connections = trigger.GetConnectedNodes(this, portName);
        
        foreach(var connection in connections) connection.Execute(trigger);
    }

    public  EventNodeData clone()
    {
        return (ConditionNode) this.MemberwiseClone();
    }
}