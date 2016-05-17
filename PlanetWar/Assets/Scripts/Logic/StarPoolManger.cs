using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 行星列表管理器
/// </summary>
public class StarPoolManager{
    public static Dictionary<int, GameObject> starMap;

    private StarPoolManager() { }

    public static readonly StarPoolManager instance = new StarPoolManager();

    public void InitManager()
    {
        starMap = new Dictionary<int, GameObject>();
    }

    public void ReleaseManager()
    {
        foreach(var sm in starMap.Values)
        {
            GameObject.Destroy(sm);
        }
        starMap.Clear();
    }

    public void CacheStar(int index, GameObject star)
    {
        starMap.Add(index, star);
    }

    /// <summary>
    /// 通过索引获取行星
    /// </summary>
    /// <returns>null表示不存在</returns>
    public GameObject GetStarByIndex(int index)
    {
        GameObject result = null;
        if (starMap.TryGetValue(index,out result))
        {
            return result;
        }
        return null;
    }

    /// <summary>
    /// 通过行星对象获取索引值
    /// </summary>
    /// <returns>-1表示不存在</returns>
    public int GetIndexByGameObj(GameObject star)
    {
        foreach (var item in starMap)
        {
            if (star == item.Value)
            {
                return item.Key;
            }
        }

        return -1;
    }
}
