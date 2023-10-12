using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public sealed class ManualSwapTextColor : ManualSwapColor
{
    private TextMeshProUGUI text;
    private void Awake(){
        text = GetComponent<TextMeshProUGUI>();
    }  
    protected override void Swap(Color color)
    {
        text.color = color;
    }
    public void SwapText(string text)=> this.text.text = text;
}