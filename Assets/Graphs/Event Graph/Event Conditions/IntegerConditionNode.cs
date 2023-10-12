public class IntegerConditionNode : EventNodeData<EventInteger>{
    private string[] _portNames = new string[3] {"Less than", "Equal", "Greater than"};
    public override string[] portNames => _portNames;
    public override void Execute(EventTrigger trigger)
    {
        if(items == null || items.Count == 0) return;

        int portIndex = items[0].CheckOutputPort(trigger);
        if(portIndex > _portNames.Length) return; 

        var CustomNames = GetCustomNames();
        var connections = trigger.GetConnectedNodes(this, portNames == null? _portNames[portIndex] : CustomNames[portIndex]);
        
        foreach(var connection in connections) connection.Execute(trigger);
    }
    public override bool IsFull()=>items != null &&  items.Count == 1;
}