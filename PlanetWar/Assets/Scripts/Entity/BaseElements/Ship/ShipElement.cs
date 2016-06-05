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

[RequireComponent(typeof(ShipMover))]
public class ShipElement : MonoBehaviour
{
    public ShipMover shipMover;
    public ShipStateManager m_StateManager;                 //状态管理器
    //[HideInInspector]
    public int m_FromIndex = -1;                            //来源行星的索引
    //[HideInInspector]
    public int m_ToIndex = -1;                              //目的行星的索引
    //[HideInInspector]
    public int m_SurrondIndex = -1;                         //环绕行星的索引
 
    //[HideInInspector]
    public int m_MasterIndex = -1;                          //飞船所属主人索引

    public StarElement m_StarFrom { get; set; }
    public StarElement m_StarTo { get; set; }
    public StarElement m_SurrondStar { get; set; }

    public float m_BaseFlySpeed;                            //初始飞船速度
    public float m_FlySpeedScale = 1.0f;                    //飞行速度比例

	public float m_MaxFlySpeed                              //最大飞行速度
    {
        get { return m_BaseFlySpeed * m_FlySpeedScale; }
    }

	//public float m_SurrondSpeed                           //环绕速度
    //{
    //   get { return SharedGameData.shipSurrendSpeed; }
    //}
    //public int m_SurrondDirection = 1;                    //环绕旋转方向

    public bool m_CanMove = false;                          //是否出发

    // 到目标行星的单位移动向量
	public Vector3 Direction2TargetNormalized
	{
		get
		{
            var deltaPos = m_StarTo.TargetPosition - this.transform.position;
            //z轴不动
            deltaPos.z = 0;
            return deltaPos.normalized;
		}
	}

    /// <summary>
    /// 重置以回收
    /// </summary>
    public void _Reset()
    {
        m_FromIndex = -1;
        m_ToIndex = -1;
        m_SurrondIndex = -1;
        m_MasterIndex = -1;
        m_CanMove = false;
        m_StarFrom = null;
        m_StarTo = null;
        m_SurrondStar = null;
        this.m_StateManager = null;
        this.transform.up = Vector3.up;
    }

    /// <summary>
    /// 初始化
    /// 没有设置目标行星则不会运动
    /// </summary>
	public void _Init()
	{
        m_FromIndex = -1;
        m_ToIndex = -1;
        m_SurrondIndex = -1;
        m_MasterIndex = -1;
        m_CanMove = false;
        m_StarFrom = null;
        m_StarTo = null;
        m_SurrondStar = null;
        this.transform.up = Vector3.up;
        this.m_StateManager = new ShipStateManager(this);
    }

    /// <summary>
    /// 设置目的行星
    /// </summary>
    public void SetTarget(int fromStar,int toStar)
    {
        if (fromStar >= 0 && toStar >= 0)
        {
            this.m_FromIndex = fromStar;
            this.m_ToIndex = toStar;
            this.m_StarFrom = StarPoolManager.instance.GetStarByIndex(fromStar).GetComponent<StarElement>();
            this.m_StarTo = StarPoolManager.instance.GetStarByIndex(toStar).GetComponent<StarElement>();
        }
    }

    /// <summary>
    /// 更新
    /// </summary>
	public void _Update()
	{
        if (this.m_StateManager != null)
        {
            if (m_CanMove)
            {
                this.m_StateManager.UpdateState();
            }
        }
	}

    // <summary>
    // 环绕飞行
    // </summary>
	//public virtual void MoveSurrond()
	//{
	//	Vector3 crossV1 = m_SurrondStar.transform.position - this.transform.position;
	//	Vector3 crossV2 = new Vector3 (crossV1.x, crossV1.y + 1, crossV1.z); //this.transform.up;
	//	Vector3 crossResult = Vector3.Cross (crossV1, crossV2) * m_SurrondDirection;
	//	this.m_CurrentDirection = crossResult;
	//	this.transform.position = this.transform.position + this.m_CurrentDirection * Time.deltaTime * m_MaxFlySpeed;
	//}

    /// <summary>
    /// 进入目标行星
    /// </summary>
	protected void _EnterTarget(GameObject targetCollider)
	{
		this.m_StateManager.EnterTargetStar (targetCollider);
	}

    /// <summary>
    /// 进入其他行星
    /// </summary>
	protected void _EnterOther(GameObject otherCollider)
	{
		this.m_StateManager.EnterOtherStar (otherCollider);
	}

    /// <summary>
    /// 销毁
    /// </summary>
	public void _Destroy()
	{
        //Destroy (this.gameObject);
        EventData data = new EventData();
        data.intData1 = m_FromIndex;
        data.intData2 = m_ToIndex;
        data.intData3 = m_MasterIndex;
        data.objData1 = this.transform.position;
        GameEventDispatcher.instance.InvokeEvent(EventNameList.LEVEL_SHIP_BOOM_EVENT, data);

        _Reset();
        ShipPoolManager.instance.ReturnShip(this.gameObject);
    }

    public bool IfCollideTarget()
    {
        Vector2 deltaPos = new Vector2(this.transform.position.x - m_StarTo.transform.position.x, this.transform.position.y - m_StarTo.transform.position.y);
        if (deltaPos.sqrMagnitude <= m_StarTo.GetScaleByLevel())
        {
            return true;
        }
        return false;
    }
}