using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

public abstract class EventNodeData : ScriptableObject, ISerializationCallbackReceiver{
    public Guid ID;
    public Vector2 Position;

    public abstract void Execute(EventTrigger trigger);
    [SerializeField] private string _idString;
    public void OnBeforeSerialize() => _idString = ID.ToString();
    public void OnAfterDeserialize() => Guid.TryParse(_idString, out ID);
}

public abstract class EventNodeData<T> : EventNodeData where T : EventItemDataNode{
    [SerializeField] protected List<T> items;
    public abstract string[] portNames {get;}
    public virtual bool IsFull() => false;

    public string[] GetCustomNames() {
        
        if(items == null) return null;
        
        var result= items.FirstOrDefault((item)=> item.CustomPortNames != null && item.CustomPortNames.Length > 0);
        return result?.CustomPortNames;
    }
}