using UnityEngine;

public class HPBarBehaviour : ExpandableBarBehaviour{
    public override void Init(UIBarComponent manager)
    {
        maxValue = manager.Stats.HitPoint;
        base.Init(manager);
    }
} 