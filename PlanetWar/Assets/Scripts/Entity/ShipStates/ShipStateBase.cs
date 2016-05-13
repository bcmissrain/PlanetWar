using UnityEngine;

/// <summary>
/// 飞船状态基类
/// </summary>
public abstract class ShipStateBase
{
    public static readonly float MIN_RADIAN = 0.98f;        //最小旋转差值 10度
    protected ShipStateManager m_StateManager;                  //状态管理器
    public ShipElement m_Ship;                              //飞船对象
    public abstract ShipState m_ShipState { get; }          //状态

    public ShipStateBase(ShipStateManager stateManager, ShipElement ship)
    {
        this.m_StateManager = stateManager;
        this.m_Ship = ship;
    }

    /// <summary>
    /// 更新状态
    /// </summary>
    public virtual void UpdateState() { }

    /// <summary>
    /// 进入目标行星
    /// </summary>
    /// <param name="collider"></param>
    public virtual void EnterTargetStar(Collider collider) { } //Trigger By Manager Earlier Than UpdateState

    /// <summary>
    /// 进入其他行星
    /// </summary>
    /// <param name="collider"></param>
    public virtual void EnterOtherStar(Collider collider) { } //Trigger By Manager Earlier Than UpdateState

    /// <summary>
    //  是否可以恢复路线
    /// </summary>
    /// <param name="dir0"></param>
    /// <param name="dir1"></param>
    protected bool CanResumeMove(Vector3 dir0, Vector3 dir1)
    {
        if (Vector3.Dot(dir0.normalized, dir1.normalized) > MIN_RADIAN)
            return true;

        return false;
    }
}