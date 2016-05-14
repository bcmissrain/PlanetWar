using UnityEngine;
using System.Collections;

public class TestAutoRotate : MonoBehaviour
{
    public Transform center;
    public float rotateSpeed;

    void Update()
    {
        if (center)
        {
            var cacheRotation = this.transform.rotation;
            transform.RotateAround(center.position, Vector3.forward, rotateSpeed * Time.deltaTime);
            this.transform.rotation = cacheRotation;
        }
    }
}