using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Icon : MonoBehaviour{
    private Image icon;
    [SerializeField] bool isHidden;
    [SerializeField] private Color FadeColor;
    private void Awake(){
        icon = GetComponent<Image>();
    }
    // public void Start(){
    //     Toggle(!isHidden);
    // }
    public void Toggle(bool value){
        if(icon == null){
            icon = GetComponent<Image>();
        }
        if(value){
            icon.color = Color.white;
        }
        else icon.color = FadeColor;
    }
}