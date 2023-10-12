public class FiniteStateMachine{
    public State CurrentState {get;private set;}

    public void Init(State startingState){
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(State newState){
        if(newState == null) return;
        
        CurrentState.Exit();
        
        CurrentState = newState;
        CurrentState.Enter();
    }
}