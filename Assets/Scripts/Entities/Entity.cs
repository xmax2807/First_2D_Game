using UnityEngine;
public abstract class Entity : MonoBehaviour{
    public FiniteStateMachine StateMachine {get; protected set;}
    public Animator Animator {get;protected set;}
    protected BaseCore _core;
    [SerializeField]protected EntityData _data;
    [SerializeField]protected ScriptableObject _audioPreset;
    protected virtual void Awake(){
        StateMachine = new FiniteStateMachine();
        _core = GetComponentInChildren<BaseCore>();
        Animator = GetComponent<Animator>();
    }
    protected virtual void Start(){
        //StateMachine = new FiniteStateMachine();
    }

    protected virtual void Update(){
        StateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate(){
        StateMachine.CurrentState.PhysicUpdate();
    }
    private void AnimationTrigger() { 
        StateMachine.CurrentState.AnimationTrigger();
    }

    private void AnimationFinishTrigger() {
        StateMachine.CurrentState.AnimationFinishTrigger();
        
    }
}