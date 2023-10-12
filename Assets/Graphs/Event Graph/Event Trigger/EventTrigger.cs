using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Linq;
public abstract class EventTrigger : MonoBehaviour{
    
    [SerializeField] private string _name;
    public string Name => _name;

    public List<EventNodeData> nodes = new();
    public List<EventEdgeData> edges = new();
    private bool IsRunning => CheckIsRunning();
    public void Trigger(){
        CheckIsRunning();
        if(IsRunning) return;
        foreach(var node in GetRootNodes()) {
            node.Execute(this);
        }
    }

    private List<EventNodeData> GetRootNodes() {
        var edgeData = edges.FindAll(edge => edge.OutputId == Guid.Empty);
        return nodes.FindAll(node => edgeData.Exists(e=> e.InputId == node.ID));
    }
    
    public List<EventNodeData> GetConnectedNodes(EventNodeData node, string portName){
        var edgeData = edges.FindAll(edge => edge.OutputId == node.ID && edge.PortName == portName);
        return nodes.FindAll(node=> edgeData.Exists(e=> e.InputId == node.ID));
    }

    private bool CheckIsRunning(){
        #if UNITY_EDITOR
        return nodes.FindAll(node => node is ReactionNode reaction && reaction.isActive).Count > 0;
        #else
        return false;
        #endif
    }

    public bool CheckExistedNode(EventNodeData data){
        return nodes.FirstOrDefault((node) => data.ID == node.ID) != null;
    }
}