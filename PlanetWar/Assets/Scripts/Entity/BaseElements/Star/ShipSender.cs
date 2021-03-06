﻿using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 飞船生成&发射器
/// </summary>
[RequireComponent(typeof(StarElement))]
public class ShipSender : MonoBehaviour
{
    public StarElement starElement;         //行星的基本信息

    public float ringLength;                //生成环长度
    public float ringDepth;                 //生成环深度
    public float ringAddition;              //环增加倍数

    public float sendShipTime;              //一次派遣飞船的间隔时间
    public int sendShipNum;                 //一次派遣飞船的数目   

    public List<GameObject> shipList;       //飞船列表
    public List<GameObject> sendList;       //发送的飞船列表

    private float sendTimeCounter = 0;      //发射时间计数
    private List<GameObject> tmpTransList = new List<GameObject>();

    /// <summary>
    /// 产生数目为n个飞船
    /// </summary>
    public void CreateTroopBy(int num)
    {
        float ringScale = starElement.GetScaleByLevel();
        int ringTroopNum = starElement.m_MaxTroop;
        string showType = starElement.m_ShipShowType;
        for (int i = 0; i < num; i++)
        {
            CreateTroop(ringTroopNum, shipList.Count, showType, 0, ringScale);
        }
    }

    /// <summary>
    /// 删除数目为n个飞船
    /// </summary>
    /// <param name="num"></param>
    public void DestroyTroopBy(int num)
    {
        if (num > shipList.Count)
        {
            num = shipList.Count;
        }
        int totalNum = shipList.Count;
        for (int i = totalNum - 1; i >= totalNum - num; i--)
        {
            shipList[i].GetComponent<ShipElement>()._Destroy();
            shipList.RemoveAt(i);
        }
    }

    /// <summary>
    /// 产生一个新的飞船
    /// </summary>
    /// <param name="totoal">一轮的总数目</param>
    /// <param name="beginIndex">新飞船的index</param>
    /// <param name="degree">倾斜度数</param>
    /// <param name="scale">放大倍数</param>
    public void CreateTroop(int total,int beginIndex,string showType,float degree = 0.0f,float scale = 1.0f)
    {
        if (showType == ShipShowType.Cloud)
        {
            //第几轮
            int ringIndex = (beginIndex / total) % 3;
            //第几个
            int realIndex = beginIndex % total;
            //平均距离度数
            int deltaDeg = 360 / total;
            degree = ringIndex * 120;

            var newShip = ShipPoolManager.instance.BorrowShip();
            var cachePos = this.transform.position;
            float posX = ringLength * Mathf.Sin(Mathf.Deg2Rad * deltaDeg * realIndex) * scale;
            float posY = ringDepth * Mathf.Cos(Mathf.Deg2Rad * deltaDeg * realIndex) * scale;
            float posZ = ringLength * Mathf.Cos(Mathf.Deg2Rad * deltaDeg * realIndex) * scale;

            cachePos.x += posX;
            cachePos.y += posY;
            cachePos.z += posZ;

            newShip.transform.position = cachePos;
            var tempRotation = newShip.transform.rotation;
            newShip.transform.RotateAround(this.transform.position, Vector3.forward, degree);
            newShip.transform.rotation = tempRotation;
            
            //设置父节点
            newShip.transform.parent = this.transform;
            shipList.Add(newShip);

            var shipScript = newShip.GetComponent<ShipElement>();
            var shipMatScript = newShip.GetComponent<ShipMaterial>();
            shipScript.m_BaseFlySpeed = starElement.m_ShipFlySpeed;
            
            //设置主人
            shipScript.m_MasterIndex = starElement.m_MasterIndex;

            //设置UI
            var shipMat = shipMatScript.GetMaterialByShip(starElement.GetMasterElement().m_ThemeColor);
            if (shipMat)
            {
                newShip.renderer.material = shipMat;
            }

            //发送生成飞船的消息
            EventData eventData = new EventData();
            eventData.intData1 = starElement.m_MasterIndex;
            GameEventDispatcher.instance.InvokeEvent(EventNameList.LEVEL_SHIP_BORN_EVENT, eventData);
        }
        else if (showType == ShipShowType.Ring)
        {
            //默认容量扩为两倍
            int ringIndex = (beginIndex / total) % 3;
            int realIndex = beginIndex % total + ringIndex * total;
            int deltaDeg = 360 / total;
            if (ringIndex > 0)
            {
                deltaDeg /= 2;
                //加一层
                scale *= 1.5f;
            }

            var newShip = ShipPoolManager.instance.BorrowShip();
            var cachePos = this.transform.position;

            float posX = ringLength * Mathf.Sin(Mathf.Deg2Rad * deltaDeg * realIndex) * scale;
            float posY = ringDepth * Mathf.Cos(Mathf.Deg2Rad * deltaDeg * realIndex) * scale;
            float posZ = ringLength * Mathf.Cos(Mathf.Deg2Rad * deltaDeg * realIndex) * scale;

            cachePos.x += posX;
            cachePos.y += posY;
            cachePos.z += posZ;

            newShip.transform.position = cachePos;
            var tempRotation = newShip.transform.rotation;
            newShip.transform.RotateAround(this.transform.position, Vector3.forward, degree);
            newShip.transform.rotation = tempRotation;

            //设置父节点
            newShip.transform.parent = this.transform;
            shipList.Add(newShip);

            var shipScript = newShip.GetComponent<ShipElement>();
            var shipMatScript = newShip.GetComponent<ShipMaterial>();

            //设置主人
            shipScript.m_MasterIndex = starElement.m_MasterIndex;

            //设置UI
            var shipMat = shipMatScript.GetMaterialByShip(starElement.GetMasterElement().m_ThemeColor);
            if (shipMat)
            {
                newShip.renderer.material = shipMat;
            }

            //发送生成飞船的消息
            EventData eventData = new EventData();
            eventData.intData1 = starElement.m_MasterIndex;
            GameEventDispatcher.instance.InvokeEvent(EventNameList.LEVEL_SHIP_BORN_EVENT, eventData);
        }
    }

