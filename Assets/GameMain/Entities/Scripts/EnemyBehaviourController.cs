using QuickCode;
using System;
using UnityEngine;

public class EnemyBehaviourController : MonoBehaviour {
    public enum EnemyBehaviours { Sleep, Wander, GetClose, Attack, GetAway }
    private EnemyBehaviours behaviour;
    public EnemyBehaviours Behaviour {
        get => behaviour;
        set {
            if (value == behaviour) return;
            behaviour = value;
            BehaviourChangedEvent();
        }
    }
    public Action BehaviourChangedEvent;
    public float UpdateTimeInterval;
    public bool IsUpdate {
        get => timer.isPaused;
        set {
            if (!value) timer.Pause();
            else timer.Resume();
        }
    }
    public bool IsRandom;

    public float AweakRange;
    public float AttackRange;
    public float GetAwayHP;

    public EntityData CharacterData;
    public EntityData Player;
    private Timer timer;
    void Awake() {
        CharacterData = gameObject.GetComponent<EntityData>();
        Player = GameObject.Find("Player").GetComponent<EntityData>();
        Behaviour = EnemyBehaviours.Sleep;
        timer = Timer.Register(UpdateTimeInterval, updateBehaviour, isLooped: true);
    }

    // 怪物状态更新
    private void updateBehaviour() {
        if (CharacterData.HP <= 0) {// 怪物死亡
            IsUpdate = false;
            Behaviour = EnemyBehaviours.Sleep;
            return;
        }

        if (Player.GetComponent<EntityData>().HP <= 0) {// 玩家死亡
            Behaviour = EnemyBehaviours.Wander;
            return;
        }

        if (Behaviour == EnemyBehaviours.Sleep && !isInAweakRange()) return;// 玩家不在唤醒范围内

        if (IsRandom) UpdateBehaviour_Random();// 随机更新状态
        else UpdateBehaviour();//规则更新状态
    }
    public EnemyBehaviours UpdateBehaviour() {
        if (Behaviour == EnemyBehaviours.GetAway) UpdateBehaviour_Random();
        else {
            if (shouldGetAwey()) Behaviour = EnemyBehaviours.GetAway;
            else {
                if (isInAttackRange()) Behaviour = EnemyBehaviours.Attack;
                else Behaviour = EnemyBehaviours.GetClose;
            }
        }
        return Behaviour;
    }
    public EnemyBehaviours UpdateBehaviour_Random() {
        int random = MathTool.RandomEx.GetInt(1, 5);
        if (random == 0) UpdateBehaviour();
        else Behaviour = (EnemyBehaviours)random;
        return Behaviour;
    }

    private bool isInAttackRange() =>
        Vector3.Distance(transform.position, Player.transform.position) <= AttackRange;
    private bool isInAweakRange() =>
        Vector3.Distance(transform.position, Player.transform.position) <= AweakRange;
    private bool shouldGetAwey()
        => CharacterData.HP <= GetAwayHP;

}
