using UnityEngine;
using System.Collections;

public class TestBackRotate : MonoBehaviour {
    public float rotateDegree = 0;

	void Start () {
	
	}
	
	void Update () {
        this.transform.RotateAround(this.transform.position, Vector3.up, rotateDegree * Time.deltaTime);
	}
}