    /// <summary>
    /// 派遣飞船命令
    /// </summary>
    /// <param name="starIndex">飞船索引</param>
    /// <param name="percent">派遣比例</param>
    public void SendTroopTo(int starIndex, float percent)
    {
        if (starIndex >= 0)
        {
            //容错
            if (percent > 1.0f)
            {
                percent = 1.0f;
            }

            //计算派遣数目
            int sendNum = (int)(shipList.Count * percent);
            if (sendNum == 0)
            {
                //如果有飞船且计算的值为0，则置为1
                if (percent > 0 && shipList.Count > 0)
                {
                    sendNum = 1;
                }
            }

            EventData sendNotify = new EventData();
            sendNotify.intData1 = this.starElement.m_Index;     //源行星
            sendNotify.intData2 = starIndex;                    //目标行星
            sendNotify.intData3 = sendNum;                      //派遣兵力值
            //发送攻击通知
            GameEventDispatcher.instance.InvokeEvent(EventNameList.STAR_SEND_SHIP, sendNotify);


            //移除派遣星球
            if (sendNum > 0)
            {
                int totalCount = shipList.Count;
                for (int i = totalCount - 1; i >= totalCount - sendNum; i--)
                {
                    var delShip = shipList[i];
                    sendList.Add(delShip);
                    //这里并没有正式派遣，正式派遣在BeginSendTroop
                    var delShipScript = delShip.GetComponent<ShipElement>();
                    delShipScript.SetTarget(starElement.m_Index, starIndex);
                    shipList.RemoveAt(i);
                }
            }
        }
    }

    /// <summary>
    /// 根据设置的时间和数目派遣飞船
    /// </summary>
    public void UpdateSendTroop()
    {
        if (sendList.Count > 0)
        {
            sendTimeCounter += Time.deltaTime;
            if (sendTimeCounter >= sendShipTime)
            {
                sendTimeCounter = 0;
                int minNum = sendShipNum;
                if (sendShipNum > sendList.Count)
                {
                    minNum = sendList.Count;
                }

                //准备发射
                for (int i = 0; i < minNum; i++)
                {
                    tmpTransList.Add(sendList[i]);
                }

                //删除环绕
                for (int i = 0; i < minNum; i++)
                {
                    sendList.RemoveAt(0);
                }

                //发射
                BeginSendTroop(tmpTransList);
                
                //记得重置
                tmpTransList.Clear();
            }
        }
    }

    /// <summary>
    /// 派遣飞船的操作
    /// </summary>
    /// <param name="troop"></param>
    protected void BeginSendTroop(List<GameObject> troop)
    {
        if (troop != null && troop.Count > 0)
        {
            ShipTransportManager.AddTransportTroop(tmpTransList);
            for (int i = 0; i < troop.Count; i++)
            {
                //重置父节点
                troop[i].transform.parent = null;
                var delShipScript = troop[i].GetComponent<ShipElement>();
                
                //出发
                delShipScript.m_CanMove = true;
            }
        }
    }

    /// <summary>
    /// 设置飞船生成点位置
    /// </summary>
    /// <param name="total">总数</param>
    /// <param name="curIndex">当前索引</param>
    protected void GetShipBornPoint(GameObject ship,int total, int curIndex,float distance)
    {
        int deltaDeg = 360 / total;
        int degree = curIndex * deltaDeg;
        float posX = ringLength * Mathf.Sin(Mathf.Deg2Rad * deltaDeg * curIndex);
        float posY = ringDepth * Mathf.Cos(Mathf.Deg2Rad * deltaDeg * curIndex);
        float posZ = ringLength * Mathf.Cos(Mathf.Deg2Rad * deltaDeg * curIndex);
        var cachePos = this.transform.position;

        cachePos.x += posX;
        cachePos.y += posY;
        cachePos.z += posZ;

        ship.transform.position = cachePos;
        var tempRotation = ship.transform.rotation;
        ship.transform.RotateAround(this.transform.position, Vector3.forward, degree);
        ship.transform.rotation = tempRotation;
    }

    public void OnSendTroop(EventData eventData)
    {
        if (starElement.m_Index == eventData.intData1)
        {
            SendTroopTo(eventData.intData2, SharedGameData.TroopSendPercent);
        }
    }
}