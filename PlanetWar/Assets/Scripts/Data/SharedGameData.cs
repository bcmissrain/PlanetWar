using UnityEngine;
using System.Collections;

public class SharedGameData : MonoBehaviour {
    public static float shipFlySpeed;
    public static float shipSurrendSpeed;

    public float param_shipFlySpeed = 1.0f;
    public float param_shipSurrendSpeed = 0;

    void Start()
    {
        shipFlySpeed = param_shipFlySpeed;
        shipSurrendSpeed = param_shipSurrendSpeed;
    }
}
