using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipElement))]
public class ShipMover : MonoBehaviour {
    public ShipElement shipElement;         //飞船元素
    public Vector3 m_Force;                 //飞船受力方向
    //public float m_CurrentSpeed;            //飞船当前速度
    public Vector3 m_GroupDirection;        //飞船组分配的飞行方向

    public Vector3 m_CurrentDirection       // 当前方向
    {
        get
        {
            return this.transform.forward;
        }

        set
        {
            this.transform.forward = value.normalized;
        }
    }

    /// <summary>
    /// 向目标飞行
    /// </summary>
    public void MoveToTarget()
    {
        this.transform.up = shipElement.Direction2TargetNormalized;
        this.transform.position = this.transform.position + this.transform.up * Time.deltaTime * shipElement.m_MaxFlySpeed;
    }
}
