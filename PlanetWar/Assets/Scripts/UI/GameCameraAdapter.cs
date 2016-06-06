using UnityEngine;
using System.Collections;

public class GameCameraAdapter : MonoBehaviour {
    public Camera mainCamera;
    public float cameraScale = 1f;
    public float iPhoneAspect = 9.0f / 16.0f;
    public float iPadAspect = 3.0f / 4.0f;

    void Start () {
        cameraScale = mainCamera.orthographicSize;
        float deviceAspect = (float)Screen.height / (float)Screen.width;
        float adapteScale = deviceAspect / iPhoneAspect * mainCamera.orthographicSize;
        mainCamera.orthographicSize = adapteScale;
	}
}
