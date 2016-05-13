using UnityEngine;

/// <summary>
/// 飞船销毁状态
/// </summary>
public class ShipHideState : ShipStateBase
{
    public ShipHideState(ShipStateManager stateManager, ShipElement ship) : base(stateManager, ship) { }

    public override ShipState m_ShipState
    {
        get
        {
            return ShipState.Hide;
        }
    }

    public override void UpdateState()
    {
        //销毁
        m_Ship._Destroy();
    }
}