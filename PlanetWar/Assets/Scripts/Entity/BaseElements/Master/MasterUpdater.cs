using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MasterElement))]
public class MasterUpdater : MonoBehaviour {
    public MasterElement masterElement;

	void Start () {
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.LEVEL_SHIP_BORN_EVENT, OnShipBorn);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.LEVEL_SHIP_BOOM_EVENT, OnShipBoom);

        //注册到缓冲池
        MasterPoolManager.instance.AddMasterByIndex(masterElement.m_Index, masterElement);
	}
	
	void Update () {
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
}