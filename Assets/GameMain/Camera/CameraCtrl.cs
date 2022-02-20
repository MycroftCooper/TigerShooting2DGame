using Cinemachine;
using Cinemachine.PostFX;
using DG.Tweening;
using QuickCode;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraCtrl : MonoBehaviour {
    private CinemachineCollisionImpulseSource inpulse;
    private CinemachineVirtualCamera virtualCamera;
    private CinemachinePostProcessing postFX;

    public PostProcessProfile ShakePF;
    private void Start() {
        inpulse = GetComponent<CinemachineCollisionImpulseSource>();
        virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        postFX = GetComponentInChildren<CinemachinePostProcessing>();
        CurZoomSize = virtualCamera.m_Lens.OrthographicSize;
        ShakePF = Resources.Load<PostProcessProfile>("ShakePost");
        postFX.m_Profile = null;
        virtualCamera.m_Lens.OrthographicSize = 4;
    }

    private void Update() {
        virtualCamera.m_Lens.OrthographicSize = CurZoomSize;
    }

    public void Shake(Vector3 force, float time = 1f, bool reset = true) {
        inpulse.m_ScaleImpactWithSpeed = true;
        postFX.m_Profile = ShakePF;
        inpulse.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = time;
        inpulse.GenerateImpulse(force);
        if (reset) Timer.Register(time, () => postFX.m_Profile = null);
    }
    public float CurZoomSize;
    public void Zoom(float size, float inTime) {
        DOTween.To(() => CurZoomSize, x => CurZoomSize = x, size, inTime);
    }
    public void SetGameTime(float timeScale, float holdTime) {
        Time.timeScale = timeScale;
        Timer.Register(holdTime, () => Time.timeScale = 1, useRealTime: true);
    }

    public void OnPlayerOnHit() {
        ShakePF.GetSetting<MotionBlur>().active = true;
        ShakePF.GetSetting<Vignette>().active = true;
        ShakePF.GetSetting<ChromaticAberration>().active = true;
        ShakePF.GetSetting<LensDistortion>().active = true;

        Shake(new Vector3(0.8f, 0.8f, 0.8f), 0.3f);
        SetGameTime(0.1f, 0.2f);
    }
    public void OnEnemyDead() {
        ShakePF.GetSetting<MotionBlur>().active = true;
        ShakePF.GetSetting<Vignette>().active = false;
        ShakePF.GetSetting<ChromaticAberration>().active = true;
        ShakePF.GetSetting<LensDistortion>().active = true;
        Shake(new Vector3(0f, 0f, 0.5f), 0.3f);
        SetGameTime(0.2f, 0.8f);
    }
    public void OnPlayerDash() {
        ShakePF.GetSetting<MotionBlur>().active = true;
        ShakePF.GetSetting<Vignette>().active = false;
        ShakePF.GetSetting<ChromaticAberration>().active = true;
        ShakePF.GetSetting<LensDistortion>().active = false;

        Shake(new Vector3(0f, 0f, 0.5f), 0.5f);
    }
    public void OnPlayerDead() {
        ShakePF.GetSetting<MotionBlur>().active = true;
        ShakePF.GetSetting<Vignette>().active = true;
        ShakePF.GetSetting<ChromaticAberration>().active = true;
        ShakePF.GetSetting<LensDistortion>().active = true;
        SetGameTime(0.1f, 3f);
        Shake(Vector3.one, 1.5f, false);
        Zoom(3.5f, 3f);
    }
    public void OnPlayerShot(int num) {
        ShakePF.GetSetting<MotionBlur>().active = true;
        ShakePF.GetSetting<Vignette>().active = false;
        ShakePF.GetSetting<ChromaticAberration>().active = false;
        ShakePF.GetSetting<LensDistortion>().active = false;

        if (num == 1) {
            Shake(new Vector3(-0.8f, 0f, 0f), 0.1f);
        } else {
            Shake(new Vector3(-1.6f, 0f, 0f), 0.1f);
        }
    }
    public void OnExploded() {
        ShakePF.GetSetting<MotionBlur>().active = true;
        ShakePF.GetSetting<Vignette>().active = false;
        ShakePF.GetSetting<ChromaticAberration>().active = true;
        ShakePF.GetSetting<LensDistortion>().active = true;
        Shake(Vector3.one * 1.5f, 0.8f);
    }
}
