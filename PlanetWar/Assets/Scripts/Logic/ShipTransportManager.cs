using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 飞船传输管理器
/// </summary>
public class ShipTransportManager : MonoBehaviour {
    public static List<List<GameObject>> shipList = //传输列表
        new List<List<GameObject>>();

    void Start () {
	    
	}
	
	void Update () {
        UpdateFilterSihp();
	}

    /// <summary>
    /// 添加发送飞船分组
    /// </summary>
    public static void AddTransportTroop(List<GameObject> troop)
    {
        if (troop != null && troop.Count > 0)
        {
            List<GameObject> newTroop = new List<GameObject>(troop);
            shipList.Add(newTroop);
        }
    }

    /// <summary>
    /// 清理所有传输飞船分组
    /// </summary>
    public static void ClearTransportList()
    {
        for (int i = 0; i < shipList.Count; i++)
        {
            shipList[i].Clear();
        }
        shipList.Clear();
    }

    /// <summary>
    /// 过滤
    /// </summary>
    public static void UpdateFilterSihp()
    {
        for (int i = 0; i < shipList.Count; i++)
        {
            int counter = 0;
            for (int j = 0; j < shipList[i].Count; j++)
            {
                if (shipList[i][j] != null)
                {
                    if (shipList[i][j].activeSelf == true)
                    {
                        counter++;
                        break;
                    }
                }
            }
            //全都传完
            if (counter == 0)
            {
                shipList[i].Clear();
                shipList.RemoveAt(i);
                //记得修改列表
                --i;
            }
        }
    }
}