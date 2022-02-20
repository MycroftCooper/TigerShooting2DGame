using DG.Tweening;
using QuickCode;
using System.Collections.Generic;
using UnityEngine;
using static EnemyBehaviourController;

public class EnemyCtrl_Base : MonoBehaviour {
    protected Rigidbody2D rb;
    protected Animator animator;
    protected SpriteRenderer sr;
    protected EnemyBehaviourController behaviourCtrl;
    protected EntityData entityData;

    public ParticleSystem OnHitParticle;
    public ParticleSystem DeadParticle;

    public int Damage = 1;
    public float Speed = 1;
    public Vector2 MoveDir;

    protected bool IsDead { get => entityData.HP <= 0; }
    protected bool IsSleep { get => behaviourCtrl.Behaviour == EnemyBehaviours.Sleep; }
    protected EnemyBehaviours LastBehaviour;
    protected EnemyBehaviours Behaviour {
        get => behaviourCtrl.Behaviour;
        set => behaviourCtrl.Behaviour = value;
    }
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        behaviourCtrl = GetComponent<EnemyBehaviourController>();
        behaviourCtrl.BehaviourChangedEvent += UpdateBehabiours;
        LastBehaviour = EnemyBehaviours.Sleep;

        entityData = GetComponent<EntityData>();
        entityData.OnHitEvent += OnHit;
        entityData.OffHitEvent += OffHit;
        entityData.DeadEvent += OnDead;
    }

    public virtual void UpdateBehabiours() {
        if (LastBehaviour == EnemyBehaviours.Sleep) {
            OnAweak();
        }
        LastBehaviour = Behaviour;
    }
    public virtual void OnAttack() {
        animator.SetTrigger("Attack");
        behaviourCtrl.Player.OnHit(Damage);
    }
    public virtual void OnHit() {
        animator.SetTrigger("OnHit");
        rb.velocity = Vector2.zero;
        transform.DOShakePosition(1, 1f);
        OnHitParticle.Play();
        GameEntry.Instance._AudioManager.PlaySFX(AudioResCtrl.EnemyOnHitClip);
    }
    public virtual void OffHit() {
        OnHitParticle.Stop();
    }
    public virtual void OnDead() {
        List<Collider2D> inRange = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        LayerMask layerMask = LayerMask.NameToLayer("Ground");
        filter.SetLayerMask(layerMask);
        Physics2D.OverlapCircle((Vector2)transform.position, 10, filter, inRange);
        int count = inRange.FindAll(p => p.tag == "Enemies").Count;
        if (count == 1)
            GameEntry.Instance._CameraCtrl.OnEnemyDead();

        Behaviour = EnemyBehaviours.Sleep;
        animator.ResetTrigger("OnHit");
        animator.SetBool("IsDead", true);
        DeadParticle.Play();
        gameObject.layer = LayerMask.NameToLayer("Stacked");
        Timer.Register(2f, () => {
            rb.Sleep();
            OnHitParticle.Stop();
        });
    }
    public virtual void OnReset() {
        animator.SetBool("IsDead", false);
        sr.enabled = true;
        entityData.hp.x = entityData.hp.y;
    }
    public virtual void OnAweak() {
        animator.SetTrigger("Aweak");
        gameObject.layer = LayerMask.NameToLayer("Default");
        rb.WakeUp();
        OnHitParticle.Stop();
    }
}
