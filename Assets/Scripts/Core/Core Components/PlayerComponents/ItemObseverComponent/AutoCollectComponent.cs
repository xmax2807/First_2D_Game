using UnityEngine;
public class AutoCollectComponent : PlayerCoreComponent{
    public float MaxDistance;
    public LayerMask Mask;
    [SerializeField] Transform CollectDetector;

    private void FixedUpdate(){
        var originPosition = new Vector2(CollectDetector.position.x - MaxDistance, CollectDetector.position.y);
        var hitColliders = Physics2D.RaycastAll(originPosition, Vector2.right,MaxDistance * 2, Mask);
        for (int i = hitColliders.Length - 1; i > -1; i--)
        {   
            var obj = hitColliders[i].transform.GetComponent<BaseItem>();
            obj.Trigger(Core.Player);
        }
    }
}