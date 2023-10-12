public class GaurdBarBehaviour : ExpandableBarBehaviour{
    public override void Init(UIBarComponent manager)
    {
        maxValue = 2000;
        base.Init(manager);
    }
}