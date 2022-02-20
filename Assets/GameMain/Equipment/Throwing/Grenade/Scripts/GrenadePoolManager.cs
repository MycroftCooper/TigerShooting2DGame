using QuickCode;
using UnityEngine;

public class GrenadePoolManager : MonoBehaviour {
    public GameObject GrenadePrefab;
    public Vector2 InitOffset;
    public float ThrowForce;
    public float TorqueForce;
    public int MaxGrenadeNum = 100;
    public GameObjectPool GrenadePool;

    void Start() {
        gameObject.GetComponent<GunController>().ThrowGrenadeAction += GetGrenadeOnThrow;
        GrenadePool = new GameObjectPool();
        GrenadePool.InitPool(MaxGrenadeNum, GrenadePrefab, GameObject.Find("Grenades").transform);
    }

    public void GetGrenadeOnThrow() {
        GameObject newGrenade = GrenadePool.GetObject(false);
        var gc = newGrenade.GetComponent<GrenadeCtrl>();
        gc.OnThrowing();
        newGrenade.transform.position = transform.position + (Vector3)InitOffset;
        newGrenade.transform.rotation = transform.rotation;
        GrenadePool.Recycle(newGrenade, true);
        ThrowGrenade(newGrenade);
    }
    public void ThrowGrenade(GameObject grenade) {
        Rigidbody2D grenadeRB = grenade.GetComponent<Rigidbody2D>();
        Vector3 screenMousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenMousePos);
        Vector3 forceDic = ((Vector3)mousePos - transform.position).normalized;
        grenadeRB.AddForce(forceDic * ThrowForce);
        float t = MathTool.RandomEx.GetFloat(-TorqueForce, TorqueForce);
        grenadeRB.AddTorque(t);
    }
}
