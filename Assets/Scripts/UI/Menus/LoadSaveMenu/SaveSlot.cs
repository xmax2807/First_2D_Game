using UnityEngine;
using System.Text;
using TMPro;
public class SaveSlot : DisplayButton<string>{
    [Header("Profile")]
    [SerializeField] private string profileID = "";
    [SerializeField] public TextMeshProUGUI DisplayInfo;
    private StringBuilder cacheBuilder;
    private SaveSlotData slotData;
    public void SetProfileID(string id) => profileID = id;
    
    public void SetData(GameData data){
        if(data != null){
            slotData = new()
            {
                LastSavedDate = data.LastSavedDate,
                TimePlayed = data.TimePlayed
            };
            ManualUpdate();
        }
    }
    public override void ManualUpdate()
    {
        if(DisplayInfo == null) return;
        DisplayInfo.text = GetDisplayInfo();
    }
    protected override string GetDisplayInfo()
    {
        string data = slotData == null ? "No Data" : slotData.LastSavedDate;
        Color c = Graphics.BrightenColor(Color.blue);

        cacheBuilder ??= new();
        cacheBuilder.Clear();
        cacheBuilder.Append($"<color=#{((Color32) c).ToValue():X}>")
        .Append($"<size=24>{data}</size>")
        .Append("</color>\n");

        return cacheBuilder.ToString();
    }

    public override string GetIdentity()
    {
        return this.profileID;
    }
}