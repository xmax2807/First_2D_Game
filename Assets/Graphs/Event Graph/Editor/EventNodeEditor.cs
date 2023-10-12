using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class EventNodeEditor : Node
{
    public Guid ID;

    public void CreatePorts<T,V>(T Node, Direction direction, Port.Capacity capacity) where T : EventNodeData<V> where V : EventItemDataNode{
        
        var list = Node.GetCustomNames();
        if(list == null) list = Node.portNames;
        foreach(string portName in list){
            var port = InstantiatePort(Orientation.Horizontal, direction, capacity, typeof(float));
            port.portName = portName;
            outputContainer.Add(port);
        }
    }

    public T GetUIElement<T>() where T : VisualElement{
        var children = extensionContainer.Children();
        return (T)children.FirstOrDefault((element)=> element.GetType() == typeof(T));
    }
}
