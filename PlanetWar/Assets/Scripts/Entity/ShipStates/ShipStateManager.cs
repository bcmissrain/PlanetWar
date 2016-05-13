using UnityEngine;

/// <summary>
/// 飞船状态管理器
/// </summary>
public class ShipStateManager
{
    private ShipStateBase m_ShipState;        //飞船当前状态
    public ShipState m_ShipStateDesc
    {
        get
        {
            return m_ShipState.m_ShipState;
        }
    }       //飞船当前状态描述

    private ShipStateManager() { }

    public ShipStateManager(ShipElement ship)
    {
        m_ShipState = new ShipBornState(this, ship);
    }

    /// <summary>
    /// 更新状态
    /// </summary>
    public void UpdateState()
    {
        m_ShipState.UpdateState();
    }

    /// <summary>
    /// 处理进入目标行星
    /// </summary>
    public void EnterTargetStar(Collider collider)
    {
        m_ShipState.EnterTargetStar(collider);
    }

    /// <summary>
    /// 处理进入其他行星
    /// </summary>
    public void EnterOtherStar(Collider collider)
    {
        m_ShipState.EnterOtherStar(collider);
    }

    /// <summary>
    /// 状态转换
    /// </summary>
    /// <param name="shipState">改变的状态枚举</param>
    public void ChangeStateTo(ShipState shipState)
    {
        switch (shipState)
        {
            case ShipState.Born:
                m_ShipState = new ShipBornState(this, m_ShipState.m_Ship);
                break;
            case ShipState.Fly:
                m_ShipState = new ShipFlyState(this, m_ShipState.m_Ship);
                break;
            case ShipState.Surrond:
                m_ShipState = new ShipSurrondState(this, m_ShipState.m_Ship);
                break;
            case ShipState.Land:
                m_ShipState = new ShipLandState(this, m_ShipState.m_Ship);
                break;
            case ShipState.Hide:
                m_ShipState = new ShipHideState(this, m_ShipState.m_Ship);
                break;
        }
    }
}