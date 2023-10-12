using UnityEngine;

public class SummonSword : Magic
{
    public SummonSword(BaseAbility root, Core core, MagicAbilityData data) : base(root, core,data)
    {
    }

    public override void AbilityEnter()
    {
        base.AbilityEnter();
        Summon();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if(combatInputState.isReleased){
            OnFinishCallback?.Invoke();
            return;
        }
        
    }

    private void Summon(){
        MagicSword obj = SpawnSystem.Instance.SpawnObject<MagicSword>(data.MagicSword, core.Player.GetPosition());
        if(obj == null){
            OnFinishCallback?.Invoke();
            return;
        }
        obj.transform.position += new Vector3(Random.Range(-1f,1f) * data.RandomSpawn, Random.Range(-1f,1f)*data.RandomSpawn);
        obj.Init(core.Player, data.DamageMultiplier * core.Stats.Damage);
        obj.WeaponEnter();
    }
}