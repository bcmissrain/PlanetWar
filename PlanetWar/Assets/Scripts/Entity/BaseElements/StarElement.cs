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
    /// 攻击enemyStar
    /// </summary>
    /// <param name="enemyStar">敌方行星</param>
    public virtual void AttackStar(StarElement enemyStar) { }

    /// <summary>
    /// 派遣attackNum的兵力攻击enemyStar
    /// </summary>
    /// <param name="enemyStar"></param>
    /// <param name="attackNum"></param>
    public virtual void AttackStar(StarElement enemyStar, float percent) { }

    /// <summary>
    /// 全力支援friendStar
    /// </summary>
    /// <param name="friendStar">友星</param>
    public virtual void SupportStar(StarElement friendStar) { }

    /// <summary>
    /// 支援friendStar以supportNum的兵力
    /// </summary>
    /// <param name="friendStar">友星</param>
    /// <param name="supportNum">支援兵力</param>
    public virtual void SupportStar(StarElement friendStar, float percent) { }

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
    public virtual void CreateTroopTo(int num)
    {
        shipSender.CreateTroopTo(num);
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
}