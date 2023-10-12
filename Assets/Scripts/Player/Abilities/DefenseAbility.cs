using UnityEngine;
using MilkShake;
public class DefenseAbility : BaseAbility{
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private DefenseAbilityData Data;
    
    private AbstractAbility[] _variants;
    protected override AbstractAbility[] Variants => _variants;

    public override string GetName => "Defense";

    protected override void Start()
    {
        base.Start();
        _variants = new Defense[]{
            new AutoCounter(this,core, Data),
            new Block(this,core, Data),
            new Counter(this,core, Data),
        };
    }
    public override void AbilityEnter()
    {
        base.AbilityEnter();
        weaponAnimator.SetBool("ability", true);
    }
    public override void AbilityExit(){
        base.AbilityExit();
        weaponAnimator.SetBool("ability", false);
    }
    public override void SetFloat(string name, float value)
    {
        base.SetFloat(name, value);
        weaponAnimator.SetFloat(name, value);
    }
    public override void SetInt(string name, int value)
    {
        base.SetInt(name, value);
        weaponAnimator.SetInteger(name, value);
    }
}