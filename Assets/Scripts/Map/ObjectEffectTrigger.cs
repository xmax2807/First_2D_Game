using UnityEngine;

public abstract class ObjectEffectTrigger<T> : MonoBehaviour where T : Entity{
    
    void OnCollisionEnter2D(Collision2D collision){
        if(!collision.collider.TryGetComponent<T>(out T entity)) return;

        TriggerEffect(entity);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(!collider.TryGetComponent<T>(out T entity)) return;
        TriggerEffect(entity);
    }

    protected virtual void TriggerEffect(T target){}
}