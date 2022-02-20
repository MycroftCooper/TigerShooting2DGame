using QuickCode;
using UnityEngine;

public class PlayerAnimaController : MonoBehaviour {
    public SpriteRenderer sr;
    private Animator animator;
    private CharacterCollision cColl;
    private EntityData entityData;
    private CameraCtrl cameraCtrl;
    public enum PlayerStates { Stand = 0, Run = 1, Jump = 2, Climb = 3, Dash = 4, OnHit = 5, Dead = 6 };
    private PlayerStates state;
    public PlayerStates State {
        get {
            state = (PlayerStates)animator.GetInteger("PlayerStates");
            return state;
        }
        set {
            if (state == value || state == PlayerStates.Dead)
                return;
            animator.SetInteger("PlayerStates", (int)value);
            state = (PlayerStates)animator.GetInteger("PlayerStates");
        }
    }
    private bool isRunBack;
    public bool IsRunBack {
        get {
            isRunBack = animator.GetBool("IsRunBack");
            return isRunBack;
        }
        set {
            if (value == IsRunBack) return;
            isRunBack = value;
            animator.SetBool("IsRunBack", isRunBack);
        }
    }

    private void Start() {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cColl = GetComponent<CharacterCollision>();
        entityData = gameObject.GetComponent<EntityData>();
        entityData.OnHitEvent += OnHit;
        entityData.OffHitEvent += OffHit;
        entityData.DeadEvent += OnDead;
        cameraCtrl = GameEntry.Instance._CameraCtrl;
    }
    private void Update() {
        updateFlip();
        if (cColl.OnGround || cColl.OnWall) OffAir();
    }

    #region 反转相关
    public bool IsFlip {
        get => sr.flipX;
        set {
            if (value == sr.flipX) return;
            sr.flipX = value;
        }
    }
    private void updateFlip() {
        if (State == PlayerStates.Dead) return;
        if (State == PlayerStates.Climb) {
            if (cColl.OnLeftWall) IsFlip = true;
            else IsFlip = false;
            OnClimb();
            return;
        }
        OffClimb();
        Vector3 screenMousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenMousePos);
        if (mousePos.x < transform.position.x) IsFlip = true;
        else IsFlip = false;
    }
    #endregion

    #region 动画回调函数
    #region 跳跃相关
    [Space]
    [Header("JumpFX")]
    public GameObject JumpSmoke;
    public Vector2 JumpFXOffset;
    public float JumponWallOffset = 0.2f;
    public GameObject OnAirSmoke;
    public void OnJump() {
        if (JumpSmoke != null) {
            Vector3 offset = JumpFXOffset;
            Quaternion rotation = Quaternion.Euler(0f, 0f, 0f);
            if (!cColl.OnGround && cColl.OnWall) {
                if (IsFlip) {
                    rotation = Quaternion.Euler(0f, 0f, -90f);
                    offset = new Vector3(-JumponWallOffset, 0f, 0f);
                } else {
                    rotation = Quaternion.Euler(0f, 0f, 90f);
                    offset = new Vector3(JumponWallOffset, 0f, 0f);
                }
            }
            JumpSmoke.transform.rotation = rotation;
            JumpSmoke.transform.position = transform.position + offset;
            JumpSmoke.SetActive(true);
        }
    }
    public void OnAir() {
        if (!OnAirSmoke.activeSelf && !cColl.OnWall) {
            GameEntry.Instance._AudioManager.PlaySFX(AudioResCtrl.PlayerJumpClip);
            OnAirSmoke.SetActive(true);
        }
    }
    public void OffAir() {
        if (OnAirSmoke.activeSelf && !cColl.OnWall) {
            OnAirSmoke.SetActive(false);
            GameEntry.Instance._AudioManager.PlaySFX(AudioResCtrl.PlayerJumpClip);
        }
    }
    #endregion

    #region 爬墙相关
    [Space]
    [Header("ClimbFX")]
    public GameObject ClimbFX;
    public Vector2 ClimbFXOffset;
    AudioSource source;
    public void OnClimb() {
        if (ClimbFX.activeSelf) return;
        if (source == null)
            source = GameEntry.Instance._AudioManager.PlaySFXLoop(AudioResCtrl.PlayerClimbClip);
        if (JumpSmoke != null && !ClimbFX.activeSelf) {
            Vector3 offset;
            if (IsFlip) {
                offset = new Vector3(-ClimbFXOffset.x, ClimbFXOffset.y, 0);
            } else {
                offset = new Vector3(ClimbFXOffset.x, ClimbFXOffset.y, 0);
            }
            ClimbFX.transform.position = transform.position + offset;
            ClimbFX.SetActive(true);
        }
    }
    public void OffClimb() {
        if (JumpSmoke != null) {
            if (source != null) {
                GameEntry.Instance._AudioManager.StopSFXLoop(source);
                source = null;
            }
            ClimbFX.SetActive(false);
        }
    }
    #endregion

    #region 冲锋相关
    [Space]
    [Header("DashFX")]
    public GameObject DashFX;
    public void OnDash() {
        DashFX.SetActive(true);
        FindObjectOfType<GhostTrail>().ShowGhost();
        cameraCtrl.OnPlayerDash();
        GameEntry.Instance._AudioManager.PlaySFX(AudioResCtrl.PlayerDashClip);
    }
    public void OffDash() {
        animator.SetBool("IsDashing", false);
        DashFX.SetActive(false);
    }
    #endregion

    public void OnHit() {
        State = PlayerStates.OnHit;
        GameEntry.Instance._UICtrl.SetHealthNum(entityData.hp);
        Timer.Register(0.2f, cameraCtrl.OnPlayerOnHit);
        GameEntry.Instance._AudioManager.PlaySFX(AudioResCtrl.EnemyOnHitClip);
    }
    public void OffHit() {
        if (entityData.HP == 0) {
            OnDead();
        } else {
            State = PlayerStates.Stand;
        }
    }
    public void OnDead() {
        State = PlayerStates.Dead;
        cameraCtrl.OnPlayerDead();
        GameEntry.Instance._UICtrl.SetGG(true);
    }
    public void OnMove() {
        GameEntry.Instance._AudioManager.PlaySFX(AudioResCtrl.FootstepClip);
    }
    #endregion
}
