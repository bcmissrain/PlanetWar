using UnityEngine;
using System.Collections;

public class CenterStarRotate : MonoBehaviour {
    public GameObject centerStar;
    public float rotateSpeed;

	void Start () {
	
	}
	
	void Update () {
        centerStar.transform.RotateAround(Vector3.zero, Vector3.forward, Time.deltaTime * rotateSpeed);
        centerStar.transform.rotation = new Quaternion();
	}
}
