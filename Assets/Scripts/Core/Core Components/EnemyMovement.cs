using UnityEngine;
public class EnemyMovement : Movement{
    public void LookAtTarget(Vector2 targetPosition){
        var selfPosition = (Vector2)RigidBody.transform.position;
        int x = targetPosition.x >= selfPosition.x ? 1 : -1;
        CheckIfShouldFlip(x);
    }

    public float GetDistance(Vector2 target) => Vector2.Distance((Vector2)RigidBody.transform.position, target);
    public Vector2 GetVectorDifference(Vector2 target) => target - (Vector2)RigidBody.transform.position;
}