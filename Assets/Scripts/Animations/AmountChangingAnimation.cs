using UnityEngine;
public abstract class AmountChangingAnimation : BaseAnimationScript<float>{
    protected float amount;
    public override void SettingUp(float start, float end)
    {
        base.SettingUp(start, end);
        amount = end - start;
    }
}