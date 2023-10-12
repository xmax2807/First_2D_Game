using UnityEngine;
public class Volcano : Magic
{
    public Volcano(BaseAbility root, Core core, MagicAbilityData data) : base(root, core, data)
    {
        OnFinishCallback += SummonVolcano;
    }
    public override bool CheckCanEnter()
    {
        return CurrentMana >= data.VolcanoManaRequired;
    }
    public override void AbilityEnter()
    {
        base.AbilityEnter();
        core.Movement.SetVectorZero();
        //InputManager.Controls.GamePlay.Disable();
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        core.Movement.SetVelocityY(2f);
    }
    public override void AbilityExit()
    {
        base.AbilityExit();
        //InputManager.Controls.GamePlay.Enable();
    }
    private void SummonVolcano(){
        ExplosionWeapon obj = SpawnSystem.Instance.SpawnObject<ExplosionWeapon>(data.DarkMagic, core.Player.GetPosition());
        if(obj == null){
            return;
        }
        obj.Init(core.Player, data.VolcanoDamageMultiplier * core.Stats.Damage);
        obj.WeaponEnter();
        core.UIBarComponent.UpdateValue(UIBarComponent.UIType.Mana, -data.VolcanoManaRequired);
    }
}