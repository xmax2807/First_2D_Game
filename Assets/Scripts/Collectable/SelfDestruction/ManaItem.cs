using UnityEngine;
public class ManaItem : AutoCollectableItem{
    [SerializeField] float value;
    private const UIBarComponent.UIType type = UIBarComponent.UIType.Mana;
    protected override void ApplyItem(Player player)
    {   
        base.ApplyItem(player);
        //
        player.UpdateUIComponent(type, value);
        OnDestroyState();
    }
}