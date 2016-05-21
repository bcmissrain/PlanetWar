using UnityEngine;
using System.Collections;

[RequireComponent(typeof(StarElement))]
[RequireComponent(typeof(ShipSender))]
public class StarUpdater : MonoBehaviour {
    public StarElement starElement;
    public ShipSender shipSender;

    private float timeCounter = 0;
    void Start () {
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.LEVEL_SHIP_BOOM_EVENT, starElement.OnShipDestroy);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.GAME_INPUT_RELEASE_EVENT, shipSender.OnSendTroop);
        ShipPoolManager.instance.InitManager(shipSender.shipPrefab);
        StarPoolManager.instance.InitManager();
        shipSender.CreateTroopBy(10);
        if (StarPoolManager.starMap.Count == 0)
        {
            StarPoolManager.instance.CacheStar(0, GameObject.Find("StarFrame_0"));
            StarPoolManager.instance.CacheStar(1, GameObject.Find("StarFrame_1"));
            StarPoolManager.instance.CacheStar(2, GameObject.Find("StarFrame_2"));
        }
    }
	
	void Update () {
        timeCounter += Time.deltaTime;
        if (timeCounter >= starElement.m_BornTime)
        {
            timeCounter = 0;
            if (shipSender.shipList.Count < starElement.m_MaxTroop)
            {
                //CreateTroopTo(1);
                shipSender.CreateTroop(starElement.m_MaxTroop, shipSender.shipList.Count, starElement.m_ShipShowType, 0.0f, starElement.GetScaleByLevel());
            }
        }

        shipSender.UpdateSendTroop();
    }

    void OnDestroy()
    {
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.LEVEL_SHIP_BOOM_EVENT, starElement.OnShipDestroy);
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.GAME_INPUT_RELEASE_EVENT, shipSender.OnSendTroop);
    }
}
