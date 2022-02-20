using QuickCode;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeCtrl : MonoBehaviour {
    public bool IsEnemy;
    public Animator animator;
    public SpriteRenderer sr;

    public GameObject BurningFX;
    public GameObject ExplodeFX;
    public GameObject SmokeFX;

    public float ExplodedRadius;
    public int Damage;
    public float SmokeTime = 10f;
    public float BeatBackForce = 10f;

    private CameraCtrl cameraCtrl;
    private void Awake() {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cameraCtrl = GameEntry.Instance._CameraCtrl;
    }

    public void OnThrowing() {
        gameObject.layer = LayerMask.NameToLayer("Default");
        animator.SetTrigger("Reset");
        BurningFX.SetActive(true);
        ExplodeFX.SetActive(false);
        SmokeFX.SetActive(false);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
    }
    public void OnExploded() {
        if (ExplodeFX.activeSelf) return;
        BurningFX.SetActive(false);
        ExplodeFX.SetActive(true);
        SmokeFX.SetActive(true);
        doDamage();
        cameraCtrl.OnExploded();
        GameEntry.Instance._AudioManager.PlaySFX(AudioResCtrl.ExplosionClip);
    }

    public void OnReset() {
        BurningFX.SetActive(false);
        this.AttachTimer(SmokeTime, () => SmokeFX.SetActive(false));
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        gameObject.layer = LayerMask.NameToLayer("Stacked");
    }

    private void doDamage() {
        List<Collider2D> inRange = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.NoFilter();
        int count = Physics2D.OverlapCircle((Vector2)transform.position, ExplodedRadius, filter, inRange);
        if (count == 0) return;
        foreach (var go in inRange) {
            EntityData ed = null;
            if ((go.tag == "Player" && IsEnemy) ||
                 (go.tag == "Enemies" && !IsEnemy) ||
                 (go.tag == "Obstacles")) ed = go.GetComponent<EntityData>();
            if (ed == null) continue;

            var targetRB = go.gameObject.GetComponent<Rigidbody2D>();
            if (targetRB != null && tag != "Obstacles") {// 击退力
                Vector2 force = (go.transform.position - transform.position).normalized * BeatBackForce;
                targetRB.AddForce(force);
            }
            ed.OnHit(Damage);
        }
    }
}
