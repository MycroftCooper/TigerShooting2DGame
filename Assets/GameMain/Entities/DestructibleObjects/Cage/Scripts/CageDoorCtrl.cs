using System.Collections.Generic;
using UnityEngine;

public class CageDoorCtrl : MonoBehaviour {
    public EntityData ED;
    public Sprite startSprite;
    public Sprite endSprite;
    public SpriteRenderer sr;
    public Rigidbody2D rb;
    public ParticleSystem OnHitParticle;
    public float BreakForce;

    void Start() {
        OnHitParticle = GetComponent<ParticleSystem>();
        ED = GetComponent<EntityData>();
        ED.DeadEvent += OnBreak;
        ED.OnHitEvent += OnHit;
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        List<Sprite> sprites = new List<Sprite>(Resources.LoadAll<Sprite>("Door"));
        startSprite = sprites.Find(p => p.name == "Door_0");
        endSprite = sprites.Find(p => p.name == "Door_1");
        sr.sprite = startSprite;
    }

    public void OnHit() {
        OnHitParticle.Play();
    }
    public void OnBreak() {
        sr.sprite = endSprite;
        gameObject.layer = LayerMask.NameToLayer("NoCollision");
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(Vector2.right * BreakForce);
    }
}
