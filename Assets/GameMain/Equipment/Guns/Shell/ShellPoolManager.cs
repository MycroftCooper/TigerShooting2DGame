using QuickCode;
using UnityEngine;

public class ShellPoolManager : MonoBehaviour {
    public GameObject ShellPrefab;
    public Vector2 InitOffset;
    public float ThrowForce;
    public float TorqueForce;
    public int MaxShellNum = 100;
    public GameObjectPool ShellPool;

    void Start() {
        GetComponent<GunController>().FireAction += GetShellOnFire;
        ShellPool = new GameObjectPool();
        ShellPool.InitPool(MaxShellNum, ShellPrefab, GameObject.Find("Shells").transform);
    }
    public void GetShellOnFire() {
        GameObject newShell = ShellPool.GetObject(false);
        newShell.transform.position = transform.position + (Vector3)InitOffset;
        newShell.transform.rotation = transform.rotation;
        ShellPool.Recycle(newShell, true);
        ThrowShell(newShell);
    }
    public void ThrowShell(GameObject shell) {
        Rigidbody2D shellRB = shell.GetComponent<Rigidbody2D>();
        float x = MathTool.RandomEx.GetFloat(-1f, 0f);
        float y = MathTool.RandomEx.GetFloat(0f, 1f);
        Vector3 forceDic = new Vector3(x, y, 0);
        shellRB.AddForce((transform.up + forceDic) * ThrowForce);
        float t = MathTool.RandomEx.GetFloat(-TorqueForce, TorqueForce);
        shellRB.AddTorque(t);
        Timer.Register(0.5f, () => {
            GameEntry.Instance._AudioManager.PlaySFX(AudioResCtrl.ShellClip);
        });
    }
}
