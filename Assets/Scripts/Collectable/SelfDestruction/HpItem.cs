using UnityEngine;
using System.Linq;
public class HpItem : AutoCollectableItem{
    [SerializeField] private float value;
    private const UIBarComponent.UIType type = UIBarComponent.UIType.HP;
    protected override void ApplyItem(Player player)
    {
        base.ApplyItem(player);
        //
        player.UpdateUIComponent(type, value);
        OnDestroyState();
    }
}