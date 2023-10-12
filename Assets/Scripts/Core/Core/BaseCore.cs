using UnityEngine;

public abstract class BaseCore : MonoBehaviour{
    public Movement Movement {get; protected set;}
    public CollisionSenseBase Senses {get; protected set;}
    protected virtual void Awake(){
        Movement = GetComponentInChildren<Movement>();
        Senses = GetComponentInChildren<CollisionSenseBase>();
    }
    public virtual void Update(){
        
    }
}