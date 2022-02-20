using System.Collections.Generic;
using UnityEngine;

public class SightController : MonoBehaviour {
    private static class SightResouses {
        private static List<Sprite> spritesList = new List<Sprite>(Resources.LoadAll<Sprite>("Line"));
        private static Dictionary<string, Texture2D> textureList = new Dictionary<string, Texture2D>{
            {"Sight_On", Resources.Load<Texture2D>("Sight_On")},
            {"Sight_On_Fire", Resources.Load<Texture2D>("Sight_On_Fire")},
            {"Sight_Off", Resources.Load<Texture2D>("Sight_Off")},
            {"Sight_Off_Fire", Resources.Load<Texture2D>("Sight_Off_Fire")},
        };
        private static Sprite GetResouseSprite(string name) =>
            spritesList.Find(x => x.name == name);
        public static Texture2D Sight_On { get => textureList["Sight_On"]; }
        public static Texture2D Sight_On_Fire { get => textureList["Sight_On_Fire"]; }
        public static Texture2D Sight_Off { get => textureList["Sight_Off"]; }
        public static Texture2D Sight_Off_Fire { get => textureList["Sight_Off_Fire"]; }
        public static Sprite Sight_On_Line { get => GetResouseSprite("Sight_On_Line"); }
        public static Sprite Sight_Off_Line { get => GetResouseSprite("Sight_Off_Line"); }
    }

    public Vector2 MousePos;
    public Vector2 SightPos;
    public Vector2 StartPosOffset;

    void Start() {
        IsOnFire = false;
        IsOnEntity = false;
        sightLineRenderer = GetComponentInChildren<SpriteRenderer>();
        sightLineRenderer.sprite = SightResouses.Sight_Off_Line;
        Cursor.SetCursor(SightResouses.Sight_Off, hotSpot, CursorMode.Auto);
    }

    void Update() {
        updateSight();
        updateLine();
    }
    #region 准星相关
    [Header("Sight")]
    public Vector2 hotSpot;
    public bool IsOnFire;
    public bool IsOnEntity;

    [Header("Collision")]
    public LayerMask EnemiesLayer;
    public Vector2 CollisionOffset;
    public float collisionRadius = 0.25f;
    private Color debugCollisionColor = Color.green;
    private void updateSight() {
        updateMousePos();
        updateOnEntity();
        updateOnFire();
    }
    private void updateMousePos() {
        Vector3 screenMousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        MousePos = Camera.main.ScreenToWorldPoint(screenMousePos);
        SightPos = MousePos + CollisionOffset;
    }
    private void updateSightResouses() {
        if (IsOnEntity) {
            sightLineRenderer.sprite = SightResouses.Sight_On_Line;
            if (IsOnFire) {
                Cursor.SetCursor(SightResouses.Sight_On_Fire, hotSpot, CursorMode.Auto);
            } else {
                Cursor.SetCursor(SightResouses.Sight_On, hotSpot, CursorMode.Auto);
            }
        } else {
            sightLineRenderer.sprite = SightResouses.Sight_Off_Line;
            if (IsOnFire) {
                Cursor.SetCursor(SightResouses.Sight_Off_Fire, hotSpot, CursorMode.Auto);
            } else {
                Cursor.SetCursor(SightResouses.Sight_Off, hotSpot, CursorMode.Auto);
            }
        }
    }
    private void updateOnFire() {
        if (Input.GetButton("Fire1")) {
            if (IsOnFire) return;
            IsOnFire = true;
            updateSightResouses();
            return;
        }
        if (!IsOnFire) return;
        IsOnFire = false;
        updateSightResouses();
    }
    private void updateOnEntity() {
        Collider2D entity = Physics2D.OverlapCircle((Vector2)MousePos + CollisionOffset, collisionRadius, EnemiesLayer);
        if (entity != null && entity.tag == "Enemies") {
            if (IsOnEntity) return;
            IsOnEntity = true;
            updateSightResouses();
            return;
        }
        if (!IsOnEntity) return;
        IsOnEntity = false;
        updateSightResouses();
    }
    private void OnDrawGizmos() {
        Gizmos.color = debugCollisionColor;
        Gizmos.DrawWireSphere(SightPos, collisionRadius);
    }
    #endregion

    #region 准线相关
    public float LineWide = 0.15f;
    private SpriteRenderer sightLineRenderer;
    private void updateLine() {
        Vector2 pos = transform.parent.position + (Vector3)StartPosOffset;

        float d = Vector2.Distance(pos, SightPos);
        sightLineRenderer.size = new Vector2(d + 0.2f, LineWide);
    }
    #endregion
}
