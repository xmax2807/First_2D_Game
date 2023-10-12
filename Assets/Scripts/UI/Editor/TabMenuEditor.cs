using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TabMenu), true)]
public class TabMenuEditor: Editor{
    public SerializedProperty flag;
    public string[] SubMenuName;
    private SaveLoadMenu _menu;
    void OnEnable()
    {
        _menu = (SaveLoadMenu)target;
        _menu.GetSubMenus();
        SubMenuName = _menu.MenuPanelNames;
        flag = serializedObject.FindProperty(nameof(_menu.selectedMenuFlag));
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        flag.intValue = EditorGUILayout.MaskField(new GUIContent("Active Menu"), flag.intValue, SubMenuName);
        Undo.RecordObject(target, "Change Active Menu");
        serializedObject.ApplyModifiedProperties();
    }
}