using UnityEngine;
using System.Collections;

/// <summary>
/// 角色个性
/// </summary>
public static class AIType
{
    public const string MasterGoodMan  = "GoodMan"; 
    public const string MasterPeace    = "Peace";
    public const string MasterAttack   = "Attack";
    public const string MasterCoward   = "Coward";
    public const string MasterGod      = "God";
}

public enum BattleDecision
{
    Attack,
    Defence,
    Conquer
}

/// <summary>
/// 角色AI更新器
/// </summary>
[RequireComponent(typeof(MasterElement))]
public class MasterUpdater : MonoBehaviour {
    public MasterElement masterElement;

    public string masterType = AIType.MasterPeace;
    private BattleDecision currentDecision = BattleDecision.Conquer;
    public float thinkTime = 0;
    private float timeCounter = 0;

    private float defenceDegree = 0;
    private float conquerDegree = 0;
    private float attackDegree = 0;  

	void Start () {
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.LEVEL_SHIP_BORN_EVENT, OnShipBorn);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.LEVEL_SHIP_BOOM_EVENT, OnShipBoom);

        //注册到缓冲池
        MasterPoolManager.instance.AddMasterByIndex(masterElement.m_Index, masterElement);

        //第一次做决定
        UpdateMasterInfo();
        UpdateDecision();
	}
	
	void Update () {
        timeCounter += Time.deltaTime;
        if (timeCounter >= thinkTime)
        {
            timeCounter = 0;
            UpdateMasterInfo();
            UpdateDecision();
        }
	}

    void OnDestroy()
    {
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.LEVEL_SHIP_BORN_EVENT, OnShipBorn);
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.LEVEL_SHIP_BOOM_EVENT, OnShipBoom);
        MasterPoolManager.instance.RemoveMasterByIndex(masterElement.m_Index);
    }

    void OnShipBorn(EventData data)
    {
        if (data.intData1== masterElement.m_Index)
        {
            masterElement.m_ShipCount++;
        }
    }

    void OnShipBoom(EventData data)
    {
        if (data.intData3 == masterElement.m_Index)
        {
            masterElement.m_ShipCount--;
        }
    }

    /// <summary>
    /// 星球获取飞船的战略布局
    /// </summary>
    public BattleDecision GetDecision()
    {
        return currentDecision;
    }

    /// <summary>
    /// 更新统计信息
    /// </summary>
    public void UpdateMasterInfo()
    {
        masterElement.UpdateStarAbility();
        masterElement.UpdateBornAbility();
    }

    /// <summary>
    /// 做出战略决策
    /// * 对比
    /// * 1.总兵力数目
    /// * 2.生产兵力潜力
    /// * 3.目前生产兵力能力
    /// </summary>
    public void UpdateDecision()
    {
        //只有计算机模拟的角色才做决定
        if (masterElement.m_ControllerType == ControllerType.Computer)
        {
            var enemyMaster = MasterPoolManager.instance.GetMasterByIndex(masterElement.m_EnemyIndex);
            if (enemyMaster)
            {
                switch (this.masterType)
                {
                    case AIType.MasterGoodMan:
                        MakeDecisionByGoodMan(enemyMaster);
                        break;
                    case AIType.MasterPeace:
                        MakeDecisionByPeacer(enemyMaster);
                        break;
                    case AIType.MasterAttack:
                        MakeDecisionByAttacker(enemyMaster);
                        break;
                    case AIType.MasterCoward:
                        MakeDecisionByCoward(enemyMaster);
                        break;
                    case AIType.MasterGod:
                        MakeDecisionByGod(enemyMaster);
                        break;
                    default:
                        MakeDecisionByGoodMan(enemyMaster);
                        break;
                }
            }
            //没敌人也别做啥决定了
            else
            {
                MakeDecisionByGoodMan(enemyMaster);
            }
        }
    }

    private void MakeDecisionByGoodMan(MasterElement enemyMaster)
    {

    }

    private void MakeDecisionByPeacer(MasterElement enemyMaster)
    {

    }

    private void MakeDecisionByCoward(MasterElement enemyMaster)
    {

    }

    private void MakeDecisionByAttacker(MasterElement enemyMaster)
    {

    }

    private void MakeDecisionByGod(MasterElement enemyMaster)
    {

    }

    public bool IfLoseGame()
    {
        if (masterElement.m_ControllerType == ControllerType.Computer)
        {
            //没星球且没有飞机才算输
            if (masterElement.m_ShipCount == 0 && masterElement.m_StarList.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (masterElement.m_ControllerType == ControllerType.None)
        {
            return true;
        }
        else if(masterElement.m_ControllerType == ControllerType.Human)
        {
            if (masterElement.m_ShipCount == 0 && masterElement.m_StarList.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}