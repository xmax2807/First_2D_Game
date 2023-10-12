using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

public class InfoButtonMenuGenerator : ButtonMenuGenerator{
    [SerializeField]private string prefix;
    [SerializeField]private TextMeshProUGUI Displayer; 
    private InfoMenuData data => navigations as InfoMenuData;
    public override void PopulateButtons()
    {
        base.PopulateButtons();
        InfoButton[] list = GetComponentsInChildren<InfoButton>(true);
        if(list == null) return;

        for(int i = 0; i < list.Length; i++){
            list[i].SetID(prefix + i);
            list[i].DisplayInfo = Displayer;
            list[i].SetInfo(data?.Infos[i]);
        }
    }
}