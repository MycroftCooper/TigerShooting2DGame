using QuickCode;
using UnityEngine;

public class BulletController : MonoBehaviour {
    public bool IsEnemy;
    public Animator animator;
    public float DestoryTime = 5f;
    public bool IsHit {
        set {
            if (IsHit == value) return;
            animator.SetBool("IsHit", value);
            if (value) GameEntry.Instance._AudioManager.PlaySFX(AudioResCtrl.BulletHitClip);
        }
        get {
            return animator.GetBool("IsHit");
        }
    }
    public float BeatBackForce = 15;
    public int DamageValue;
    public float RandomY;
    public float RandomAngle;
    public Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        this.AttachTimer(DestoryTime, () => IsHit = true);
    }
    public void OnFire(Vector2 dir, float speed) {
        // 确认发射方向
        transform.LookAt(transform.TransformPoint(dir));
        transform.Rotate(new Vector3(0, -90, 0));

        // 加入纵向随机性
        float ry = Random.Range(-RandomY, RandomY);
        Vector2 pos = new Vector3(0, ry);
        transform.position = transform.TransformPoint(pos);

        // 加入速度随机性
        float ra = Random.Range(-RandomAngle, RandomAngle);
        rb.velocity = transform.TransformDirection(new Vector2(1, ra).normalized) * speed;

        GameEntry.Instance._AudioManager.PlaySFX(AudioResCtrl.GunFireClip);
    }
    public void AfterHit() {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        string tag = collision.gameObject.tag;
        if (tag == "Bullet") return;
        if (tag == "Player" && !IsEnemy) return;
        if (tag == "Enemies" && IsEnemy) return;
        IsHit = true;
        rb.velocity = Vector2.zero;
        var targetRB = collision.gameObject.GetComponent<Rigidbody2D>();
        if (tag == "Player" || tag == "Enemies" || tag == "Obstacles") {
            if (targetRB != null && tag != "Obstacles")
                targetRB.MovePosition(transform.TransformPoint(Vector2.right * BeatBackForce));// 击退力
            EntityData ed = collision.gameObject.GetComponent<EntityData>();
            if (ed.HP == 0) return;
            ed.OnHit(DamageValue);
            return;
        }
        if (tag == "Swing") {
            Vector2 force = (collision.transform.position - transform.position) * BeatBackForce * 100;
            targetRB.AddForceAtPosition(force, collision.ClosestPoint(transform.position));
        }
    }
    private void OnBecameInvisible() {
        IsHit = true;
        rb.velocity = Vector2.zero;
    }
}
