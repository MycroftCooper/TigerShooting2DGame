using QuickCode;
using UnityEngine;
using static PlayerAnimaController;

public class PlayerMovementController : MonoBehaviour {
    [HideInInspector]
    public Rigidbody2D rb;
    private CharacterCollision cColl;
    private PlayerAnimaController playerAC;


    [Space]
    [Header("Stats")]
    public float RunSensitivity;
    public float RunSpeed = 10;
    public float SlideSpeed = 5;
    public float WallJumpLerp = 10;
    public float DashTime = 0.5f;
    public float DashSpeed = 20;

    [Space]
    [Header("Booleans")]
    public bool CanMove;
    public bool IsDashing;

    [Space]
    [Header("JumpSetting")]
    public ParticleSystem jumpParticle;
    public Vector2Int JumpTimes;
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public GunController Gun;
    private EntityData entityData;
    void Start() {
        cColl = GetComponent<CharacterCollision>();
        rb = GetComponent<Rigidbody2D>();
        playerAC = GetComponentInChildren<PlayerAnimaController>();
        jumpParticle = GetComponent<ParticleSystem>();
        JumpTimes = new Vector2Int(0, 2);
        JumpFlag = true;
        Gun = gameObject.GetComponentInChildren<GunController>();
        entityData = gameObject.GetComponent<EntityData>();
        entityData.OnHitEvent += OnHit;
        entityData.OffHitEvent += OffHit;
        entityData.DeadEvent += OnDead;

    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.O)) {
            entityData.hp.x = entityData.hp.y;
            GameEntry ge = GameEntry.Instance;
            ge._UICtrl.SetHealthNum(entityData.hp);
            ge._GunCtrl.Magazine.x = ge._GunCtrl.Magazine.y;
            ge._UICtrl.SetBulletNum(ge._GunCtrl.Magazine);
            ge._GunCtrl.GrenadNum.x = ge._GunCtrl.GrenadNum.y;
            ge._UICtrl.SetGrenadNum(ge._GunCtrl.GrenadNum);
        }
        if (Input.GetButtonDown("Dash") && !IsDashing && AxisRow != Vector2.zero) {
            Dash_Start();
        }
        if (!CanMove) return;
        playerInput();
        doMovement();
    }
    #region 玩家输入
    [Space]
    [Header("Input")]
    public Vector2 Axis;
    public Vector2 AxisRow;
    public bool JumpFlag;
    private void playerInput() {
        // 用户虚拟轴输入
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Axis = new Vector2(x, y);
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        AxisRow = new Vector2(xRaw, yRaw);
    }
    #endregion

    #region 具体运动控制
    private void doMovement() {
        Run(Axis.x);
        if (AxisRow.y == 1) Jump();
        if (AxisRow.y == 0) JumpFlag = true;
        if (!cColl.OnGround && !cColl.OnWall && !IsDashing) onAir();
        if (!cColl.OnGround && cColl.OnWall) onWallSlide();
    }
    public void Run(float dirX) {
        // 挂墙和不可移动
        if (!CanMove || IsDashing) return;
        // 检测是否大于敏感度
        if (cColl.OnGround && Mathf.Abs(dirX) < RunSensitivity) dirX = 0;

        // 在地面
        if (cColl.OnGround) {
            if (rb.velocity.x == 0 && dirX == 0) {
                playerAC.State = PlayerStates.Stand;
                return;
            } else {
                // 倒着走减速
                if ((playerAC.IsFlip && rb.velocity.x > 0) ||
                    !playerAC.IsFlip && rb.velocity.x < 0) {
                    playerAC.IsRunBack = true;
                    rb.velocity = new Vector2(dirX * RunSpeed / 2, rb.velocity.y);
                } else {
                    playerAC.IsRunBack = false;
                    rb.velocity = new Vector2(dirX * RunSpeed, rb.velocity.y);
                }
                playerAC.State = PlayerStates.Run;
                return;
            }
        }

        rb.velocity = new Vector2(dirX * RunSpeed, rb.velocity.y);
    }
    #region 跳跃相关
    private void onAir() {// 滞空时
        if (rb.velocity.y > 0) { //上升时
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if (rb.velocity.y < 0) { // 下降时
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        playerAC.OnAir();
    }
    public void Jump() {// 跳跃
        if (cColl.OnGround) JumpTimes.x = 0;
        if (JumpTimes.x >= JumpTimes.y || !JumpFlag) return;
        JumpFlag = false;
        JumpTimes.x++;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        playerAC.State = PlayerStates.Jump;
        playerAC.OnJump();
    }
    #endregion
    #region 相位猛冲相关
    public void Dash_Start() { // 相位猛冲
        Gun.CanShot = false;
        playerAC.State = PlayerStates.Dash;
        IsDashing = true;
        rb.velocity = AxisRow.normalized * DashSpeed;
        gameObject.layer = LayerMask.NameToLayer("NoCollision");

        playerAC.OnDash();
        Invoke("Dash_End", DashTime);
        return;
    }
    public void Dash_End() {
        IsDashing = false;
        playerAC.OffDash();
        Gun.CanShot = true;
        rb.velocity = Axis.normalized * RunSpeed;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
    #endregion
    private void onWallSlide() {
        if (rb.velocity.y < 0) {
            playerAC.State = PlayerStates.Climb;
            if (JumpTimes.x == 0) JumpTimes.x = 1;
            if ((AxisRow.x == 1 && cColl.OnRightWall) || (AxisRow.x == -1 && cColl.OnLeftWall)) {
                rb.velocity = new Vector2(0, -SlideSpeed / 2);
            } else {
                rb.velocity = new Vector2(0, -SlideSpeed * 2);
            }
        } else {
            onAir();
        }
    }
    #endregion

    public void OnHit() {
        CanMove = false;
        Gun.CanShot = false;
        rb.velocity = Vector2.zero;
    }
    public void OffHit() {
        CanMove = true;
        Gun.CanShot = true;
    }
    public void OnDead() {
        CanMove = false;
        Gun.CanShot = false;
        rb.velocity = Vector2.zero;
        Timer.Register(2f, () => {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        });
    }

}
