using QuickCode;
using System;
using UnityEngine;

public class EntityData : MonoBehaviour {
    public bool IsEnemy;
    public Vector2Int hp;
    public int HP { get => hp.x; set => hp.x = value < 0 ? 0 : value; }
    public int MaxHP { get => hp.y; }

    public bool IsOnHit = false;
    public float OnHitTime = 1f;

    public event Action OnHitEvent;
    public event Action OffHitEvent;
    public event Action DeadEvent;

    public void OnHit(int damage) {
        if (HP <= 0) return;
        if (IsOnHit) return;
        IsOnHit = true;
        HP -= damage;
        if (OnHitEvent != null) OnHitEvent();
        if (HP <= 0 && DeadEvent != null) DeadEvent();
        else Timer.Register(OnHitTime, () => {
            IsOnHit = false;
            if (OffHitEvent != null) OffHitEvent();
        });
    }
}
