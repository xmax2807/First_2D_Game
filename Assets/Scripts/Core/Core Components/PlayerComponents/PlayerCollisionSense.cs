using UnityEngine;
public class PlayerCollisionSense : CollisionSenseBase{
    
    [SerializeField]
    protected Transform _lowWallCheck;

    [SerializeField]
    protected Transform _edgeCheck;
    
    public override bool CheckIfTouchingGround(){
        return Physics2D.OverlapCircle(_groundTransform.position, collisionData.GroundRadius, collisionData.GroundLayer)
        && Physics2D.OverlapCircle(_groundTransformLeft.position, collisionData.GroundRadius, collisionData.GroundLayer);
    }
    public bool CheckIfTouchingAnyGroundSide(){
        return Physics2D.OverlapCircle(_groundTransform.position, collisionData.GroundRadius, collisionData.GroundLayer)
        || Physics2D.OverlapCircle(_groundTransformLeft.position, collisionData.GroundRadius, collisionData.GroundLayer);
    }
    // public override bool CheckIfTouchingWall(){
    //     return Physics2D.Raycast(_highWallCheck.position,Vector2.right * FacingDirection, collisionData.WallCheckDistance, collisionData.GroundLayer);
    // }
    
    public bool CheckIfHalfPartTouchedWall(){
        return Physics2D.Raycast(_lowWallCheck.position, Vector2.right * FacingDirection, collisionData.WallCheckDistance, collisionData.GroundLayer);
    }

    public override bool CheckIfTouchingLedge(){
        return Physics2D.Raycast(_ledgeCheck.position, Vector2.right * FacingDirection, collisionData.WallCheckDistance, collisionData.GroundLayer);
    }

    public Vector2 DetermineCornerPosition(){
        RaycastHit2D xHit = Physics2D.Raycast(_highWallCheck.position, Vector2.right * FacingDirection, collisionData.WallCheckDistance, collisionData.GroundLayer);
        workspace.Set(xHit.distance * FacingDirection, 0f);
        
        RaycastHit2D yHit = Physics2D.Raycast(_ledgeCheck.position + (Vector3)workspace, Vector2.down, _ledgeCheck.position.y - _highWallCheck.position.y, collisionData.GroundLayer);
        workspace.Set(_highWallCheck.position.x + (xHit.distance *FacingDirection), _ledgeCheck.position.y - yHit.distance);

        return workspace;
    }

    public Vector2 DetermineCornerPositionLow(){
        RaycastHit2D xHit = Physics2D.Raycast(_lowWallCheck.position, Vector2.right * FacingDirection, collisionData.WallCheckDistance, collisionData.GroundLayer);
        workspace.Set(xHit.distance * FacingDirection, 0f);
        
        RaycastHit2D yHit = Physics2D.Raycast(_ledgeCheck.position + (Vector3)workspace, Vector2.down, _ledgeCheck.position.y - _lowWallCheck.position.y, collisionData.GroundLayer);
        workspace.Set(_lowWallCheck.position.x + (xHit.distance *FacingDirection), _ledgeCheck.position.y - yHit.distance);

        return workspace;
    }

    public Vector2 GetHighWallPosition() => _highWallCheck.position;
    public override bool CheckIfReachEdge()
    {
        return !Physics2D.Raycast(_edgeCheck.position, Vector2.down, collisionData.WallCheckDistance, collisionData.GroundLayer);

    }
}