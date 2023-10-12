using UnityEngine;
public class CollisionSenseBase : CoreComponent{
    [SerializeField] protected CollisionSenseData collisionData;
    //protected CollisionSenseData collisionData => core.CollisionData;
    [SerializeField]
    protected Transform _groundTransform;
    [SerializeField] protected Transform _groundTransformLeft;
    [SerializeField]
    protected Transform _ledgeCheck;
    [SerializeField]
    protected Transform _highWallCheck;
    protected int FacingDirection => core.Movement.FacingDirection;
    protected Vector2 workspace = Vector2.zero;
    
    public Transform GetGroundTransform() => _groundTransform;
    public virtual bool CheckIfTouchingGround(){
        return collisionData.GroundLayer != 0 && Physics2D.OverlapCircle(_groundTransform.position, collisionData.GroundRadius, collisionData.GroundLayer);
    }
    public virtual bool CheckIfTouchingWall(){
        return Physics2D.Raycast(_highWallCheck.position,Vector2.right * FacingDirection, collisionData.WallCheckDistance, collisionData.GroundLayer);
    }

    public virtual bool CheckIfTouchingLedge(){
        return collisionData.GroundLayer != 0 && Physics2D.Raycast(_ledgeCheck.position, Vector2.down, collisionData.LedgeCheckDistance, collisionData.GroundLayer);
    }
    public virtual bool CheckIfReachEdge(){
        return false;
    }
}