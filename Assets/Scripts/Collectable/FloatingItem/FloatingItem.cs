using UnityEngine;
public class FloatingItem : BaseItem{
    [SerializeField] protected float amplitude = 1f;
    [SerializeField] protected float speed = 2f;
    private Vector2 startPosition;
    private Vector2 currentPosition;
    protected Player _player;
    protected override void Awake(){
        base.Awake();
        startPosition = transform.position;
    }
   
    protected virtual void Update(){
        if(_player != null) return;

        currentPosition.Set(startPosition.x, startPosition.y + amplitude * Mathf.Sin(speed * Time.time));
        transform.position = currentPosition;
    }
}