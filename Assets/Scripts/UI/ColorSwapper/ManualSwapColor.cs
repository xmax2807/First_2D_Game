using UnityEngine;

public abstract class ManualSwapColor : MonoBehaviour{
    [SerializeField] private Color[] colors;

    public void Swap(int index){
        index = Mathf.Clamp(index, 0, colors.Length);
        Swap(colors[index]);
    }

    protected abstract void Swap(Color color);
}