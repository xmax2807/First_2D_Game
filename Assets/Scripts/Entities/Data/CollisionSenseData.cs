using UnityEngine;

[CreateAssetMenu(fileName = "collisionBaseData", menuName = "Data/Entities/CollisionData")]

public class CollisionSenseData : ScriptableObject{
    public float GroundRadius = 0.5f;
    public LayerMask GroundLayer;
    public float WallCheckDistance = 0.3f;
    public float LedgeCheckDistance = 0.5f;
}