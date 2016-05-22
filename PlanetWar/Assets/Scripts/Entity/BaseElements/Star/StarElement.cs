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

// 行星基类
[RequireComponent(typeof(ShipSender))]
public class StarElement : MonoBehaviour
{
    public ShipSender shipSender;       //飞船生成&发射器

    public int m_Index;                 //行星索引，行星的所属可变，但是索引唯一
    public int m_MasterIndex;           //主人索引
    public MasterElement m_Master;      //主人
	public StarType m_StarType;         //种类（不可变）
	public StarLevel m_StarLevel;       //等级（可变）
    public int m_MaxTroop;              //产生兵力的最大值，超过不再生产
    public float m_BornTime;            //产生兵力的时间
    public float m_BornNum;             //一次产生兵力的数目
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
    public virtual void LevelUp() { }

    /// <summary>
    /// 派遣所有兵力到star
    /// </summary>
    /// <param name="starIndex"></param>
    public virtual void SendTroopToStar(int starIndex)
    {
        SendTroopToStar(starIndex, 1.0f);
    }

    /// <summary>
    /// 派遣percent比例的兵力到star
    /// </summary>
    /// <param name="starIndex"></param>
    /// <param name="percent"></param>
    public virtual void SendTroopToStar(int starIndex, float percent)
    {
        shipSender.SendTroopTo(starIndex, percent);
    }

    /// <summary>
    /// 自动产生兵力
    /// </summary>
    protected virtual void UpdateCreateTroop() { }

    /// <summary>
    /// 自动发送兵力
    /// </summary>
    protected virtual void UpdateSendTroop() { shipSender.UpdateSendTroop(); }

    /// <summary>
    /// 添加飞船
    /// </summary>
    public virtual void CreateTroop(){ }

    /// <summary>
    /// 添加数目为num的飞船
    /// </summary>
    public virtual void CreateTroopBy(int num)
    {
        shipSender.CreateTroopBy(num);
    }

    public virtual void DestroyTroopBy(int num)
    {
        shipSender.DestroyTroopBy(num);
    }

    /// <summary>
    /// 根据等级获取放大倍数
    /// </summary>
    /// <returns></returns>
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


    public virtual void OnShipDestroy(EventData eventData)
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
    public virtual void ChangeMasterTo(int masterIndex)
    {
        //Debug.Log(this.m_MasterIndex + " ChangeMasterTo " + masterIndex);
        if (masterIndex != this.m_MasterIndex)
        {
            //删除旧的
            var oldMaster = MasterPoolManager.instance.GetMasterByIndex(m_MasterIndex);
            if (oldMaster)
            {
                var oldStarList = oldMaster.m_StarList;
                for (int i = 0; i < oldStarList.Count; i++)
                {
                    if (oldStarList[i] == this)
                    {
                        oldStarList.RemoveAt(i);
                        break;
                    }
                }
            }

            //添加新的
            var newMaster = MasterPoolManager.instance.GetMasterByIndex(masterIndex);
            if (newMaster)
            {
                newMaster.m_StarList.Add(this);
            }

            //更改索引
            this.m_MasterIndex = masterIndex;

            //播放动画
            //更改UI
        }
    }
}