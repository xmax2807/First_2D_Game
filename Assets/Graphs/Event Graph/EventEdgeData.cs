using System;
using UnityEngine;
[Serializable]
public class EventEdgeData : ISerializationCallbackReceiver{
    public Guid OutputId;
    public Guid InputId;
    public string PortName;

    [SerializeField] private string outputIdString;
    [SerializeField] private string inputIdString;

    public void OnBeforeSerialize(){
        outputIdString = OutputId.ToString();
        inputIdString = InputId.ToString();
    }

    public void OnAfterDeserialize(){
        Guid.TryParse(outputIdString, out OutputId);
        Guid.TryParse(inputIdString, out InputId);
    }
}