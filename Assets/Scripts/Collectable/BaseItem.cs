using UnityEngine;
using System;
public abstract class BaseItem : MonoBehaviour, IVisibleObject{
    [SerializeField] protected GameObject DestroyEffect;
    protected bool isApplied;
    protected Renderer itemRenderer;
     protected Transform[] children;
    protected virtual void Awake(){
        children = new Transform[transform.childCount];

        for (int i = 0; i < children.Length; ++i){
            children[i] = transform.GetChild(i);
        }
    }
    protected virtual void Start(){
        itemRenderer = GetComponent<Renderer>();
        isApplied = false;
        ToggleVisual(false);
    }
    protected virtual void OnTriggerEnter2D(Collider2D other){
        if(!other.CompareTag("Player")){ // if not player
            return;
        }

        if(!isApplied) ApplyItem(other.GetComponent<Player>());
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision){
        OnTriggerEnter2D(collision.collider);
    }
    protected virtual void ApplyItem(Player player){
        isApplied = true;
    }

    public virtual void ToggleVisual(bool value){
        if(itemRenderer == null) return;
        itemRenderer.enabled = value;

        enabled = value;
        if(children == null) return;
        for(int i = 0; i < children.Length; i++){
            children[i].gameObject.SetActive(value);
        }
    }
    
    public virtual void Trigger(Player player){}
    protected virtual void OnDestroyState(){
        ToggleVisual(false);
        if(this == null || SpawnSystem.Instance == null) return;
        SpawnSystem.Instance.SpawnObject(DestroyEffect, this.transform);
        Destroy(gameObject);
    }
}