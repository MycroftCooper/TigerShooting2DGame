using QuickCode;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour {
    private EntityData ed;
    public int maxScrapNum;
    public GameObject ScrapPrefab;
    public GameObjectPool ScrapPool;
    public float ScrapForce;

    private Sprite[] sprites;
    private Sprite startSP;
    private Sprite endSP;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private ParticleSystem onhitPS;
    // Start is called before the first frame update
    void Start() {
        ed = GetComponent<EntityData>();
        ScrapPool = new GameObjectPool();
        ScrapPool.InitPool(maxScrapNum, ScrapPrefab, transform);

        sr = gameObject.GetComponent<SpriteRenderer>();
        startSP = sr.sprite;
        sprites = Resources.LoadAll<Sprite>("BarrelScrap");
        endSP = sprites[0];
        rb = gameObject.GetComponent<Rigidbody2D>();
        onhitPS = rb.GetComponent<ParticleSystem>();

        ed.OnHitEvent += OnHit;
        ed.DeadEvent += OnDead;
    }
    private Sprite getBarrelScrapSprite() {
        int index = MathTool.RandomEx.GetInt(1, 17);
        return sprites[index];
    }
    private GameObject getBarrelScrapGO() {
        GameObject go = ScrapPool.GetObject(true);
        go.RemoveComponent<PolygonCollider2D>();
        go.GetComponent<SpriteRenderer>().sprite = getBarrelScrapSprite();
        go.AddComponent<PolygonCollider2D>();
        ScrapPool.Recycle(go, true);
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
    public void OnHit() {
        onhitPS.Play();
        createScraps(-1);
    }
    public void OnDead() {
        sr.sprite = endSP;
        createScraps(5);
        gameObject.layer = LayerMask.NameToLayer("Stacked");
        rb.constraints = RigidbodyConstraints2D.None;
    }
}
