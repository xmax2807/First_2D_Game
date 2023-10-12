using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ButtonMenuGenerator), true)]
public class ButtonMenuGeneratorEditor : Editor{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        ButtonMenuGenerator script = (ButtonMenuGenerator) target;
        if(GUILayout.Button("Apply")){
            script.PopulateButtons();
            EditorUtility.SetDirty(target);
        }
        if(GUILayout.Button("Update Events")){
            script.UpdateEvent();
            EditorUtility.SetDirty(target);
        }
    }
}
