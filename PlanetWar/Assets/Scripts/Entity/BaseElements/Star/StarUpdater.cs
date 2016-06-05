using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(StarElement))]
[RequireComponent(typeof(ShipSender))]
public class StarUpdater : MonoBehaviour {
    public StarElement starElement;
    public ShipSender shipSender;

    public float thinkTime = 0;

    private float logicTimeCounter = 0;

    private List<StarElement> nearStarList  //附近的星球列表   非实时
        = new List<StarElement>();
    public float safeDegree = 0;            //安全度           非实时 
    public bool haveEmptyStar = false;      //附近有没有空行星 非实时

    private float bornTimeCounter = 0;

    void Start () {
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.LEVEL_SHIP_BOOM_EVENT, starElement.OnShipDestroy);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.GAME_INPUT_RELEASE_EVENT, shipSender.OnSendTroop);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.STAR_SEND_SHIP, this.OnSentTroopNotify);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.STAR_ASK_HELP, this.OnAskedHelp);

        //初始生成默认数目的飞船
        shipSender.CreateTroopBy(starElement.m_StartTroopNum);
    }
	
    /// <summary>
    /// 1.更新生成飞船
    /// 2.更新星球决策
    /// 3.更新飞船派遣
    /// </summary>
	void Update () {
        //更新生成飞船
        UpdateShipBorn();

        logicTimeCounter += Time.deltaTime;
        if (logicTimeCounter >= this.thinkTime)
        {
            logicTimeCounter = 0;

            //更新安全度计算
            UpdateSafeDetect();

            //更新星球逻辑
            UpdateStarLogic();
        }

        //更新派遣
        UpdateTroopSend();
    }

    void OnDestroy()
    {
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.LEVEL_SHIP_BOOM_EVENT, starElement.OnShipDestroy);
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.GAME_INPUT_RELEASE_EVENT, shipSender.OnSendTroop);
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.STAR_SEND_SHIP, this.OnSentTroopNotify);
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.STAR_ASK_HELP, this.OnAskedHelp);
    }

    /// <summary>
    /// 当有飞船被派遣来进攻or支援该行星
    /// </summary>
    public void OnSentTroopNotify(EventData eventData)
    {
        int sourceIndex = eventData.intData1;
        int targetIndex = eventData.intData2;
        int troopNum = eventData.intData3;

        if (starElement.m_MasterIndex == -1)
        {
            return;
        }

        //目标是本行星
        if (targetIndex == this.starElement.m_Index)
        {

            var sourceStar = StarPoolManager.instance.GetStarByIndex(sourceIndex);
            if (sourceStar)
            {
                var sourceScript = sourceStar.GetComponent<StarElement>();
                if (sourceScript)
                {
                    //支援
                    if (starElement.m_MasterIndex == sourceScript.m_MasterIndex)
                    {
                        //不处理
                    }
                    //攻击
                    else
                    {
                        DealWithAttackNotify(sourceScript, troopNum);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 当受到同伴的求救信息
    /// </summary>
    public void OnAskedHelp(EventData eventData)
    {
        //根据1.当前策略 2.本行星安全度 3.派遣后安全度 来排遣兵力
    }

    /// <summary>
    /// 处理被敌方攻击的消息
    /// </summary>
    /// <param name="enemyStar">敌方行星</param>
    /// <param name="enemyNum">派遣的兵力数目</param>
    private void DealWithAttackNotify(StarElement enemyStar, int enemyNum)
    {
        int deltaShipNum = GetDeltaShipByTime(enemyStar.transform.position,this.transform.position);
        int askHelpNum = enemyNum - deltaShipNum - starElement.m_TroopNum;
        //print("enemyNum :" + enemyNum + " DeltaNum :" + deltaShipNum + " thisNum :" + starElement.m_TroopNum+"Ask help:"+askHelpNum);
        //打不过我
        if (askHelpNum <= 0)
        {
            //静静做我的美男子
        }
        else
        {
            EventData helpData = new EventData();
            helpData.intData1 = this.starElement.m_Index;       //我的索引
            helpData.intData2 = this.starElement.m_MasterIndex; //主人
            helpData.intData3 = askHelpNum;                     //求助兵力数量
            GameEventDispatcher.instance.InvokeEvent(EventNameList.STAR_ASK_HELP, helpData);
        }
    }

    /// <summary>
    /// 计算两个行星之间相距的“飞船”数目
    /// </summary>
    public int GetDeltaShipByTime(Vector3 pos1, Vector3 pos2)
    {
        Vector3 deltaPos = pos1 - pos2;
        deltaPos.z = 0;
        float distance = deltaPos.magnitude;
        float deltaTime = distance / starElement.m_ShipFlySpeed;
        int deltaShip = (int)(starElement.m_BornNum * deltaTime / starElement.m_BornTime);
        return deltaShip;
    }

    /// <summary>
    /// 更新产生飞船
    /// </summary>
    public void UpdateShipBorn()
    {
        var masterObj = MasterPoolManager.instance.GetMasterByIndex(starElement.m_MasterIndex);
        if (masterObj)
        {
            if (masterObj.m_ControllerType != ControllerType.None)
            {
                bornTimeCounter += Time.deltaTime;
                if (bornTimeCounter >= starElement.m_BornTime)
                {
                    bornTimeCounter = 0;

                    starElement.CreateTroopOnce();
                }
            }
        }
    }

    /// <summary>
    /// 更新行星飞船派遣的逻辑
    /// </summary>
    public void UpdateStarLogic()
    {
        var masterObj = MasterPoolManager.instance.GetMasterByIndex(starElement.m_MasterIndex);
        if (masterObj)
        {
            if (masterObj.m_ControllerType == ControllerType.Computer)
            {
                switch (starElement.m_StarType)
                {
                    case StarType.TroopStar:
                        UpdateTroopStarLogic();
                        break;
                    case StarType.DefenceStar:
                        UpdateDefenceStarLogic();
                        break;
                    case StarType.MasterStar:
                        UpdateMasterStarLogic();
                        break;
                    case StarType.DoorStar:
                        UpdateDoorStarLogic();
                        break;
                }
            }
        }
    }

    #region 更新不同行星的逻辑
    public void UpdateTroopStarLogic()
    {
        MasterElement myMaster = starElement.GetMasterElement();
        MasterElement enemyMaster = null;
        if (myMaster)
        {
            enemyMaster = MasterPoolManager.instance.GetMasterByIndex(myMaster.m_EnemyIndex);
        }

        if (enemyMaster)
        {
            //Test
            if (enemyMaster.m_StarList.Count > 0)
            {
                StarElement enemyStar = enemyMaster.m_StarList[0];
                shipSender.SendTroopTo(enemyStar.m_Index, 0.5f);
            }
        }

    }

    public void UpdateDefenceStarLogic()
    {

    }

    public void UpdateMasterStarLogic()
    {

    }

    public void UpdateDoorStarLogic()
    {

    }
    #endregion

    /// <summary>
    /// 更新安全度度量
    /// </summary>
    public void UpdateSafeDetect()
    {
        float detectScopeSqr = starElement.m_DetectScope * starElement.m_DetectScope;       //距离的平方，为了快速计算
        nearStarList.Clear();
        haveEmptyStar = false;
        safeDegree = this.starElement.m_TroopNum;

        var myMaster = starElement.GetMasterElement();

        //计算周围行星列表
        foreach (var item in StarPoolManager.starMap.Values)
        {
            if (item != this.gameObject)
            {
                Vector3 deltaPos = item.transform.position - this.transform.position;
                deltaPos.z = 0;
                //监测范围内
                if (deltaPos.sqrMagnitude <= detectScopeSqr)
                {
                    //print("UpdateSafeDetect " + deltaPos.sqrMagnitude + ":" + detectScopeSqr);
                    nearStarList.Add(item.GetComponent<StarElement>());
                }
            }           
        }

        //print(this.gameObject.name + "范围" + starElement.m_DetectScope+"内有："+nearStarList.Count);

        //统计周围行星列表相关信息
        if (nearStarList.Count > 0)
        {
            for (int i = 0; i < nearStarList.Count; i++)
            {

                var currentNearStar = nearStarList[i];
                var otherMaster = currentNearStar.GetMasterElement();

                //空行星
                if (otherMaster.m_ControllerType == ControllerType.None)
                {
                    haveEmptyStar = true;
                    continue;
                }
                //人 or PC
                else
                {
                    //队友
                    //更安全
                    if (otherMaster.m_Index == myMaster.m_Index)
                    {
                        safeDegree += currentNearStar.m_TroopNum;
                    }
                    //目标敌人
                    //不安全
                    else if (otherMaster.m_Index == myMaster.m_EnemyIndex)
                    {
                        safeDegree -= currentNearStar.m_TroopNum;
                    }
                    //非目标敌人         
                    //有点不安全（目前给一个0.5f的默认值）
                    else
                    {
                        safeDegree -= 0.5f * currentNearStar.m_TroopNum;
                    }
                }
            }
        }

        //print(this.gameObject.name + "更新行星安全度:"+safeDegree);
    }

    /// <summary>
    /// 自动发送兵力
    /// </summary>
    public void UpdateTroopSend()
    {
        shipSender.UpdateSendTroop();
    }
}
