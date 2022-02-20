using QuickCode;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICtrl : MonoBehaviour {
    void Start() {
        initBulletUI();
        initGrenadUI();
        initHealthUI();
        initProgressBar();
    }
    #region 子弹UI
    public Image Img_BulletNum;
    public Text Text_BulletNum;
    private void initBulletUI() {
        Img_BulletNum = GameObject.Find("Img_BulletNum").GetComponent<Image>();
        Text_BulletNum = GameObject.Find("Text_BulletNum").GetComponent<Text>();
    }
    public void SetBulletNum(Vector2Int num) {
        Text_BulletNum.text = num.x.ToString() + '/' + num.y.ToString();
        float amount = (float)num.x / (float)num.y;
        Img_BulletNum.fillAmount = amount;
    }
    #endregion

    #region 手榴弹UI
    public GameObject GrenadUI;
    public Image Img_GrenadNum;
    private void initGrenadUI() {
        GrenadUI = GameObject.Find("GrenadUI");
        Img_GrenadNum = GrenadUI.GetComponent<Image>();
    }
    public void SetGrenadNum(Vector2Int num) {
        float amount = (float)num.x / (float)num.y;
        Img_GrenadNum.fillAmount = amount;
    }
    #endregion

    #region 生命UI
    public Image Health_Head;
    public Image Health_Num;
    private static class HealthUIResources {
        private static List<Sprite> sprites = new List<Sprite>(Resources.LoadAll<Sprite>("HealthUI"));
        public static Sprite Head_Good { get => sprites.Find(p => p.name == "Head_Good"); }
        public static Sprite Head_OnHit { get => sprites.Find(p => p.name == "Head_OnHit"); }
        public static Sprite Head_Dead { get => sprites.Find(p => p.name == "Head_Dead"); }
        public static Sprite Num_Good { get => sprites.Find(p => p.name == "HealthUI_1"); }
        public static Sprite Num_Shit { get => sprites.Find(p => p.name == "HealthUI_2"); }
        public static Sprite Num_GG { get => sprites.Find(p => p.name == "HealthUI_3"); }
        public static Sprite Num_OnHit { get => sprites.Find(p => p.name == "HealthUI_4"); }
    }
    private void initHealthUI() {
        Health_Num = GameObject.Find("Img_HealthNum").GetComponent<Image>();
        Health_Head = GameObject.Find("HeadUI").GetComponent<Image>();
    }

    public void SetHealthNum(Vector2Int num) {

        float amount = (float)num.x / (float)num.y;
        Health_Num.fillAmount = amount;

        if (num.x == 0) {
            Health_Head.sprite = HealthUIResources.Head_Dead;
            return;
        };
        Health_Head.sprite = HealthUIResources.Head_OnHit;
        Health_Num.sprite = HealthUIResources.Num_OnHit;
        Timer.Register(0.5f, () => {
            Health_Head.sprite = HealthUIResources.Head_Good;
            if (amount > 0.66) Health_Num.sprite = HealthUIResources.Num_Good;
            else if (amount > 0.33) Health_Num.sprite = HealthUIResources.Num_Shit;
            else Health_Num.sprite = HealthUIResources.Num_GG;
        });
    }
    #endregion

    #region 关卡进度UI
    public GameObject ProgressBar;
    public Image Progress_Num;
    public GameObject Win;
    public GameObject GG;
    private void initProgressBar() {
        ProgressBar = GameObject.Find("ProgressBar");
        Progress_Num = GameObject.Find("ProgressNum").GetComponent<Image>();
        Win = GameObject.Find("Win_Img");
        GG = GameObject.Find("GG_Img");
        Win.SetActive(false);
        GG.SetActive(false);
    }
    public void SetProgressBar(bool isShow) {
        ProgressBar.SetActive(isShow);
    }
    public void SetProgressNum(float progress) {
        Progress_Num.fillAmount = progress;
    }
    public void SetWinUI(bool isShow) {
        Win.SetActive(isShow);
    }
    public void SetGG(bool isShow) {
        GG.SetActive(isShow);
    }
    #endregion
}
