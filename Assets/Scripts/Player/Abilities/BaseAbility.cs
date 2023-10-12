using UnityEngine;
public abstract class BaseAbility : MonoBehaviour {
    [SerializeField] protected Animator baseAnimator;
    protected PlayerAbilityInputHandler abilityInputHandler;
    public System.Action OnFinishCallback;
    protected Core core;
    protected PlayerCombatInput combatInputState;
    protected abstract AbstractAbility[] Variants {get;}
    protected int currentIndex;
    public AbstractAbility CurrentActiveAbility => Variants[currentIndex];
    
    public abstract string GetName{get;}
    public virtual void AbilityEnter(){
        int inputDir = 0; 
        if(core.InputHandler.isFocus){
            inputDir = core.Movement.FacingDirection * (int)core.InputHandler.Movement.x;
        }
        currentIndex = inputDir + 1;
        gameObject.SetActive(true);

        SetInt("xInput", inputDir);

        baseAnimator.SetBool("ability", true);
        combatInputState = new PlayerCombatInput(Time.time, false);

        CurrentActiveAbility.OnFinishCallback += OnFinishCallback;
        CurrentActiveAbility.AbilityEnter();
        //isActive = true;
    }
    protected void Awake(){
        abilityInputHandler = GetComponentInParent<AbilityHandler>().InputHandler;
        core = abilityInputHandler.GetComponentInParent<Core>();
    }
    protected virtual void Start(){
        gameObject.SetActive(false);
    }
    // public void Update(){
    //     UpdateState();
    // }
    // public void FixedUpdate() => FixedUpdateState();

    public virtual void UpdateState(){
        CurrentActiveAbility.UpdateState();
        
        //SetFloat("transitionValue", Mathf.Clamp(combatInputState.HoldTime * 10,0f, 1f));
        // combatInputState.UpdateHoldTime(Time.time);
        // if(!core.AbilityInputHandler.IsAbilityPressed){
        //     combatInputState.Release();
        //     return;
        // }
    }
    public virtual void FixedUpdateState(){
        CurrentActiveAbility.FixedUpdateState();
    }
    
    public virtual void AbilityExit(){
        CurrentActiveAbility.OnFinishCallback -= OnFinishCallback;
        CurrentActiveAbility.AbilityExit();
        baseAnimator.SetBool("ability", false);
        gameObject.SetActive(false);
    }

    public virtual void SetInt(string name, int value){
        baseAnimator.SetInteger(name, value);
    }

    public virtual void SetFloat(string name, float value){
        baseAnimator.SetFloat(name, value);
    }
    public virtual void SetBool(string name, bool value){
        baseAnimator.SetBool(name, value);
    }
    public virtual bool CheckCanEnter(){
        int dir = 0;
        if(core.InputHandler.isFocus){
           dir = core.Movement.FacingDirection * (int)core.InputHandler.Movement.x;
        }
        dir+=1;// from (-1,1) to (0,2) => match the array index 
        
        if(dir > Variants.Length) return false;

        return Variants[dir] != null && Variants[dir].CheckCanEnter();
    }
    public virtual void ResetState()=>CurrentActiveAbility.ResetState();

    public void ManualFinishAnimation() => CurrentActiveAbility.OnFinishCallback?.Invoke();
}
