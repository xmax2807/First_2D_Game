
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(SaveSlotGenerator))]
public class SaveSlotGeneratorEditor : Editor{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        SaveSlotGenerator script = (SaveSlotGenerator) target;
        if(GUILayout.Button("Apply")){
            script.PopulateButtons();
            EditorUtility.SetDirty(target);
        }
    }
}