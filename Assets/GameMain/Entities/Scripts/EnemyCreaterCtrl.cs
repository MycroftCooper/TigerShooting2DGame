using QuickCode;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreaterCtrl : MonoBehaviour {
    private bool isUpdate;
    public bool IsUpdate {
        get => isUpdate;
        set {
            if (value == isUpdate) return;
            isUpdate = value;
            if (value) timer.Resume();
            else timer.Pause();
        }
    }
    public Vector2Int EnemyNum_All;// 总刷敌数
    public Vector2Int EnemyNum_Now;// 同时在场敌人数
    public float UpdateTime;
    private Timer timer;
    public List<Transform> EnemyPoints = new List<Transform>();
    public List<GameObject> EnemyPrefabs = new List<GameObject>();
    public List<EntityData> Enemies;
    private Transform enemiesParent;

    void Start() {
        enemiesParent = GameObject.Find("Enemies").transform;
        timer = this.AttachTimer(UpdateTime, updateEnemyNum, isLooped: true);
        timer.Pause();
        IsUpdate = false;
        loadAllEnemies();
    }

    private void loadAllEnemies() {
        Enemies = new List<EntityData>();
        List<GameObject> e = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemies"));
        e.ForEach((p) => {
            EntityData ed = p.GetComponent<EntityData>();
            if (ed != null) Enemies.Add(ed);
        });
        e.Clear();
    }
    private void updateEnemyNum() {
        for (int i = Enemies.Count - 1; i >= 0; i--) {
            if (Enemies[i].HP > 0) continue;
            Enemies.RemoveAt(i);
            EnemyNum_Now.x--;
        }
        if (EnemyNum_All.x >= EnemyNum_All.y || EnemyNum_Now.x >= EnemyNum_Now.y) return;
        CreateEnemy(-1, -1);
        EnemyNum_All.x++;
        EnemyNum_Now.x++;
    }
    public GameObject CreateEnemy(int pointIndex, int enemyIndex) {
        if (pointIndex > EnemyPoints.Count || pointIndex < 0)
            pointIndex = MathTool.RandomEx.GetInt(0, EnemyPoints.Count);
        if (enemyIndex > EnemyPrefabs.Count || enemyIndex < 0)
            enemyIndex = MathTool.RandomEx.GetInt(0, EnemyPrefabs.Count);
        if (EnemyPoints[pointIndex] == null || EnemyPrefabs[enemyIndex] == null) return null;

        GameObject enemy = Instantiate(EnemyPrefabs[enemyIndex], enemiesParent, true);
        enemy.transform.position = EnemyPoints[pointIndex].position;
        EnemyBehaviourController ebc = enemy.GetComponent<EnemyBehaviourController>();
        ebc.Behaviour = EnemyBehaviourController.EnemyBehaviours.GetClose;
        ebc.BehaviourChangedEvent();
        ebc.IsUpdate = false;
        Enemies.Add(enemy.GetComponent<EntityData>());
        return enemy;
    }
}
