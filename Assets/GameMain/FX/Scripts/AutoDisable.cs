using UnityEngine;

public class AutoDisable : MonoBehaviour {
    public void OnFinish() {
        gameObject.SetActive(false);
    }
}
