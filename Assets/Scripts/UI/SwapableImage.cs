using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapableImage : MonoBehaviour
{
    public enum ImageState{
        Selected, Default, Disabled
    }
    [SerializeField] private Sprite Selected;
    [SerializeField] private Sprite Default;
    [SerializeField] private Sprite Disabled;
    private Image gameObjectImage; 
    void Awake(){
        if(!TryGetComponent<Image>(out gameObjectImage)){
            Debug.LogWarning($"No Image Holder Found in {this.gameObject.name}");
            this.enabled = false;
        }
    }

    public void Swap(ImageState state){
        switch(state){
            case ImageState.Selected: gameObjectImage.sprite = Selected; break;
            case ImageState.Default: gameObjectImage.sprite = Default; break;
            case ImageState.Disabled: gameObjectImage.sprite = Disabled; break;
        }
    }

    public void Select() => gameObjectImage.sprite = Selected;
    public void UnSelect() => gameObjectImage.sprite = Default;
}
