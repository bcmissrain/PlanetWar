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

// 行星基类
public class StarElement : MonoBehaviour
{
    public int m_Index;                 //行星索引，行星的所属可变，但是索引唯一
	public MasterElement m_Master;      //主人
	public StarType m_StarType;         //种类（不可变）
	public StarLevel m_StarLevel;       //等级（可变）
	public int m_MaxTroop;              //产生兵力的最大值，超过不再生产
	public int m_TroopNum;              //当前兵力数目    
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
    public virtual void AttackStar(StarElement enemyStar, int attackNum) { }

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
    public virtual void SupportStar(StarElement friendStar, int supportNum) { }

    /// <summary>
    /// 自动产生兵力
    /// </summary>
    protected virtual void UpdateCreateTroop() { }
}