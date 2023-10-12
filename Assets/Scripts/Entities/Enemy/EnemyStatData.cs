using UnityEngine;

[CreateAssetMenu(fileName = "BaseEnemyStat", menuName = "Data/Entities/EnemyStat")]
public class EnemyStatData : EntityData{
    [Header("Idle State")]
    public float IdleWaitMin = 1f;
    public float IdleWaitMax = 2f;

    [Header("MovementState")]
    public float speed = 12f;
    public float distance = 20f;

    [Header("AttackState")]
    public float delayAttackTime = 2f;
    public float AttackRange = 2f;
}