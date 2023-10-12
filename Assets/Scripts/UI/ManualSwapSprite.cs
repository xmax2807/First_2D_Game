using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ManualSwapSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] Sprites;
    private Image avatar;
    private void Awake(){
        avatar = GetComponent<Image>();
    }
    public void SwapSprite(int index){
        if(index > Sprites.Length){
            Debug.Log("index out of bound");
            return;
        }
        avatar.sprite = Sprites[index];
    }
}
