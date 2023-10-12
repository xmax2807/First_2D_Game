using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

public class SaveSlotGenerator : ButtonMenuGenerator{
    [SerializeField]private TextMeshProUGUI Displayer; 
    public override void PopulateButtons()
    {
        base.PopulateButtons();
        SaveSlot[] list = GetComponentsInChildren<SaveSlot>(true);
        if(list == null) return;

        for(int i = 0; i < list.Length; i++){
            list[i].SetProfileID("save00"+ i);
            list[i].DisplayInfo = Displayer;

            var button = list[i].GetComponent<Button>();
            button.onClick.RemoveAllListeners();
#if UNITY_EDITOR
            while(button.onClick.GetPersistentEventCount() > 0){
                UnityEventTools.RemovePersistentListener(button.onClick,0);
            }
            UnityEventTools.AddPersistentListener(button.onClick, list[i].Click);
#endif
        }
    }
}