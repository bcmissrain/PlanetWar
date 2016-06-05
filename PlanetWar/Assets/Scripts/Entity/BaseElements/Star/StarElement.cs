using UnityEngine;
using System.Collections;

//行星类型
public class StarType
{
    public const string TroopStar = "TroopStar";        //普通行星
    public const string DefenceStar = "DefenceStar";    //防卫行星
    public const string MasterStar = "MasterStar";      //要塞行星
    public const string DoorStar = "DoorStar";          //传送行星
}

public class StarThemeColor
{
    public const string Grey    = "Grey";
    public const string Blue    = "Blue";
    public const string Red     = "Red";
    public const string Green   = "Green";
    public const string Yellow  = "Yellow";
    public const string Orange  = "Orange";
    public const string Black   = "Black";
}

//行星等级
public class StarLevel
{
    public const int Level0 = 0;
    public const int Level1 = 1;
    public const int Level2 = 2;
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
public class ShipShowType
{
    public const string Cloud = "Cloud";          //云状
    public const string Ring = "Ring";            //环状
}

/// <summary>
/// 行星基类
/// </summary>
[RequireComponent(typeof(ShipSender))]
public class StarElement : MonoBehaviour
{
    public ShipSender shipSender;       //飞船生成&发射器
    public StarUpdater starUpdater;     //行星逻辑更新器
    public StarMaterial starMaterial;   //行星UI管理器

    public GameObject m_StarSon;        //飞船旋绕子节点 （不显示）

    public int m_Index;                 //行星索引，行星的所属可变，但是索引唯一
    public int m_MasterIndex;           //主人索引
    public string m_StarType            //种类（不可变）
        = StarType.TroopStar;
    public int m_StarLevel              //等级（可变）
        = StarLevel.Level0;
    public int m_MaxTroop;              //产生兵力的最大值，超过不再生产
    public int m_StartTroopNum;         //初始化的起始兵力
    public float m_BornTime;            //产生兵力的时间
    public int m_BornNum;               //一次产生兵力的数目
    public float m_DetectScope;         //监测范围
    public float m_ShipFlySpeed;        //飞船飞行速度
    public string m_ShipShowType        //飞船展现方式
        = ShipShowType.Cloud;

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

    public AudioClip m_WinStarSound;
    public AudioClip m_LoseStarSound;

    public AudioSource m_AudioSource;

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
                oldMaster.LoseStarElement(this);
            }

            //添加新的
            var newMaster = MasterPoolManager.instance.GetMasterByIndex(masterIndex);
            if (newMaster)
            {
                newMaster.WinStarElement(this);

                if (m_AudioSource)
                {
                    if (newMaster.m_ControllerType == ControllerType.Human)
                    {
                        m_AudioSource.clip = m_WinStarSound;
                        m_AudioSource.Play();
                    }
                    else
                    {
                        m_AudioSource.clip = m_LoseStarSound;
                        m_AudioSource.Play();
                    }
                }

            }

            //更改索引
            this.m_MasterIndex = masterIndex;

            //播放动画
            //更改UI
            var starMat = starMaterial.GetMaterialByStar(newMaster.m_ThemeColor);
            if (starMat)
            {
                this.renderer.material = starMat;
            }

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