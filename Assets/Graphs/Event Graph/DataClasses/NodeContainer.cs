
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NodeContainer{
    [SerializeField]public List<string> nodeIDs;
    [SerializeField]public List<EventEdgeData> edges;

    public NodeContainer(){
        this.nodeIDs = new List<string>();
        this.edges = new List<EventEdgeData>();
    }
    
}