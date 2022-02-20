using QuickCode;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioResCtrl ResCtrl;
    public AudioSource BGMSource;
    public AudioSource SFXSource;
    public ComponentPool<AudioSource> ASPool;

    void Start() {
        ResCtrl = new AudioResCtrl();
        ResCtrl.LoadAllClip();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        BGMSource = audioSources[0];
        SFXSource = audioSources[1];
        ASPool = new ComponentPool<AudioSource>();
        ASPool.InitPool(GameObject.Find("AudioSources"), 2);
        PlayBGM(AudioResCtrl.BGM);
    }

    private bool isMute;
    public bool IsMute {
        set {
            isMute = value;
            if (isMute)
                mute();
            else
                unmute();
        }
        get { return isMute; }
    }
    private void mute() {
        BGMSource.mute = true;
        SFXSource.mute = true;
        isMute = true;
    }
    private void unmute() {
        BGMSource.mute = false;
        SFXSource.mute = false;
        isMute = false;
    }
    public void PlayBGM(AudioClip clip) {
        if (BGMSource.isPlaying)
            BGMSource.Stop();
        BGMSource.clip = clip;
        BGMSource.loop = true;
        BGMSource.Play();
    }
    public void PauseBGM(bool isPaues) {
        if (isPaues) BGMSource.Pause();
        else BGMSource.UnPause();
    }

    public AudioSource PlaySFXLoop(AudioClip clip) {
        AudioSource source = ASPool.GetObject(true);
        source.clip = clip;
        source.loop = true;
        source.enabled = true;
        source.Play();
        return source;
    }
    public void StopSFXLoop(AudioSource source) {
        source.loop = false;
        source.Stop();
        ASPool.Recycle(source);
    }
    public void PlaySFX(AudioClip clip) {
        SFXSource.PlayOneShot(clip);
    }
    public void PlaySFX(AudioClip[] clips) {
        for (int i = 0; i < clips.Length; i++) {
            PlaySFX(clips[i]);
        }
    }
}
