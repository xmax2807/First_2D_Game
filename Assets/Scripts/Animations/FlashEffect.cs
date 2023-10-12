using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FlashEffect : BaseAnimationScript {
    [SerializeField]private SpriteRenderer _renderer;
    [SerializeField]private Material targetMaterial;
    private Material originMaterial;
    public void Awake(){
        if(_renderer == null){
            _renderer = GetComponent<SpriteRenderer>();
        }
        originMaterial = _renderer.material;
    }

    public override void StartAnimation(bool skipAnimation = false)
    {
        if(_renderer == null){
            return;
        }
        originMaterial = _renderer.material;
        _renderer.material = targetMaterial;
        TimeManager.Instance.WaitForSeconds(0.1f, ()=>{
            if(_renderer == null){
                return;
            }
            _renderer.material = originMaterial;
        });
    }
}