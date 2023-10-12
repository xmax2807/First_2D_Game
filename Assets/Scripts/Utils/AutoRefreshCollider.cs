using UnityEngine;
using System.Collections.Generic;
public class AutoRefreshCollider : MonoBehaviour
{
    private PolygonCollider2D polygonCollider;
    private SpriteRenderer spriteRenderer;
    private Sprite _currentSprite;
    void Awake()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (spriteRenderer.sprite != _currentSprite)
        {
            Refresh();
        }
    }
    public void Refresh(){
        _currentSprite = spriteRenderer.sprite;
            
            // for (int i = 0; i < polygonCollider.pathCount; i++){
            //     polygonCollider.SetPath(i, (List<Vector2>)null);
            // }
            
            polygonCollider.pathCount = _currentSprite.GetPhysicsShapeCount();

            List<Vector2> path = new();
            for (int i = 0; i < polygonCollider.pathCount; i++)
            {
                path.Clear();
                _currentSprite.GetPhysicsShape(i, path);
                polygonCollider.SetPath(i, path.ToArray());
            }
    }
    void DisableCollider(){
        polygonCollider.enabled = false;
        enabled = false;
    }
    void EnableCollider(){
        enabled = true;
        polygonCollider.enabled = true;
    }
}