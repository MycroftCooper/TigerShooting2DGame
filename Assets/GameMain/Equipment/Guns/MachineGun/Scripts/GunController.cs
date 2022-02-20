using System;
using UnityEngine;

public class GunController : MonoBehaviour {
    private bool canShot;
    public bool CanShot {
        get => canShot;
        set {
            if (value == canShot) return;
            canShot = value;
            if (!canShot) State = GunStates.Hold;
        }
    }
    private Animator animator;
    public ParticleSystem FireParticle;
    public SightController SC;
    public BulletPoolManager BP;
    public int BulletSpeed;
    public Action FireAction;
    public Action ThrowGrenadeAction;
    public Vector2Int Magazine;
    public float Recoil;// 后坐力
    public enum GunStates { Hold, SingleShot, TripleShot, Reloading };
    public GunStates State {
        set {
            if (State == value) return;
            animator.SetInteger("GunStates", (int)value);
        }
        get {
            return (GunStates)animator.GetInteger("GunStates");
        }
    }

    void Start() {
        CanShot = true;
        BP = GetComponentInChildren<BulletPoolManager>();
        SC = GetComponentInChildren<SightController>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        lookAtSight();
        stateUpdate();
        if (Input.GetKeyDown(KeyCode.G)) useGrenad();
    }

    private void stateUpdate() {
        if (Input.GetKeyDown(KeyCode.R)) {
            State = GunStates.Reloading;
            GameEntry.Instance._AudioManager.PlaySFX(AudioResCtrl.ReloadClip);
            return;
        }
        if (Magazine.x <= 0) {
            Magazine.x = 0;
            CanShot = false;
        }
        if (!CanShot) {
            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
                GameEntry.Instance._AudioManager.PlaySFX(AudioResCtrl.NoMagazineClip);
            return;
        }
        if (Input.GetButton("Fire1")) {
            State = GunStates.SingleShot; return;
        }
        if (Input.GetButtonDown("Fire2")) {
            if (Magazine.x < 3) {
                GameEntry.Instance._AudioManager.PlaySFX(AudioResCtrl.NoMagazineClip);
                return;
            }
            State = GunStates.TripleShot;
            return;
        }
        State = GunStates.Hold;
    }
    private void lookAtSight() {
        Vector2 SightPos = SC.SightPos;
        transform.LookAt(new Vector3(SightPos.x, SightPos.y));
        transform.Rotate(new Vector3(0, -90, 0));
    }

    #region 动画卡帧回调
    private void reload() {
        Magazine.x = Magazine.y;
        canShot = true;
        State = GunStates.Hold;
        GameEntry.Instance._UICtrl.SetBulletNum(Magazine);
    }
    private void singleShot() {
        FireAction();
        Magazine.x--;
        doRecoil(Recoil);
        var bullet = BP.GetBullet();
        bullet.OnFire(Vector2.right, BulletSpeed);
        GameEntry.Instance._CameraCtrl.OnPlayerShot(1);
        GameEntry.Instance._UICtrl.SetBulletNum(Magazine);
    }
    private void tripleShot() {
        FireAction();
        Magazine.x -= 3;
        doRecoil(Recoil * 3);
        var bullet1 = BP.GetBullet();
        var bullet2 = BP.GetBullet();
        var bullet3 = BP.GetBullet();
        bullet1.OnFire(new Vector2(1, 0.25f), BulletSpeed);
        bullet2.OnFire(new Vector2(1, 0), BulletSpeed);
        bullet3.OnFire(new Vector2(1, -0.25f), BulletSpeed);
        if (FireParticle != null) FireParticle.Play();
        GameEntry.Instance._CameraCtrl.OnPlayerShot(3);
        GameEntry.Instance._UICtrl.SetBulletNum(Magazine);
    }
    #endregion

    private void doRecoil(float force) {// 后坐力
        Rigidbody2D playRB = transform.parent.GetComponent<Rigidbody2D>();
        Vector3 screenMousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenMousePos);
        if (mousePos.x < transform.position.x)
            playRB.MovePosition(playRB.transform.TransformPoint(Vector2.right * force));
        else
            playRB.MovePosition(playRB.transform.TransformPoint(Vector2.left * force));
    }

    #region 丢手雷
    public Vector2Int GrenadNum;
    private void useGrenad() {
        if (GrenadNum.x == 0) return;
        ThrowGrenadeAction();
        GrenadNum.x--;
        GameEntry.Instance._UICtrl.SetGrenadNum(GrenadNum);
    }
    #endregion
}
