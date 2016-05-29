using UnityEngine;
using System.Collections;

[RequireComponent(typeof(StarElement))]
[RequireComponent(typeof(ShipSender))]
public class StarUpdater : MonoBehaviour {
    public StarElement starElement;
    public ShipSender shipSender;
    public float thinkTime = 0;
    private float bornTimeCounter = 0;
    private float logicTimeCounter = 0;

    void Start () {
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.LEVEL_SHIP_BOOM_EVENT, starElement.OnShipDestroy);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.GAME_INPUT_RELEASE_EVENT, shipSender.OnSendTroop);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.STAR_SEND_SHIP, this.OnSentTroopNotify);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.STAR_ASK_HELP, this.OnAskedHelp);
    }
	
    /// <summary>
    /// 1.更新生成飞船
    /// 2.更新星球决策
    /// 3.更新飞船派遣
    /// </summary>
	void Update () {
        //更新生成飞船
        UpdateShipBorn();

        //更新星球逻辑
        UpdateStarLogic();
        
        //更新派遣
        shipSender.UpdateSendTroop();
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

    private void DealWithAttackNotify(StarElement enemyStar, int enemyNum)
    {
        int deltaShipNum = GetDeltaShipByTime(enemyStar.transform.position,this.transform.position);
        int askHelpNum = enemyNum - deltaShipNum - starElement.m_TroopNum;
        Debug.Log("enemyNum :" + enemyNum + " DeltaNum :" + deltaShipNum + " thisNum :" + starElement.m_TroopNum+"Ask help:"+askHelpNum);
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
        float deltaTime = distance / SharedGameData.shipFlySpeed;
        int deltaShip = (int)(starElement.m_BornNum * deltaTime / starElement.m_BornTime);
        return deltaShip;
    }

    public void UpdateShipBorn()
    {
        bornTimeCounter += Time.deltaTime;
        if (bornTimeCounter >= starElement.m_BornTime)
        {
            bornTimeCounter = 0;

            int bornNum = starElement.m_BornNum;
            for (int i = 0; i < bornNum; i++)
            {
                if (!starElement.IfFullShip())
                {
                    shipSender.CreateTroop(starElement.m_MaxTroop, shipSender.shipList.Count, starElement.m_ShipShowType, 0.0f, starElement.GetScaleByLevel());
                }
            }
        }
    }

    public void UpdateStarLogic()
    {
        logicTimeCounter += Time.deltaTime;
        if (logicTimeCounter >= this.thinkTime)
        {
            logicTimeCounter = 0;

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

}
