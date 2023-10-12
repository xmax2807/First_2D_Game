using System.Collections;
using UnityEngine;
public class DebugReaction : EventReaction{
    [SerializeField] string DebugMessage;

    public override string Name => "Debug";

    public override IEnumerator React(EventTrigger trigger)
    {
        Debug.Log(DebugMessage);
        yield break;
    }
}