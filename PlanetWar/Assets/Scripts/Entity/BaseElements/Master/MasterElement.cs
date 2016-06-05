using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 控制者类型
/// </summary>
public static class ControllerType
{
    public const string Human       = "Human";
    public const string Computer    = "Computer";
    public const string None        = "None";
}

public static class ThemeColor
{
    public const string Grey    = "Grey";
    public const string Blue    = "Blue";
    public const string Green   = "Green";
    public const string Yellow  = "Yellow";
    public const string Red     = "Red";
    public const string Orange  = "Orange";
    public const string Black   = "Black";
    public const string White   = "White";

}

public class MasterElement : MonoBehaviour {
    public MasterUpdater masterUpdater;     //Master逻辑控制

    public int m_Index;                     //主人索引
    public int m_EnemyIndex = -1;           //敌方索引
    public List<StarElement> m_StarList     //控制的星球列表
        = new List<StarElement>();
    public int m_StarCount                  //具有的行星数目
    {
        get { return m_StarList.Count; }
    }
    public string m_ControllerType          //控制者类型
        = ControllerType.None;
    public string m_ThemeColor              //主题色
        = ThemeColor.Grey;
    public int m_ShipCount;                 //具有的飞船总数目
    public int m_StarAbility;               //具有的行星数目（非实时）
    public float m_BornAbility;             //1秒产生兵力的能力（非实时）
    public float m_RealBornAbility;         //实际上1秒产生兵力的能力（非实时）
    
    /// <summary>
    ///更新行星数目
    /// </summary>
    public void UpdateStarAbility()
    {
        m_StarAbility = m_StarList.Count;
    }

    /// <summary>
    /// 更新产生兵力的能力
    /// </summary>
    public void UpdateBornAbility()
    {
        float bornAbility = 0;
        float realBornAbility = 0;

        for (int i = 0; i < m_StarList.Count; i++)
        {
            float bornTime = m_StarList[i].m_BornTime;
            float bornNum = m_StarList[i].m_BornNum;
            if (bornNum > 0 && bornTime > 0.001)
            {
                float bornSpeed = bornNum / bornTime;
                bornAbility += bornSpeed;
                if (!m_StarList[i].IfFullShip())
                {
                    realBornAbility += bornSpeed;
                }
            }
        }
        m_BornAbility = bornAbility;
        m_RealBornAbility = realBornAbility;

        //print("Master"+ this.m_Index +" UpdateBornAbility m_BornAbility:" + m_BornAbility);
        //print("Master" + this.m_Index + " UpdateBornAbility m_RealBornAbility:" + m_RealBornAbility);
    }

    /// <summary>
    /// 添加新的星球
    /// </summary>
    public void AddStarElement(StarElement starElement)
    {
        if (starElement)
        {
            m_StarList.Add(starElement);
        }
    }

    /// <summary>
    /// 添加新的星球（占领了新的星球）
    /// </summary>
    public void WinStarElement(StarElement starElement)
    {
        if (starElement)
        {
            m_StarList.Add(starElement);
        }
    }

    /// <summary>
    /// 删除已有星球
    /// </summary>
    public void RemoveStarElement(StarElement starElement)
    {
        if (starElement)
        {
            m_StarList.Remove(starElement);
        }
    }

    /// <summary>
    /// 删除已有星球（旧星球被占领）
    /// </summary>
    public void LoseStarElement(StarElement starElement)
    {
        if (starElement)
        {
            m_StarList.Remove(starElement);
        }
    }
}
