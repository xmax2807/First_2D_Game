using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public interface IVisibleObject{
    void ToggleVisual(bool value);
}
public abstract class ObserverComponent<T> : PlayerCoreComponent where T : IVisibleObject{
    public float Distance;
    public LayerMask LayerMask;
    [SerializeField] protected Transform Observer;
    protected Dictionary<GameObject, bool> VisibleObject;

    protected override void Awake()
    {
        base.Awake();
        VisibleObject = new Dictionary<GameObject, bool>();
        Observer = Camera.main.transform;
    }
    protected virtual void FixedUpdate(){
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(Observer.position, Distance, LayerMask);
        for (int i = hitColliders.Length - 1; i > -1; i--)
        {   
            var obj = hitColliders[i].gameObject;

            VisibleObject[obj] = true;
            ToggleState (obj, true);
        }
        foreach(var key in VisibleObject.Keys.ToList()){
            if(VisibleObject[key] == false){
                ToggleState(key, false);
                VisibleObject.Remove(key);
            }
            else{
                VisibleObject[key] = false;
            }
        }
    }
    protected virtual void ToggleState(GameObject obj, bool state){
        if(obj == null) return;
        if(!obj.TryGetComponent<T>(out var item)) return;

        item.ToggleVisual(state);
    }
}