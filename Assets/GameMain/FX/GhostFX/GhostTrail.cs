using DG.Tweening;
using UnityEngine;

public class GhostTrail : MonoBehaviour {
    private PlayerAnimaController anim;
    public Color trailColor;
    public Color fadeColor;
    public float ghostInterval;
    public float fadeTime;

    private void Start() {
        Init();
    }

    public void Init() {
        for (int i = 0; i < transform.childCount; i++) {
            Transform currentGhost = transform.GetChild(i);
            SpriteRenderer sr = currentGhost.GetComponent<SpriteRenderer>();
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            sr.material.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f);
        }
        anim = FindObjectOfType<PlayerAnimaController>();
    }

    public void ShowGhost() {
        Sequence s = DOTween.Sequence();
        for (int i = 0; i < transform.childCount; i++) {
            Transform currentGhost = transform.GetChild(i);
            SpriteRenderer sr = currentGhost.GetComponent<SpriteRenderer>();
            s.AppendCallback(() => AddGhost(sr));
            s.AppendInterval(ghostInterval);
        }
        for (int i = 0; i < transform.childCount; i++) {
            Transform currentGhost = transform.GetChild(i);
            SpriteRenderer sr = currentGhost.GetComponent<SpriteRenderer>();
            s.AppendCallback(() => sr.material.DOColor(new Color(sr.color.r, sr.color.g, sr.color.b, 0), fadeTime));
            s.AppendInterval(fadeTime);
        }
    }
    public void AddGhost(SpriteRenderer sr) {
        sr.transform.position = anim.transform.position;
        sr.material.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.6f);
        sr.flipX = anim.IsFlip;
        sr.sprite = anim.sr.sprite;
    }

    //public void FadeSprite(Transform current) {
    //    SpriteRenderer sr = current.GetComponent<SpriteRenderer>();
    //    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
    //}

}
