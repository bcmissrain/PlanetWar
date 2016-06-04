using UnityEngine;
using System.Collections;

//行星类型
public enum StarType
{
    TroopStar = 0,  //普通行星
    DefenceStar,    //防卫行星
    MasterStar,     //要塞行星
    DoorStar,       //传送行星
}

//行星等级
public enum StarLevel
{
    Level0 = 0,
    Level1,
    Level2,
}

//行星状态
public enum StarStatus
{
    None = 0,
    SelectMain,
    SelectSupport,
    SelectAttack,
}

//产生飞船的展现方式
public enum ShipShowType
{
    Cloud = 0,          //云状
    Ring                //环状
}

/// <summary>
/// 行星基类
/// </summary>
[RequireComponent(typeof(ShipSender))]
public class StarElement : MonoBehaviour
{
    public ShipSender shipSender;       //飞船生成&发射器
    public StarUpdater starUpdater;     //行星逻辑更新器

    public GameObject m_StarSon;        //飞船旋绕子节点 （不显示）

    public int m_Index;                 //行星索引，行星的所属可变，但是索引唯一
    public int m_MasterIndex;           //主人索引
	public StarType m_StarType;         //种类（不可变）
	public StarLevel m_StarLevel;       //等级（可变）
    public int m_MaxTroop;              //产生兵力的最大值，超过不再生产
    public int m_StartTroopNum;         //初始化的起始兵力
    public float m_BornTime;            //产生兵力的时间
    public int m_BornNum;               //一次产生兵力的数目
    public float m_DetectScope;         //监测范围
    public ShipShowType m_ShipShowType; //飞船展现方式

    public int m_TroopNum               //当前兵力数目
    {
        get
        {
            return shipSender.shipList.Count;
        }
    }

    public float m_TroopPercent         //兵力饱和度
    {
        get
        {
            if (m_MaxTroop != 0)
            {
                return (float)m_TroopNum / (float)m_MaxTroop;
            }
            else
            {
                return 1;
            }
        }
    }

    /// <summary>
    /// 升级
    /// </summary>
    public void LevelUp() { }

    /// <summary>
    /// 是否已经生产力满载
    /// </summary>
    public bool IfFullShip()
    {
        return m_TroopNum >= m_MaxTroop;
    }

    /// <summary>
    /// 获取行星的Master对象
    /// </summary>
    public MasterElement GetMasterElement()
    {
        return MasterPoolManager.instance.GetMasterByIndex(this.m_MasterIndex);
    }

    /// <summary>
    /// 派遣所有兵力到star
    /// </summary>
    public void SendTroopToStar(int starIndex)
    {
        SendTroopToStar(starIndex, 1.0f);
    }

    /// <summary>
    /// 派遣percent比例的兵力到star
    /// </summary>
    public void SendTroopToStar(int starIndex, float percent)
    {
        shipSender.SendTroopTo(starIndex, percent);
    }

    /// <summary>
    /// 添加一批飞船
    /// </summary>
    public void CreateTroopOnce()
    {
        int bornNum = m_BornNum;
        for (int i = 0; i < bornNum; i++)
        {
            if (!IfFullShip())
            {
                shipSender.CreateTroop(m_MaxTroop, shipSender.shipList.Count, m_ShipShowType, 0.0f, GetScaleByLevel());
            }
        }
    }

    /// <summary>
    /// 添加数目为num的飞船
    /// </summary>
    public void CreateTroopBy(int num)
    {
        shipSender.CreateTroopBy(num);
    }

    /// <summary>
    /// 删除数目为num的飞船
    /// </summary>
    public virtual void DestroyTroopBy(int num)
    {
        shipSender.DestroyTroopBy(num);
    }

    /// <summary>
    /// 根据等级获取放大倍数
    /// </summary>
    public virtual float GetScaleByLevel()
    {
        if (m_StarLevel == StarLevel.Level0)
        {
            return 1.0f;
        }
        else if (m_StarLevel == StarLevel.Level1)
        {
            return 1.3f;
        }
        else if (m_StarLevel == StarLevel.Level2)
        {
            return 1.5f;
        }

        return 1.0f;
    }

    /// <summary>
    /// 处理飞船销毁
    /// </summary>
    public void OnShipDestroy(EventData eventData)
    {
        //这个行星是目标行星
        if (this.m_Index == eventData.intData2)
        {
            //增援
            if (this.m_MasterIndex == eventData.intData3)
            {
                this.CreateTroopBy(1);
            }
            //被攻击
            else
            {
                if (m_TroopNum > 0)
                {
                    this.DestroyTroopBy(1);
                }
                //如果现在兵力为0，则被占领
                else
                {
                    ChangeMasterTo(eventData.intData3);
                    this.CreateTroopBy(1);
                }
            }
        }
    }

    /// <summary>
    /// 更改主人
    /// </summary>
    public void ChangeMasterTo(int masterIndex)
    {
        if (masterIndex != this.m_MasterIndex)
        {
            //删除旧的
            var oldMaster = MasterPoolManager.instance.GetMasterByIndex(m_MasterIndex);
            if (oldMaster)
            {
                oldMaster.RemoveStarElement(this);
            }

            //添加新的
            var newMaster = MasterPoolManager.instance.GetMasterByIndex(masterIndex);
            if (newMaster)
            {
                newMaster.AddStarElement(this);
            }

            //更改索引
            this.m_MasterIndex = masterIndex;

            //播放动画
            //更改UI

            //更新逻辑计算
            this.starUpdater.UpdateSafeDetect();
            this.starUpdater.UpdateStarLogic();            
        }
    }

    /// <summary>
    /// 飞船的目标点，始终围绕飞船旋转
    /// </summary>
    public Vector3 TargetPosition
    {
        get
        {
            if (m_StarSon)
            {
                return m_StarSon.transform.position;
            }
            else
            {
                return this.transform.position;
            }
        }
    }
}