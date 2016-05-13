using UnityEngine;

//飞船状态
public enum ShipState
{
    Born,
    Fly,
    Surrond,
    Land,
    Hide,
}

public class ShipElement : MonoBehaviour
{
    public ShipStateManager m_StateManager;                 //状态管理器

    public int m_FromIndex;                                 //来源行星的索引
    public int m_ToIndex;                                   //目的行星的索引
    public int m_SurrondIndex;                              //环绕行星的索引

    public StarElement m_StarFrom { get; set; }
    public StarElement m_StarTo { get; set; }
    public StarElement m_SurrondStar { get; set; }

	public int m_ShipTroopNum;                              //飞船携带兵力
	public float m_FlySpeed;                                //飞行速度
	public float m_SurrondSpeed;                            //环绕速度
    public int m_SurrondDirection = 1;                      //环绕旋转方向

    /// <summary>
    /// 当前方向
    /// </summary>
	public Vector3 m_CurrentDirection
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
    /// 到目标行星的单位移动向量
    /// </summary>
	protected Vector3 Direction2TargetNormalized
	{
		get
		{
			return (m_StarTo.transform.position - this.transform.position).normalized;
		}
	}

    /// <summary>
    /// 初始化
    /// </summary>
	public virtual void _Init()
	{
		this.m_StateManager = new ShipStateManager (this);
	}

    /// <summary>
    /// 更新
    /// </summary>
	protected virtual void _Update()
	{
		this.m_StateManager.UpdateState ();
	}

    /// <summary>
    /// 向目标飞行
    /// </summary>
	public virtual void MoveToTarget()
	{
		this.m_CurrentDirection = Direction2TargetNormalized;
		this.transform.position = this.transform.position + Direction2TargetNormalized * Time.deltaTime * m_FlySpeed;
	}

    /// <summary>
    /// 环绕飞行
    /// </summary>
	public virtual void MoveSurrond()
	{
		Vector3 crossV1 = m_SurrondStar.transform.position - this.transform.position;
		Vector3 crossV2 = new Vector3 (crossV1.x, crossV1.y + 1, crossV1.z); //this.transform.up;
		Vector3 crossResult = Vector3.Cross (crossV1, crossV2) * m_SurrondDirection;
		this.m_CurrentDirection = crossResult;
		this.transform.position = this.transform.position + this.m_CurrentDirection * Time.deltaTime * m_FlySpeed;
	}

    /// <summary>
    /// 进入目标行星
    /// </summary>
	protected virtual void _EnterTarget(Collider targetCollider)
	{
		this.m_StateManager.EnterTargetStar (targetCollider);
	}

    /// <summary>
    /// 进入其他行星
    /// </summary>
	protected virtual void _EnterOther(Collider otherCollider)
	{
		this.m_StateManager.EnterOtherStar (otherCollider);
	}

    /// <summary>
    /// 销毁
    /// </summary>
	public virtual void _Destroy()
	{
		Destroy (this.gameObject);
	}
}