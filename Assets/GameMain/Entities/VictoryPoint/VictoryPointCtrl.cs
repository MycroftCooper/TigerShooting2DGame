using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VictoryPointCtrl : MonoBehaviour {
    public Transform Statue;
    public float EnableRange;// 胜利点范围
    public float NowScore;
    public float TargetScore;// 目标分数
    public float GetScorePerSec;// 生效时每秒点数
    public float LoseScorePerSec;// 失效时每秒点数
    private bool isEnable;
    public bool IsVictory;
    public bool IsEnable {
        get => isEnable;
        set {
            if (value == isEnable) return;
            isEnable = value;
            if (isEnable) animator.SetTrigger("Enable");
            else animator.SetTrigger("Disable");
        }
    }

    private Animator animator;

    private float highestPoint = 5f;
    private float lowestPoint = 2f;
    private float pointRange { get => highestPoint - lowestPoint; }
    private float nowHight {
        get => Statue.transform.localPosition.y;
        set {
            float v = value;
            if (value < lowestPoint) v = lowestPoint;
            if (value > highestPoint) v = highestPoint;
            Statue.transform.localPosition = new Vector3(Statue.transform.localPosition.x, v, Statue.transform.localPosition.z);
        }
    }

    void Start() {
        animator = GetComponentInChildren<Animator>();
        Statue = transform.Find("Statue");
        IsVictory = false;
    }

    void Update() {
        esc();
        if (IsVictory) return;
        updateScore();
        updateHight();
    }
    private void esc() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
    private void updateScore() {
        int state = IsInRange();
        if (state == 0) {
            GameEntry.Instance._EnemyCreaterCtrl.IsUpdate = false;
            IsEnable = false;
            NowScore -= Time.deltaTime * LoseScorePerSec;
            if (NowScore < 0) NowScore = 0;
            GameEntry.Instance._UICtrl.SetProgressBar(false);
            return;
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            NowScore += Time.deltaTime * GetScorePerSec * 2;
        }
        GameEntry.Instance._EnemyCreaterCtrl.IsUpdate = true;
        GameEntry.Instance._UICtrl.SetProgressBar(true);
        GameEntry.Instance._UICtrl.SetProgressNum(NowScore / TargetScore);
        IsEnable = true;
        if (state == 1) return;
        NowScore += Time.deltaTime * GetScorePerSec;
        if (NowScore >= TargetScore) {
            NowScore = TargetScore;
            IsVictory = true;
            GameEntry.Instance._UICtrl.SetWinUI(true);
            GameEntry.Instance._EnemyCreaterCtrl.IsUpdate = false;
        }
    }
    private void updateHight() {
        nowHight = (NowScore / TargetScore) * pointRange + lowestPoint;
    }
    private int IsInRange() {// 判断是否在胜利点内 0:不在 1:人怪都在 2:仅有人在
        List<Collider2D> inRange = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        LayerMask layerMask = LayerMask.NameToLayer("Ground");
        filter.SetLayerMask(layerMask);
        int count = Physics2D.OverlapCircle((Vector2)transform.position, EnableRange, filter, inRange);
        if (count == 0) return 0;
        if (!inRange.Any(p => p.tag == "Player")) return 0;
        if (inRange.Any(p => p.tag == "Enemies")) return 1;
        return 2;
    }
}
