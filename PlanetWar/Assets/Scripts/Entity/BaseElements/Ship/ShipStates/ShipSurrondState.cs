using UnityEngine;

/// <summary>
/// 飞船环绕状态
/// =》飞行
/// </summary>
public class ShipSurrondState : ShipStateBase
{
    public ShipSurrondState(ShipStateManager stateManager, ShipElement ship)
        : base(stateManager, ship) { }

    public override ShipState m_ShipState
    {
        get
        {
            return ShipState.Surrond;
        }
    }

    public override void UpdateState()
    {
        //m_Ship.MoveSurrond();

        //Vector3 plane2TargetDirection = m_Ship.m_StarTo.transform.position - m_Ship.transform.position;
        //Vector3 planeDirection = m_Ship.m_CurrentDirection;
        //if (CanResumeMove(plane2TargetDirection, planeDirection))
        //{
        //    m_Ship.m_SurrondStar = null;
        //    m_StateManager.ChangeStateTo(ShipState.Fly);
        //    //默认无旋绕父节点
        //    m_Ship.transform.parent = null;
        //    m_StateManager.UpdateState();
        //}
    }
}