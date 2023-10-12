using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BreakableObstacle : BreakableItem{
    
    [SerializeField] private Sprite[] Statuses;
    private SpriteRenderer _renderer;
    private int currentStatus;
    protected override void Awake()
    {
        base.Awake();
        _renderer = GetComponent<SpriteRenderer>();
        currentStatus = 0;
        if(Statuses != null){
            _renderer.sprite = Statuses[0];
        }
    }
    protected override void OnDamage(float amount)
    {
        ++currentStatus;
        if(currentStatus >= Statuses.Length){
            isBroken = true;
            return;       
        }

        _renderer.sprite = Statuses[currentStatus];
    }
    protected override void OnDestroyState(){
        // exp
        SpawnSystem.Instance.SpawnObject(DestroyEffect, this.transform);
        Destroy(gameObject);
    }
}