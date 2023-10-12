using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using ExtensionMethods;
using bf = System.Reflection.BindingFlags;

public class EventGraphView : GraphView{
    private readonly EventTrigger _eventTrigger;
    private readonly EventGraphEditor _editorWindow;
    private static readonly Vector2 DefaultNodeSize = new Vector2(120,200);

    private Edge[] Edges => edges.ToArray();
    private EventNodeEditor[] Nodes => nodes.Cast<EventNodeEditor>().ToArray();

    public EventGraphView(EventGraphEditor window, EventTrigger trigger){
        this._editorWindow = window;
        _eventTrigger = trigger;

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(new ContentZoomer());

        var grid = new GridBackground();
        Insert(0, grid);
        

        var searchWindow = ScriptableObject.CreateInstance<EventNodeSearchWindow>();
        searchWindow.Initialize(_editorWindow,this, _eventTrigger);
        nodeCreationRequest = ctx => SearchWindow.Open(new SearchWindowContext(ctx.screenMousePosition), searchWindow);

        LoadGraph();
        graphViewChanged = OnGraphChanged;
    }

    private GraphViewChange OnGraphChanged(GraphViewChange graphViewChange){
        if(graphViewChange.edgesToCreate != null){
            foreach(var edge in graphViewChange.edgesToCreate){
                var outputNode = (EventNodeEditor) edge.output.node;
                var inputNode = (EventNodeEditor) edge.input.node;
                var portName = edge.output.portName;
                
                if(_eventTrigger.edges.Exists(e => 
                    e.OutputId == outputNode.ID && 
                    e.InputId == inputNode.ID &&
                    e.PortName == portName)
                ) continue;

                _eventTrigger.edges.Add(new EventEdgeData(){
                    OutputId = outputNode.ID,
                    PortName = portName,
                    InputId = inputNode.ID
                });
            }
        }

        if(graphViewChange.elementsToRemove != null){
            foreach(var ele in graphViewChange.elementsToRemove){
                if(ele.GetType() == typeof(EventNodeEditor)){
                    var id = ((EventNodeEditor) ele).ID;
                    _eventTrigger.nodes.RemoveAll(n => n.ID == id);
                    _eventTrigger.edges.RemoveAll(e => e.OutputId == id || e.InputId == id);
                }
                
                if(ele.GetType() != typeof(Edge)) continue;
                
                var edge = (Edge)ele;
                var outputNode = (EventNodeEditor)edge.output.node;
                var inputNode = (EventNodeEditor)edge.input.node;
                var port = edge.output.portName;

                _eventTrigger.edges.RemoveAll(e=> e.OutputId == outputNode.ID && e.InputId == inputNode.ID && e.PortName == port);
            }
        }

        if(graphViewChange.movedElements != null){
            foreach(var ele in graphViewChange.movedElements){
                if(ele.GetType() != typeof(EventNodeEditor)) continue;

                var node = (EventNodeEditor) ele;
                if(node.ID == Guid.Empty) continue;
                var referenceNode = _eventTrigger.nodes.Find(n => n.ID == node.ID);

                if(referenceNode == null) continue;
                referenceNode.Position = node.GetPosition().position;
            }
        }

        EditorUtility.SetDirty(_eventTrigger);
        return graphViewChange;
    }

    private void LoadGraph(){
        if(_eventTrigger == null) return;
        ClearGraph();
        var root = CreateRootNode();
        AddElement(root);
        RecreateGraph();
    }

    private void ClearGraph(){
        foreach(var edge in Edges) RemoveElement(edge);
        foreach(var node in Nodes) RemoveElement(node);
    }

    private void RecreateGraph(){
        if(_eventTrigger == null || _eventTrigger.nodes == null) return;

        foreach(var nodeData in _eventTrigger.nodes){
            
            var node=  CreateNode(nodeData);
            if(node == null) continue;
            AddElement(node);
            node.SetPosition(new Rect(nodeData.Position, DefaultNodeSize));
        }

        var cachedNodes = Nodes;
        foreach(var node in cachedNodes){
            
            for(int i = 0; i < node.outputContainer.childCount; i++){
                var port = node.outputContainer[i].Q<Port>();
                var edgeData = _eventTrigger.edges.Where(edge => edge.OutputId == node.ID && edge.PortName == port.portName).ToArray();

                foreach(var edge in edgeData){
                    
                    var targetNode = cachedNodes.FirstOrDefault(node => node.ID == edge.InputId);
                    if(targetNode == null) continue;
                    
                    LinkNode(port, (Port) targetNode.inputContainer[0]);
                }
            }
        }        
    }

    private EventNodeEditor CreateRootNode(){
        var root = new EventNodeEditor{
            title = _eventTrigger.name,
            ID = Guid.Empty
        };

        root.AddToClassList("root");
        var port = CreatePort(root, Direction.Output, Port.Capacity.Multi);

        port.portName = "OnTrigger";
        root.outputContainer.Add(port);

        root.RefreshExpandedState();
        root.RefreshPorts();
        root.SetPosition(new Rect(Vector2.one * 100, DefaultNodeSize));

        return root;
    }

