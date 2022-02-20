using QuickCode;
using UnityEngine;
using static EnemyBehaviourController;

public class BatController : EnemyCtrl_Base {

    public float FlyDirUpdateTimeInterval;
    public Vector2 moveDir;
    private Timer timer;
    void Start() {
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        timer = Timer.Register(FlyDirUpdateTimeInterval, () => UpdateBehabiours(), isLooped: true);
    }
    void Update() {
        if (IsDead || IsSleep) return;
        fly();
    }

    public override void UpdateBehabiours() {
        if (behaviourCtrl.Behaviour == EnemyBehaviours.Sleep) return;
        base.UpdateBehabiours();
        rb.constraints = RigidbodyConstraints2D.None;
        Transform player = GameObject.Find("Player").transform;
        switch (behaviourCtrl.Behaviour) {
            case EnemyBehaviours.Wander:
                float x = MathTool.RandomEx.GetFloat(-1f, 1f);
                float y = MathTool.RandomEx.GetFloat(-1f, 1f);
                moveDir = new Vector2(x, y);
                return;
            case EnemyBehaviours.GetClose:
                moveDir = (player.position - transform.position).normalized;
                return;
            case EnemyBehaviours.GetAway:
                moveDir = (transform.position - player.position).normalized;
                return;
            case EnemyBehaviours.Attack:
                moveDir = (player.position - transform.position).normalized;
                return;
        }
    }
    private void fly() {
        if (Behaviour == EnemyBehaviours.Attack) rb.velocity = moveDir * (Speed / 3);
        else rb.velocity = moveDir * Speed;
        transform.LookAt(transform.position + (Vector3)moveDir);
        transform.Rotate(new Vector3(0, 90, 0));
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        string tag = collision.gameObject.tag;
        if (tag != "Player" && tag != "Enemies") {
            if (MathTool.RandomEx.GetBool(0.33f)) {
                moveDir = moveDir * -1; return;
            } else if (MathTool.RandomEx.GetBool(0.5f)) {
                moveDir.x *= -1; return;
            }
            moveDir.y *= -1;
            return;
        }
        if (tag == "Player") OnAttack();
    }
    public override void OnAttack() {
        base.OnAttack();
        rb.MovePosition((Vector2)transform.position - moveDir);
    }
    public override void OnHit() {
        base.OnHit();
    }
    public override void OffHit() {
        base.OffHit();
    }
    public override void OnDead() {
        base.OnDead();
        timer.Cancel();
        rb.gravityScale = 1;
        rb.freezeRotation = true;
    }
}
