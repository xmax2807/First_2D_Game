using UnityEngine;
using UnityEngine.UI;

public class AutoFitRectSize : MonoBehaviour
{
    [SerializeField] private RectTransform targetTransform;

    private void Start(){
        Change();
        enabled = false;
    }
    public void Change()
    {
        if (targetTransform == null) return;
        LayoutRebuilder.ForceRebuildLayoutImmediate(targetTransform);
        
        GetComponent<RectTransform>().sizeDelta = new Vector2(targetTransform.rect.width, targetTransform.rect.height);
    }
}