using UnityEngine;

public class AttackEnemy : EnemyEntity
{
    [SerializeField] protected EnemyFactory.ApproachType approachType;
    private EnemyFactory _factory;
    public override EnemyFactory Factory => _factory;

    public override bool CanAttack => true;

    protected override void Awake()
    {
        base.Awake();
        _factory = new AttackEnemyFactory(this);
        _factory.CreateApproachState(approachType);
    }
}