    private static Port CreatePort(EventNodeEditor node, Direction direction, Port.Capacity capacity){
        return node.InstantiatePort(Orientation.Horizontal, direction, capacity, typeof(float));
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter){
        return ports.Where(port => startPort != port && startPort.node != port.node).ToList();
    }

    private void LinkNode(Port output, Port input){
        var edge = new Edge(){
            output = output,
            input = input
        };
        edge.input.Connect(edge);
        edge.output.Connect(edge);
        Add(edge);
    }

    private void CreatePropertyBoxes(EventNodeEditor node, SerializedObject serializedObject, SerializedProperty serializedProperty){
        if(serializedProperty == null) return;
        for(int i = 0; i < serializedProperty.arraySize; i++){
            var box = CreatePropertyBox(node, serializedObject, serializedProperty, i);
            
            if(box == null) continue;

            node.extensionContainer.Add(box);

        }
    }

    public EventNodeEditor CreateNode(EventNodeData data){
        if(data == null) return null;

        var node = new EventNodeEditor(){
            ID = data.ID
        };

        switch(data){
            case ConditionNode condition:
                CreateNode<ConditionNode, EventCondition>(node, condition, "Condition");
                break;
            case ReactionNode reaction:
                CreateNode<ReactionNode, EventReaction>(node, reaction, "Reaction");
                break;
            case IntegerConditionNode multi:
                CreateNode<IntegerConditionNode, EventInteger>(node, multi, "Multi");
                break;
        }

        var input = CreatePort(node, Direction.Input, Port.Capacity.Multi);
        input.portName = "Input";
        node.inputContainer.Add(input);
        node.RefreshExpandedState();
        node.RefreshPorts();
        node.SetPosition(new Rect(data.Position, DefaultNodeSize));
        AddElement(node);
        return node;
    }

    private void CreateNode<T, V>(EventNodeEditor node, T nodeData, string content) 
    where T : EventNodeData<V> where V : EventItemDataNode{
        
        var dropdown = CreateDropDown<T, V>();
        dropdown.choices.Insert(0, $"Add {content}");
        dropdown.index = 0;
        dropdown.SetEnabled(!nodeData.IsFull());
        node.title = content;
        node.extensionContainer.Add(dropdown);
        node.CreatePorts<T, V>(nodeData, Direction.Output, Port.Capacity.Multi);

        var Serialized = new SerializedObject(nodeData);
        
        var listData = Serialized.FindProperty("items");
        dropdown.RegisterValueChangedCallback(change =>
        {
                if(ScriptableObject.CreateInstance(change.newValue) is not V variable) return;
                variable.name = change.newValue;
                AssetDatabase.AddObjectToAsset(variable, nodeData);
                dropdown.SetValueWithoutNotify($"Add {content}");
                
                listData.arraySize++;
                listData.GetArrayElementAtIndex(listData.arraySize - 1).objectReferenceValue = variable;

                Serialized.ApplyModifiedProperties();
                var box = CreatePropertyBox(node, Serialized, listData, listData.arraySize - 1);
                node.extensionContainer.Add(box);
                node.RefreshExpandedState();
                dropdown.SetEnabled(!nodeData.IsFull());
        });

        CreatePropertyBoxes(node, Serialized, listData);
    }

    private GroupBox CreatePropertyBox(EventNodeEditor node, SerializedObject serializedObject, SerializedProperty serializedProperty, int i){
        var obj = serializedProperty.GetArrayElementAtIndex(i).objectReferenceValue;
        if(obj == null) return null;

        var box = new GroupBox();
        box.AddToClassList("property-box");

        var property = obj.GetType().GetProperty("Name");
        var foldOut = new Foldout(){
            text = property == null ? obj.GetType().Name : property.GetValue(obj, null).ToString()
        };
        foldOut.contentContainer.AddToClassList("property-foldout");
        
        var prop = new SerializedObject(obj);
        var itr = prop.GetIterator();
        if(itr.NextVisible(true)){
            do{
                if(itr.name == "m_Script") continue;
                var field = new PropertyField(itr);
                field.Bind(prop);
                foldOut.contentContainer.Add(field);
            }
            while(itr.NextVisible(false));
        }

        var deleteButton = new Button(
            () => {
                var item = serializedProperty.GetArrayElementAtIndex(i);
                serializedProperty.DeleteArrayElementAtIndex(i);
                serializedObject.ApplyModifiedProperties();
                node.extensionContainer.Remove(box);
                node.GetUIElement<DropdownField>().SetEnabled(true);
                AssetDatabase.RemoveObjectFromAsset(obj);
            }
        ){
            text = "Delete"
        };
        foldOut.contentContainer.Add(deleteButton);
        box.Add(foldOut);
        return box;
    }

    private DropdownField CreateDropDown<T1,T2>()where T1 : EventNodeData where T2 : EventItemDataNode{
        var choicesList = typeof(T1).Assembly.GetTypes()
            .Where(type => !type.IsAbstract && type.IsSubclassOf(typeof(T2)))
            .Select(t => t.Name).ToList();

        return new DropdownField(){
            choices = choicesList,
            label = ""
        };
    }

    
}