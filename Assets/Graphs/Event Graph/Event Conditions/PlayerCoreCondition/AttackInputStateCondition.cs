public class AttackInputStateCondition : CoreBoolCondition
{
    public override string Name => "Is Release";
    protected override bool CheckCoreCondition(Core core)
    {
        var attackInput = core.InputHandler.AttackInput;
        return attackInput.isReleased;
    }
}