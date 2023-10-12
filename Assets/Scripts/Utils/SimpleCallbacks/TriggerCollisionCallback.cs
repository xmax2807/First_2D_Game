using UnityEngine;
using UnityEngine.Events;

public class TriggerCollisionCallback : MonoBehaviour{
    [SerializeField] UnityEvent<Collider2D> TriggerEnter;
    [SerializeField] UnityEvent<Collider2D> TriggerExit;
    void OnTriggerEnter2D(Collider2D collider) {
        TriggerEnter?.Invoke(collider);
    }
    void OnCollisionEnter2D(Collision2D collision){
        TriggerEnter?.Invoke(collision.collider);
    }
    void OnTriggerExit2D(Collider2D collider){
        TriggerExit?.Invoke(collider);
    }
    void OnCollisionExit2D(Collision2D collision){
        TriggerExit?.Invoke(collision.collider);
    }
}