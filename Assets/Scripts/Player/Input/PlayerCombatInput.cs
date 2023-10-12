
using UnityEngine;
public class PlayerCombatInput{
    public enum InputType : int {
        Press = 0, Hold = 1, Release = 2
    }
    public float startTime {get;private set;}
    public float HoldTime {get; private set;}
    public bool isReleased {get;private set;}

    public PlayerCombatInput(float time, bool released){
        isReleased = released;
        startTime = time;
    }

    public void UpdateHoldTime(float value) => HoldTime = value;
    public void Release() => isReleased = true;
    public void ResetState() {
        startTime = Time.time;
        isReleased = false;
    }
    public InputType GetInputState(){
        if(HoldTime <= 0.1f + startTime) return InputType.Press;
        return isReleased ? InputType.Release : InputType.Hold;
    }
}