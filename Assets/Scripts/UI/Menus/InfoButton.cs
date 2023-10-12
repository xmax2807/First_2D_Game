using UnityEngine;
using TMPro;
public class InfoButton : DisplayButton<string>
{
    [SerializeField] private string Id;
    [SerializeField]private string Info;
    [SerializeField] public TextMeshProUGUI DisplayInfo;

    public override string GetIdentity() => Id;

    protected override string GetDisplayInfo()=> Info;

    public void SetID(string id) => Id = id;
    public void SetInfo(string Info) => this.Info = Info;

    public override void ManualUpdate()
    {
        DisplayInfo.text = GetDisplayInfo();
    }
}