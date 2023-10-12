using UnityEngine;
public class LightHurt : PlayerState
{
    public LightHurt(Player other, string otherAnimName) : base(other, otherAnimName)
    {
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        Movement.SetVectorZero();
    }
}