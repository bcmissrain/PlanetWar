using UnityEngine;
using System.Collections;

public class TryEllipseMove : MonoBehaviour {
    public float speed;
    public Vector3 ellipseCenter;
    public float ellipseRadiusX;
    public float ellipseRadiusY;

	void Start () {
	    
	}
	
	void Update () {
        float t = speed * Time.timeSinceLevelLoad % 1;
        float ellipseAngle = Mathf.PI * 2 * t;
        float x = ellipseCenter.x + ellipseRadiusX * Mathf.Cos(ellipseAngle);
        float y = ellipseCenter.y + ellipseRadiusY * Mathf.Sin(ellipseAngle);
        Vector3 pos = new Vector3(x, y, 0);
        transform.position = pos;

        float dxdt = -ellipseRadiusX * Mathf.Sin(ellipseAngle);
        float dydt = ellipseRadiusY * Mathf.Cos(ellipseAngle);

        float rot = Mathf.Atan2(dydt, dxdt) - Mathf.PI / 2.0f;

        Vector3 newRotation = new Vector3(0,0,rot * Mathf.Rad2Deg);
        //先重置 再旋转
        transform.rotation = new Quaternion();
        transform.Rotate(newRotation);
        
        //Quaternion newQua = new Quaternion();
        //newQua.SetEulerAngles(newRotation);
        //transform.rotation = newQua;
        //transform.eulerAngles = newRotation;
        //transform.rotation.SetEulerAngles(newRotation);
    }
}