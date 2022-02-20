using QuickCode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntry : MonoSingleton<GameEntry> {
    public CameraCtrl _CameraCtrl;
    public UICtrl _UICtrl;
    public GunController _GunCtrl;
    public EntityData _PlayerData;
    public EnemyCreaterCtrl _EnemyCreaterCtrl;
    public VictoryPointCtrl _VictoryPointCtrl;
    public AudioManager _AudioManager;

    void Awake() {
        _CameraCtrl = GameObject.FindObjectOfType<CameraCtrl>();
        _UICtrl = GameObject.FindObjectOfType<UICtrl>();
        _GunCtrl = GameObject.FindObjectOfType<GunController>();
        _PlayerData = GameObject.Find("Player").GetComponent<EntityData>();
        _EnemyCreaterCtrl = GameObject.FindObjectOfType<EnemyCreaterCtrl>();
        _VictoryPointCtrl = GameObject.Find("VictoryPoint").GetComponent<VictoryPointCtrl>();
        _AudioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }

    }
}
