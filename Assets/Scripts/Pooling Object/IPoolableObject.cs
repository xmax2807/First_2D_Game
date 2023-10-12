using UnityEngine;
public abstract class PoolableObject : MonoBehaviour{
    public abstract int CloneCount {get;}
    public abstract float LifeTime {get;}
}