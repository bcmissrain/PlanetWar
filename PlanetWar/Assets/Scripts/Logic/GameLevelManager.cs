using UnityEngine;
using System.Collections;

public class GameLevelManager : MonoBehaviour {
    public GameObject shipPrefab;

    void Awake()
    {
        MasterPoolManager.instance.InitManager();
        ShipPoolManager.instance.InitManager(shipPrefab);
        StarPoolManager.instance.InitManager();
    }

	void Start () {
        //自动添加所有的行星信息
        var starList = GameObject.FindGameObjectsWithTag("Star");
        if (starList != null)
        {
            for (int i = 0; i < starList.Length; i++)
            {
                var starScript = starList[i].GetComponent<StarElement>();
                StarPoolManager.instance.CacheStar(starScript.m_Index,starList[i]);
            }
        }
    }

	void Update () {

        IfGameWin();
    }

    void OnDestroy()
    {
        ShipPoolManager.instance.ReleaseManager();
        StarPoolManager.instance.ReleaseManager();
        MasterPoolManager.instance.ReleaseManager();
    }

    bool IfGameWin()
    {
        var masterMap = MasterPoolManager.masterMap;
        foreach (var item in masterMap.Values)
        {
            if (item.m_ControllerType == ControllerType.Computer)
            {
                if (!item.masterUpdater.IfLoseGame())
                {
                    return false;
                }
            }
        }
        return true;
    }
}
