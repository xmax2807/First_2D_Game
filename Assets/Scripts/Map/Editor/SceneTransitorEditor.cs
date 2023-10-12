using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneTransitor)), CanEditMultipleObjects]
public class SceneTransitorEditor : Editor
{
    public SerializedProperty transitionType, DestinationScene;

    void OnEnable()
    {
        // Setup the SerializedProperties
        transitionType = serializedObject.FindProperty("transitionType");
        DestinationScene = serializedObject.FindProperty("SceneName");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(transitionType);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("SpawnPosition"));

        SceneTransitor.SceneTransitionType st = (SceneTransitor.SceneTransitionType)transitionType.enumValueIndex;

        switch (st)
        {
            case SceneTransitor.SceneTransitionType.NextScene:
                break;

            case SceneTransitor.SceneTransitionType.PreviousScene:
                break;

            case SceneTransitor.SceneTransitionType.SpecificScene:
                EditorGUILayout.PropertyField(DestinationScene, new GUIContent("Scene Name"));
                break;

        }

        serializedObject.ApplyModifiedProperties();
    }
}