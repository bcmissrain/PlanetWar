using UnityEngine;
using System.Collections;

public class BackElementRotation : MonoBehaviour {
    public float rotateSpeed;
	void Start () {
	    
	}
	
	void Update () {
        this.transform.RotateAround(this.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
	}
}
