using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
public class EventNodeSearchWindow : ScriptableObject, ISearchWindowProvider{
    private EventGraphView _view;
    private EventGraphEditor _editor;
    private EventTrigger _eventTrigger;

    public void Initialize(EventGraphEditor editor, EventGraphView view, EventTrigger trigger){
        _editor = editor;
        _view = view;
        _eventTrigger = trigger;
    }
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context){
        var tree = new List<SearchTreeEntry>(){
            new SearchTreeGroupEntry(new GUIContent("Create Element")),
            new(new GUIContent("Condition")){
                userData = CreateInstance<ConditionNode>(),
                level = 1
            },
            new (new GUIContent("Reaction")){
                userData = CreateInstance<ReactionNode>(),
                level = 1
            },
            new (new GUIContent("MultiOutput")){
                userData = CreateInstance<IntegerConditionNode>(),
                level = 1
            },
        };
        return tree;
    }

    public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context){
        var node = (EventNodeData) searchTreeEntry.userData;
        node.ID = Guid.NewGuid();

        var worldMousePosition = _editor.rootVisualElement.ChangeCoordinatesTo(_editor.rootVisualElement.parent,
        context.screenMousePosition - _editor.position.position);
        var localMousePosition = _view.contentViewContainer.WorldToLocal(worldMousePosition);

        node.Position = localMousePosition;
        _view.CreateNode(node);
        _eventTrigger.nodes.Add(node);
        AssetDatabaseIO.SaveAsset<EventNodeData>(node, searchTreeEntry.name, _eventTrigger.Name);
        // switch(node){
        //     case ConditionNode condition:
        //         _view.CreateNode(condition);
        //         _eventTrigger.nodes.Add(condition);
        //         break;
        //     case ReactionNode reaction:
        //         _view.CreateNode(reaction);
        //         _eventTrigger.nodes.Add(reaction);
        //         break;
        // }
        return true;
    }

}