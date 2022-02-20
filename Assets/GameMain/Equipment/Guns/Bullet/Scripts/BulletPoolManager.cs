using QuickCode;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour {
    public GameObject Gun;
    public GameObject BulletPrefab;
    public Vector2 InitOffset;
    public int MaxBulletNum = 100;
    public GameObjectPool BulletPool;
    public bool IsEnemyBullet = false;

    void Start() {
        Gun = gameObject;
        BulletPool = new GameObjectPool();
        BulletPool.InitPool(MaxBulletNum, BulletPrefab, GameObject.Find("Bullets").transform);
    }
    public BulletController GetBullet() {
        GameObject newBullet = BulletPool.GetObject(false);
        newBullet.transform.position = Gun.transform.TransformPoint(InitOffset);
        newBullet.transform.rotation = Gun.transform.rotation;
        BulletPool.Recycle(newBullet, true);
        BulletController bc = newBullet.GetComponent<BulletController>();
        bc.IsEnemy = false;
        return bc;
    }
}
