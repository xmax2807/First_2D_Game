using UnityEngine;

public class EnemyCollisionSense : CollisionSenseBase{
    public EnemyCollisionSenseData enemyCollisionData => (EnemyCollisionSenseData)collisionData;

    [SerializeField] protected Transform playerCheck; 

    public RaycastHit2D CheckPlayerIsInRange(){
        return Physics2D.Raycast(playerCheck.position, Vector2.right * core.Movement.FacingDirection, enemyCollisionData.ObservationRange, enemyCollisionData.PlayerMask | enemyCollisionData.WallMask);
    }
    public RaycastHit2D CheckPlayerIsInAttackRange(){
        return Physics2D.Raycast(playerCheck.position, Vector2.right * core.Movement.FacingDirection, enemyCollisionData.AttackRange, enemyCollisionData.PlayerMask | enemyCollisionData.WallMask);
    } 
}