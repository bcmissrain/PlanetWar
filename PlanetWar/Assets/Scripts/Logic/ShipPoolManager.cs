using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 飞船缓存池
/// </summary>
public class ShipPoolManager{
    private static readonly int cacheSize = 512;        //基本缓存数目     
    public static List<GameObject> shipList;            //飞船列表
    private GameObject shipPrefab;                      //飞船预设

    private ShipPoolManager() { }

    public static readonly ShipPoolManager instance = new ShipPoolManager();

    /// <summary>
    /// 初始化缓存对象
    /// </summary>
    public void InitManager(GameObject shipPrefab)
    {
        shipList = new List<GameObject>();
        
        //缓存预设
        this.shipPrefab = shipPrefab;

        for (int i = 0; i < cacheSize; i++)
        {
            var newShip = GameObject.Instantiate(shipPrefab) as GameObject;
            newShip.SetActive(false);
            shipList.Add(newShip);
        }
    }

    /// <summary>
    /// 注销所有缓存对象
    /// </summary>
    public void ReleaseManager()
    {
        if (shipList != null)
        {
            for (int i = 0; i < cacheSize; i++)
            {
                GameObject.Destroy(shipList[i]);
            }

            shipList.Clear();
        }
    }

    /// <summary>
    /// 借用可用飞机
    /// </summary>
    public GameObject BorrowShip()
    {
        for (int i = 0; i < shipList.Count; i++)
        {
            if (!shipList[i].activeSelf)
            {
                //要重新初始化
                shipList[i].GetComponent<ShipElement>()._Init();
                shipList[i].SetActive(true);
                return shipList[i];
            }
        }

        //超出缓存添加新飞船
        var newShip = GameObject.Instantiate(shipPrefab) as GameObject;
        shipList.Add(newShip);

        return newShip;
    }

    /// <summary>
    /// 归还飞机
    /// </summary>
    public void ReturnShip(GameObject oldShip)
    {
        if (oldShip)
        {
            //重置各项参数
            oldShip.transform.parent = null;
            oldShip.SetActive(false);
        }

        /*测试    如果不缓存而是直接删除
        for (int i = 0; i < shipList.Count; i++)
        {
            if (oldShip == shipList[i])
            {
                GameObject.Destroy(shipList[i]);
                shipList.RemoveAt(i);
                break;
            }
        }//*/
    }
}
