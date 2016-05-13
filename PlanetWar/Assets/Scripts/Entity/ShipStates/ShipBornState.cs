/// <summary>
/// 飞船产生
/// =》飞行
/// </summary>
public class ShipBornState : ShipStateBase
{
    public ShipBornState(ShipStateManager stateManager, ShipElement ship)
        : base(stateManager, ship) { }

    public override ShipState m_ShipState
    {
        get
        {
            return ShipState.Born;
        }
    }

    public override void UpdateState()
    {
        m_StateManager.ChangeStateTo(ShipState.Fly);
    }
}