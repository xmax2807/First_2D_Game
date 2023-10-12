using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ManualAnimatorController : MonoBehaviour{
    [SerializeField]private Animator _animator;
    [SerializeField] private int NumberOfPreset;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    private int _index;
    private void Awake(){
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Start(){
        Shuffle();
        _animator.SetInteger("index",_index);
        int direction = this.transform.rotation.y > 0? -1:1;
        _rigidbody2D.AddForce(3f * direction * this.transform.position.normalized, ForceMode2D.Impulse);
        Destroy(this.gameObject, _animator.GetCurrentAnimatorClipInfo(0).Length);
    }

    public void Shuffle(){
        _index = Random.Range(2, NumberOfPreset);
    }
}