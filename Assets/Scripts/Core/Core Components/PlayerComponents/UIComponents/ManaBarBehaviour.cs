public class ManaBarBehaviour : ExpandableBarBehaviour{
    public override void Init(UIBarComponent manager)
    {
        maxValue = manager.Stats.Energy;
        base.Init(manager);
    }
}