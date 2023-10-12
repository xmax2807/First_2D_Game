using UnityEngine;
using UnityEngine.Events;
public class EventCallbacks : MonoBehaviour{
    public UnityEvent Events;
    public void TriggerEvents() => Events?.Invoke();
    public void DestroyThisGameObject(){
        Destroy(this.gameObject);
    }
}