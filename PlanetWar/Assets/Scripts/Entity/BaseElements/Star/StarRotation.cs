using UnityEngine;
using System.Collections;

/// <summary>
/// 控制行星的旋转，
/// 注意行星的父子节点关系
/// </summary>
public class StarRotation : MonoBehaviour {
    public GameObject m_Center;
    public float m_RotateSpeed;
    //private Quaternion zeroQuaternion = new Quaternion();

	void Update () {
        AutoRotate();
	}

    /// <summary>
    /// 自动旋转，为了同步父节点的运动，旋转实际上是父节点而非子节点
    /// </summary>
    private void AutoRotate()
    {
        if (m_Center)
        {
            var cacheRotation = this.transform.rotation;
            transform.RotateAround(m_Center.transform.position, Vector3.forward, m_RotateSpeed * Time.deltaTime);
            this.transform.rotation = cacheRotation;
        }
    }
}
