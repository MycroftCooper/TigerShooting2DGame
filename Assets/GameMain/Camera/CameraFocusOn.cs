using UnityEngine;

public class CameraFocusOn : MonoBehaviour {
    public GameObject Player;
    public float PlayerWeights;
    public float DoZoomDistance;
    public float MaxZoom = 8f;

    private CameraCtrl cameraCtrl;
    void Start() {
        Player = GameObject.Find("Player");
        cameraCtrl = GameEntry.Instance._CameraCtrl;
    }

    void Update() {
        Vector3 screenMousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(screenMousePos);
        Vector2 playerPos = Player.transform.position;
        Vector2 targetPos = Vector2.Lerp(mousePos, playerPos, PlayerWeights);
        transform.position = targetPos;

        float d = Vector2.Distance(mousePos, playerPos);
        if (cameraCtrl.CurZoomSize < MaxZoom && d > DoZoomDistance) {
            cameraCtrl.CurZoomSize += 0.01f;
            return;
        }
        if (d < DoZoomDistance && cameraCtrl.CurZoomSize > 4) {
            cameraCtrl.CurZoomSize -= 0.02f;
        }
    }

    static public bool IsAPointInACamera(Camera cam, Vector3 wordPos) {
        // 是否在视野内
        bool result1 = false;
        Vector3 posViewport = cam.WorldToViewportPoint(wordPos);
        Debug.Log("posViewport:" + posViewport.ToString());
        Rect rect = new Rect(0, 0, 1, 1);
        result1 = rect.Contains(posViewport);
        Debug.Log("result1:" + result1.ToString());
        // 是否在远近平面内
        bool result2 = false;
        if (posViewport.z >= cam.nearClipPlane && posViewport.z <= cam.farClipPlane) {
            result2 = true;
        }
        Debug.Log("result2:" + result2.ToString());
        // 综合判断
        bool result = result1 && result2;
        Debug.Log("result:" + result.ToString());
        return result;
    }
}
