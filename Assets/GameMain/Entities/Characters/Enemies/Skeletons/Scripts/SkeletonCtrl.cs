using QuickCode;
using UnityEngine;
using static EnemyBehaviourController;

public class SkeletonCtrl : EnemyCtrl_Base {
    private bool isFlip;
    public bool IsFlip {
        get {
            isFlip = sr.flipX;
            return isFlip;
        }
        set {
            if (value == isFlip) return;
            isFlip = sr.flipX = value;
        }
    }
    public float moveDir;

    private CharacterCollision cc;


    void Update() {
        if (IsDead || IsSleep) return;
        UpdateBehabiours();
        move();
    }
    public override void UpdateBehabiours() {
        if (behaviourCtrl.Behaviour == EnemyBehaviours.Sleep) return;
        base.UpdateBehabiours();

        Transform player = GameObject.Find("Player").transform;
        switch (behaviourCtrl.Behaviour) {
            case EnemyBehaviours.Wander:
                moveDir = MathTool.RandomEx.GetFloat(-1f, 1f);
                return;
            case EnemyBehaviours.GetClose:
                if (player.position.x > transform.position.x) moveDir = 1f;
                else moveDir = -1f;
                return;
            case EnemyBehaviours.GetAway:
                if (player.position.x > transform.position.x) moveDir = -1f;
                else moveDir = 1f;
                return;
            case EnemyBehaviours.Attack:
                if (player.position.x > transform.position.x) moveDir = 1f;
                else moveDir = -1f;
                return;
        }
    }
    private void move() {
        float y = 0;
        if (cc == null) cc = GetComponent<CharacterCollision>();
        if (!cc.OnGround) y = -5;
        rb.velocity = new Vector2(Speed * moveDir, y);
        if (moveDir < 0) IsFlip = true;
        else if (moveDir > 0) IsFlip = false;
    }


    public void OnCollisionEnter2D(Collision2D collision) {
        string tag = collision.gameObject.tag;
        if (tag != "Player" && tag != "Enemies") {
            moveDir = -moveDir;
            return;
        }
        if (tag == "Player") {
            OnAttack();
            float x = 0;
            if (collision.transform.position.x < transform.position.x)
                x = -0.5f;
            else x = 0.5f;
            collision.rigidbody.MovePosition(collision.transform.TransformPoint(new Vector3(x, 0, 0)));
        }
    }
    public override void OnAttack() {
        base.OnAttack();
        rb.MovePosition(new Vector2(transform.position.x + -moveDir, transform.position.y));
    }
    public override void OnHit() {
        base.OnHit();
        createScraps(2);
    }
    public override void OnDead() {
        base.OnDead();
        createScraps(5);
        sr.enabled = false;
    }
    public override void OffHit() {
        base.OffHit();
    }

    #region 碎片相关
    public int maxScrapNum;
    public float ScrapForce;
    public GameObject ScrapPrefab;
    public GameObjectPool ScrapPool;
    private Sprite[] scrapSprites;

    private void Start() {
        ScrapPool = new GameObjectPool();
        ScrapPool.InitPool(maxScrapNum, ScrapPrefab, GameObject.Find("Scraps").transform);
        scrapSprites = Resources.LoadAll<Sprite>("Skeleton_Death");
    }

    private Sprite getBarrelScrapSprite() {
        int index = MathTool.RandomEx.GetInt(1, scrapSprites.Length);
        return scrapSprites[index];
    }
    private GameObject getBarrelScrapGO() {
        GameObject go = ScrapPool.GetObject(true);
        go.RemoveComponent<PolygonCollider2D>();
        go.GetComponent<SpriteRenderer>().sprite = getBarrelScrapSprite();
        go.AddComponent<PolygonCollider2D>();
        ScrapPool.Recycle(go, true);
        go.transform.position = transform.position;
        return go;
    }
    private void ThrowScrap(GameObject scrap) {
        Rigidbody2D sacrapRB = scrap.GetComponent<Rigidbody2D>();
        float x = MathTool.RandomEx.GetFloat(-1f, 0f);
        float y = MathTool.RandomEx.GetFloat(0f, 1f);
        Vector3 forceDic = new Vector3(x, y, 0);
        sacrapRB.AddForce((transform.up + forceDic) * ScrapForce);
        float t = MathTool.RandomEx.GetFloat(-100, 100);
        sacrapRB.AddTorque(t);
    }
    private void createScraps(int num = -1) {
        if (num < 1)
            num = MathTool.RandomEx.GetInt(1, 4);
        for (int i = 0; i < num; i++) {
            GameObject scrap = getBarrelScrapGO();
            ThrowScrap(scrap);
        }
    }
    #endregion
}
