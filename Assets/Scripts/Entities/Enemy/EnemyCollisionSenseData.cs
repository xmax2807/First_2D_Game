using UnityEngine;

[CreateAssetMenu(fileName = "enemyCollisionBaseData", menuName = "Data/Entities/CollisionData/EnemyCollision")]
public class EnemyCollisionSenseData : CollisionSenseData{
    
    [Header("VisionRange")]
    public float ObservationRange = 10f;
    public float AttackRange = 3f;
    public LayerMask PlayerMask;
    public LayerMask WallMask;
}