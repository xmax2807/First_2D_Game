using System.Collections;
using System;
public class ReactionNode : EventNodeData<EventReaction>{
#if UNITY_EDITOR
    public bool isActive = false;
    public event Action<bool> OnActive;
#endif
    public const string OnCompletePort = "OnComplete";
    private string[] _portNames = new string[] {OnCompletePort};
    public override string[] portNames => _portNames;
    public override void Execute(EventTrigger trigger){
        trigger.StartCoroutine(React(trigger));
    }
    private IEnumerator React(EventTrigger trigger){
#if UNITY_EDITOR
    isActive = true;
    OnActive?.Invoke(isActive);
#endif
        var coroutines = items.ConvertAll(reaction => trigger.StartCoroutine(reaction.React(trigger)));

        foreach(var coroutine in coroutines){
            yield return coroutine;
        }

        var connections = trigger.GetConnectedNodes(this, OnCompletePort);
        
        foreach(var connection in connections){
            connection.Execute(trigger);
        
        }
#if UNITY_EDITOR
    isActive = false;
    OnActive?.Invoke(isActive);
#endif
    }

    public  EventNodeData clone()
    {
        // var newNode = new ReactionNode();
        // newNode.ID = this.ID;
        // newNode.Position = new Vector2(this.Position.x + 1, this.Position.y + 1);
        // items.CopyTo(newNode.items);
        return (ReactionNode)this.MemberwiseClone();
    }
}