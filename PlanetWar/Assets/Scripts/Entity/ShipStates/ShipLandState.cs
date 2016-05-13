using UnityEngine;

public class ShipLandState : ShipStateBase
{
    public ShipLandState(ShipStateManager stateManager, ShipElement ship)
        : base(stateManager, ship) { }

    public override ShipState m_ShipState
    {
        get
        {
            return ShipState.Land;
        }
    }

    public override void UpdateState()
    {
        m_StateManager.ChangeStateTo(ShipState.Hide);
    }
}