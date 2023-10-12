using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class DisplayButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public System.Action<DisplayButton> OnClicked;
    void Start(){
        GetComponent<Button>().onClick.AddListener(Click);
    }
    public abstract string GetIdentity();
    public virtual void OnDeselect(BaseEventData eventData){}
    public void Click(){
        OnClicked?.Invoke(this);
    }
    public virtual void OnSelect(BaseEventData eventData) => ManualUpdate();
    public virtual void ManualUpdate(){}
}

public abstract class DisplayButton<T> : DisplayButton{
    protected abstract T GetDisplayInfo();
}