using UnityEngine;
public class Movement : CoreComponent{
    [SerializeField]protected Rigidbody2D RigidBody;
    [SerializeField]protected Collider2D Collider2D;
    protected const RigidbodyConstraints2D defaultConstraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
    public Vector2 CurrentVelocity => RigidBody.velocity;
    private Vector2 workspace = Vector2.zero;
    public int FacingDirection {get; private set;} = 1;
    private bool IsBlockedDirection = false;
    protected override void Awake()
    {
        base.Awake();
        workspace = Vector2.zero;

        if(RigidBody == null) RigidBody = GetComponentInParent<Rigidbody2D>();
        if(Collider2D == null) Collider2D = GetComponentInParent<Collider2D>();

        FacingDirection = this.core.transform.parent.rotation.y == 0 ? 1 : -1;
    }
    public override void OnDisable(){
        SetVectorZero();
    }
    
    public void SetVectorZero()=> RigidBody.velocity = Vector2.zero;

    public void SetVelocityX(float value){
        workspace.Set(value, RigidBody.velocity.y);
        RigidBody.velocity = workspace;
    }
    public void SetVelocityY(float value){
        workspace.Set(RigidBody.velocity.x, value);
        RigidBody.velocity = workspace;
    }

    public void SetVelocity(float value, Vector2 angle, int direction){
        angle.Normalize();
        workspace.Set(value* angle.x * direction, value * angle.y);
        RigidBody.velocity = workspace;
    }

    public void SetVelocity(Vector2 newVelocity) => RigidBody.velocity = new Vector2(newVelocity.x * FacingDirection, newVelocity.y);

    public void Flip(){
        FacingDirection *= -1;
        RigidBody.transform.Rotate(0f, 180f, 0f);
    }

    public void ToggleBlockDirection(bool value) => IsBlockedDirection = value;

    public void CheckIfShouldFlip(int xInput, bool isLockOn = false){
        if(IsBlockedDirection || isLockOn) return;
        if(xInput != 0f && xInput != FacingDirection){
            Flip();
        }
    }

    public void FreezeVelocity(int direction){
        if(direction == 0){// X direction
            SetVelocityX(0f);
            return;
        }
        if(direction == 1){
            SetVelocityY(0f);
            return;
        }
    }

    public void FreezeContraint(RigidbodyConstraints2D whichContraint){
        RigidBody.constraints |= whichContraint;
    }
    public void DefaultConstraints(){
        RigidBody.constraints = defaultConstraints;
    }

    public void AddForce(Vector2 force, ForceMode2D forceMode = ForceMode2D.Force) => RigidBody.AddForce(force, forceMode);

    public void TemporaryDisableCollider(float duration){
        Collider2D.enabled = false;
        TimeManager.Instance.WaitForSeconds(duration, ()=>Collider2D.enabled = true);
    }
}