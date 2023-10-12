using UnityEngine;
public class PatrolEnemy : EnemyEntity
{
    [SerializeField] private FlashEffect hitEffect;
    private EnemyFactory _factory;
    public override EnemyFactory Factory => _factory;

    public override bool CanAttack => false;

    protected override void Awake()
    {
        base.Awake();
        _factory = new PatrolEnemyFactory(this);
        hitEffect ??= GetComponent<FlashEffect>();
    }

    public override void Damage(float amount)
    {
        hitEffect.StartAnimation();
        Core.UIComponent.Damage(amount);
    }

}