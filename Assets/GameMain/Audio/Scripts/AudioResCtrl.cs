using QuickCode;
using System.Collections.Generic;
using UnityEngine;

public class AudioResCtrl {
    public static List<string> AudioPaths = new List<string> {
        "BGM",
        "Enemy_Dead_1",
        "Enemy_Dead_2",
        "Enemy_Dead_3",
        "Footstep_1",
        "Footstep_2",
        "Footstep_3",
        "Footstep_4",
        "Footstep_5",
        "Footstep_6",
        "Grenade_Explosion_1",
        "Grenade_Explosion_2",
        "Gun_Bullet_Hit_1",
        "Gun_Bullet_Hit_2",
        "Gun_Bullet_Hit_3",
        "Gun_Bullet_Hit_4",
        "Gun_MainFire_1",
        "Gun_MainFire_2",
        "Gun_MainFire_3",
        "Gun_MainFire_4",
        "Gun_MainFire_5",
        "Gun_MainFire_6",
        "Gun_NoMagazine_1",
        "Gun_NoMagazine_2",
        "Gun_NoMagazine_3",
        "Gun_NoMagazine_4",
        "Gun_NoMagazine_5",
        "Gun_NoMagazine_6",
        "Gun_Reload",
        "Gun_SubFire_1",
        "Gun_SubFire_2",
        "Gun_SubFire_3",
        "Gun_SubFire_4",
        "Player_Climb",
        "Player_Dash",
        "Player_Jump_1",
        "Player_Jump_2",
        "Shell"
    };
    public static Dictionary<string, AudioClip> AudioDict;

    public void LoadAllClip() {
        AudioDict = new Dictionary<string, AudioClip>();
        foreach (string path in AudioPaths) {
            AudioClip clip = Resources.Load<AudioClip>(path);
            AudioDict.Add(path, clip);
        }
    }
    public static AudioClip BGM { get => AudioDict["BGM"]; }
    #region 实体相关
    public static AudioClip FootstepClip {
        get {
            int index = MathTool.RandomEx.GetInt(1, 7);
            string key = "Footstep_" + index.ToString();
            return AudioDict[key];
        }
    }
    public static AudioClip EnemyOnHitClip {
        get {
            int index = MathTool.RandomEx.GetInt(1, 3);
            string key = "Enemy_Dead_" + index.ToString();
            return AudioDict[key];
        }
    }
    public static AudioClip PlayerClimbClip {
        get => AudioDict["Player_Climb"];
    }
    public static AudioClip PlayerDashClip {
        get => AudioDict["Player_Dash"];
    }
    public static AudioClip PlayerJumpClip {
        get {
            int index = MathTool.RandomEx.GetInt(1, 3);
            string key = "Player_Jump_" + index.ToString();
            return AudioDict[key];
        }
    }
    #endregion

    #region 装备相关
    public static AudioClip[] GunFireClip {
        get {
            AudioClip[] output = new AudioClip[2];
            int index = MathTool.RandomEx.GetInt(1, 7);
            string key = "Gun_MainFire_" + index.ToString();
            output[0] = AudioDict[key];
            index = MathTool.RandomEx.GetInt(1, 5);
            key = "Gun_SubFire_" + index.ToString();
            output[1] = AudioDict[key];
            return output;
        }
    }
    public static AudioClip NoMagazineClip {
        get {
            int index = MathTool.RandomEx.GetInt(1, 7);
            string key = "Gun_NoMagazine_" + index.ToString();
            return AudioDict[key];
        }
    }
    public static AudioClip ReloadClip {
        get => AudioDict["Gun_Reload"];
    }
    public static AudioClip BulletHitClip {
        get {
            int index = MathTool.RandomEx.GetInt(1, 5);
            string key = "Gun_Bullet_Hit_" + index.ToString();
            return AudioDict[key];
        }
    }
    public static AudioClip ExplosionClip {
        get {
            int index = MathTool.RandomEx.GetInt(1, 3);
            string key = "Grenade_Explosion_" + index.ToString();
            return AudioDict[key];
        }
    }

    public static AudioClip ShellClip {
        get => AudioDict["Shell"];
    }
    #endregion
}
