using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;
using System;
using System.Linq;
using Unity.Plastic.Newtonsoft.Json;

public class EventGraphEditor : EditorWindow
{
    [UnityEditor.MenuItem("MyGraph/EventGraph")]
    private static void ShowWindow()
    {
        var window = GetWindow<EventGraphEditor>();
        window.titleContent = new GUIContent(text: "EventGraph");
        window.Show();
    }
    private EventTrigger _trigger;
    private EventGraphView graph;
    private DragAndDropManipulator manipulator;
    private void Awake(){
        UnityEngine.SceneManagement.SceneManager.sceneLoaded+=(_,_)=>{ 
            
            Initialize();
            LoadData();
        };
        
    }
    private void OnEnable()
    {
        Initialize();
        LoadData();
        Selection.selectionChanged += Initialize;
        EditorApplication.playModeStateChanged += PlayModeChanged;
    }
    private void OnDisable()
    {
        rootVisualElement.Clear();
        Selection.selectionChanged -= Initialize;
        EditorApplication.playModeStateChanged -= PlayModeChanged;
    }

    private void PlayModeChanged(PlayModeStateChange mode)
    {
        Initialize();
    }
    private void Initialize()
    {
        rootVisualElement.Clear();

        var box = new Box
        {
            style = {
                alignItems = Align.Center
            }
        };
        box.StretchToParentSize();
        if (Selection.activeGameObject == null || !Selection.activeGameObject.TryGetComponent<EventTrigger>(out EventTrigger trigger))
        {

            var label = new Label(text: "No Trigger selected")
            {
                style = {
                    top = 50
                    }
            };
            box.Add(label);
            rootVisualElement.Add(box);
            return;


        }
        if (_trigger == null || _trigger != null && trigger != _trigger){
            _trigger = trigger;
            LoadData();
        }
        if (PrefabStageUtility.GetCurrentPrefabStage() != null)
        {
            var label = new Label(text: "Cannot edit prefab")
            {
                style = {
                    top = 50
                }
            };
            box.Add(label);
            rootVisualElement.Add(box);
            return;
        }
        graph = new EventGraphView(this, _trigger)
        {
            name = _trigger.Name
        };
        graph.StretchToParentSize();
        //graph.AddToClassList("drop-area");
        rootVisualElement.Add(graph);
        manipulator = new(graph);
        manipulator.OnDropObject += OnDropObject;
        GenerateToolBar();
    }
    private void OnDropObject(UnityEngine.Object obj)
    {
        if (!obj.GetType().IsSubclassOf(typeof(EventNodeData))) return;
        var node = (EventNodeData)obj;
        if (_trigger.CheckExistedNode(node)) return;
        var worldMousePosition = rootVisualElement.ChangeCoordinatesTo(rootVisualElement.parent, this.position.position);
        var localMousePosition = graph.contentViewContainer.WorldToLocal(worldMousePosition);

        node.Position = localMousePosition;
        graph.CreateNode(node);
        _trigger.nodes.Add(node);
    }
    private void GenerateToolBar()
    {
        var toolbar = new UnityEditor.UIElements.Toolbar();

        var fileNameTextField = new TextField("File Name");
        fileNameTextField.SetValueWithoutNotify(_trigger.Name);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.isReadOnly = true;
        toolbar.Add(fileNameTextField);
        toolbar.Add(new Button(SaveData)
        {
            text = "Save Graph"
        });
        toolbar.Add(new Button(LoadData)
        {
            text = "Load Graph"
        });
        rootVisualElement.Add(toolbar);
    }
    private void SaveData()
    {
        // var nodeStrings = new string[_trigger.nodes.Count];
        // for(int i = 0; i < nodeStrings.Length; i++){
        //     nodeStrings[i] = _trigger.nodes[i].ID.ToString();
        // }
        string path = $"Assets/Resources/{_trigger.Name}/{_trigger.Name}.json";
        if (!AssetDatabase.IsValidFolder($"Assets/Resources/{_trigger.Name}/"))
            AssetDatabase.CreateFolder("Assets/Resources", _trigger.Name);

        var container = new NodeContainer();
        container.edges = new System.Collections.Generic.List<EventEdgeData>(_trigger.edges);

        string[] paths = _trigger.nodes.Select((node) => AssetDatabase.GetAssetPath(node)).ToArray();
        container.nodeIDs = new(paths);
        AssetDatabaseIO.DeleteAssets<EventNodeData>(_trigger.Name,
        (id) =>
            paths.All((str) => str != id)
        );
        System.IO.File.WriteAllText(path, JsonUtility.ToJson(container));
        AssetDatabase.Refresh();
    }
    private void LoadData()
    {
        if(_trigger == null) return;
        NodeContainer data = JsonUtility.FromJson<NodeContainer>(System.IO.File.ReadAllText($"Assets/Resources/{_trigger.Name}/{_trigger.Name}.json"));
        if (data == null) return;
        //var nodes = new EventNodeData[container.nodes.Count];
        // for(int i = 0; i < nodes.Length; i++){
        //     var path = AssetDatabase.GUIDToAssetPath (container.nodes[i]);
        //     Debug.Log(path);
        //     nodes[i] = AssetDatabase.LoadAssetAtPath(path, typeof(EventNodeData)) as EventNodeData;
        // }
        _trigger.nodes = new System.Collections.Generic.List<EventNodeData>(AssetDatabaseIO.LoadAssets<EventNodeData>(data.nodeIDs));
        _trigger.edges = new System.Collections.Generic.List<EventEdgeData>(data.edges);
        Initialize();
    }
}
