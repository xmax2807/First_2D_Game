public interface IAbility{
    public bool CheckCanEnter();
    public void AbilityEnter();
    public void UpdateState();
    public void FixedUpdateState();
    public void AbilityExit();
    public void ResetState();
}