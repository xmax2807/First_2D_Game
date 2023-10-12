using UnityEngine;

public class ExpItem : AutoCollectableItem{

    private const UIBarComponent.UIType type = UIBarComponent.UIType.Exp;
    private float value;
    public void Init(float initValue){
        value = initValue;
    }

    protected override void ApplyItem(Player player)
    {
        base.ApplyItem(player);
        player.UpdateUIComponent(type, value);
        OnDestroyState();
    }
}