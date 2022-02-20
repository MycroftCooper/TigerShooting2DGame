using UnityEngine;

public class CharacterCollision : MonoBehaviour {
    [Header("Layers")]
    public LayerMask GroundLayer;

    [Space]
    [Header("Stats")]
    public bool OnGround;
    public bool OnWall;
    public bool OnRightWall;
    public bool OnLeftWall;
    [Space]

    [Header("Collision")]
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;

    void Update() {
        OnGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, GroundLayer);

        OnRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, GroundLayer);
        OnLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, GroundLayer);
        if (OnRightWall || OnLeftWall) OnWall = true;
        else OnWall = false;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
}
