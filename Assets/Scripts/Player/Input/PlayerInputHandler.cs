using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour, GamePlayInputMap.IGamePlayActions
{
    public bool IsAnyKeyPressed => IsJump || Movement != Vector2.zero || abilityInputHandler.IsAbilityPressed;
    public Vector2 Movement {get;private set;}
    public bool IsJump {get;private set;}

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;

    private GamePlayInputMap.GamePlayActions Actions;
    private PlayerAbilityInputHandler abilityInputHandler;
    
    private static PlayerInputHandler _instance;
    public void Awake(){
        if(_instance == null){
            Actions = InputManager.Controls.GamePlay;
            Actions.SetCallbacks(this);
            _instance = this;
        }
        Actions.Enable();
        abilityInputHandler = GetComponent<PlayerAbilityInputHandler>();
    }
    private void Update(){
        CheckJumpInputHoldTime();
    }
    public void JumpPressed() => IsJump = false;

    public PlayerCombatInput AttackInput {get; private set;}
    public event Action OnAttackPress;
    private void CheckJumpInputHoldTime(){
        if(Time.time >= jumpInputStartTime + inputHoldTime){
            IsJump = false;
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started){//is down
            jumpInputStartTime = Time.time;
            IsJump = true;
            return;
        }
        // if(context.performed){// is holding
        //     return;
        // }
        if(context.canceled){// is up
            IsJump = false;
            return;
        }
    }

    public bool IsAttacking {get;private set;}
    public void OnNormalAttack(InputAction.CallbackContext context)
    {
        IsAttacking = context.started;
        if(context.started){
            AttackInput = new PlayerCombatInput(Time.time, false);
        }
        else if(context.performed){
            AttackInput.UpdateHoldTime(Time.time);
        }
        else if(context.canceled){
            AttackInput.Release();
            if(!AttackInput.isReleased)return;
        }
        OnAttackPress?.Invoke();
    }

    public void OnAbility(InputAction.CallbackContext context)
    {
        abilityInputHandler.AbilityInputListener(context);
    }

    public void OnAbilityAction(InputAction.CallbackContext context)
    {
        abilityInputHandler.AbilityActionListener(context);
    }

    public void OnToggleUI(InputAction.CallbackContext context)
    {
        if(!context.started) return;
        MenuManager.Instance.PushRootMenu();
    }

    public bool isFocus = false;
    public void OnFocus(InputAction.CallbackContext context)
    {
        isFocus = !context.canceled;
    }
}
