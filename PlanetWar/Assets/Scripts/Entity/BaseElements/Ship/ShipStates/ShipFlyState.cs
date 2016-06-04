using UnityEngine;

/// <summary>
/// 飞船飞行
/// =》着陆
/// =》绕行
/// =》隐藏
/// </summary>
public class ShipFlyState : ShipStateBase
{
    public ShipFlyState(ShipStateManager stateManager, ShipElement ship)
        : base(stateManager, ship) { }

    public override ShipState m_ShipState
    {
        get
        {
            return ShipState.Fly;
        }
    }

    public override void UpdateState()
    {
        if (m_Ship.m_ToIndex != -1)
        {
            m_Ship.shipMover.MoveToTarget();

            if (m_Ship.IfCollideTarget())
            {
                EnterTargetStar(m_Ship.m_StarTo.gameObject);
            }
        }
    }

    public override void EnterTargetStar(GameObject collider)
    {
        //着陆
        m_StateManager.ChangeStateTo(ShipState.Land);
    }

    //public override void EnterOtherStar(GameObject collider)
    //{
    //    StarElement otherStar = collider.gameObject.GetComponent<StarElement>();
    //    SetShipSurrondDirection(otherStar);
    //    if (CanBeginSurrond(m_Ship, otherStar)) //Change To Surrond
    //    {
    //        m_Ship.m_SurrondStar = collider.gameObject.GetComponent<StarElement>();
    //        m_Ship.transform.parent = otherStar.transform;
    //        m_StateManager.ChangeStateTo(ShipState.Surrond);
    //    }
    //}

    /// <summary>
    /// 设置飞船环绕旋转
    /// </summary>
    //private void SetShipSurrondDirection(StarElement otherStar)
    //{
    //    Vector3 crossV1 = otherStar.transform.position - m_Ship.transform.position;
    //    Vector3 crossV2 = new Vector3(crossV1.x, crossV1.y + 1, crossV1.z);//ship.transform.up;
    //    Vector3 crossResult = Vector3.Cross(crossV1, crossV2) * m_Ship.m_SurrondSpeed;
    //    Vector3 ship2Target = m_Ship.m_StarTo.transform.position - this.m_Ship.transform.position;
    //    float distance1 = (ship2Target - crossResult * 1).sqrMagnitude;
    //    float distance2 = (ship2Target - crossResult * (-1)).sqrMagnitude;

    //    if (distance1 < distance2)
    //    {
    //        m_Ship.m_SurrondDirection = 1;
    //    }
    //    else
    //    {
    //        m_Ship.m_SurrondDirection = -1;
    //    }
    //}

    /// <summary>
    /// 是否可以开始旋绕
    /// </summary>
    //private bool CanBeginSurrond(ShipElement ship, StarElement otherStar)
    //{
    //    Vector3 crossV1 = otherStar.transform.position - ship.transform.position;
    //    Vector3 crossV2 = new Vector3(crossV1.x, crossV1.y + 1, crossV1.z);//ship.transform.up;
    //    Vector3 crossResult = Vector3.Cross(crossV1, crossV2) * m_Ship.m_SurrondDirection;
    //    Vector3 plane2TargetDirection = m_Ship.m_StarTo.transform.position - m_Ship.transform.position;
    //    return !CanResumeMove(plane2TargetDirection, crossResult);
    //}
}
