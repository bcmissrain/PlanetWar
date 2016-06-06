using UnityEngine;
using System.Collections.Generic;

public class MasterPoolManager{
    public static Dictionary<int, MasterElement> masterMap = new Dictionary<int, MasterElement>();

    private MasterPoolManager() { }

    public static readonly MasterPoolManager instance = new MasterPoolManager();

    public void InitManager()
    {
        ReleaseManager();
        masterMap = new Dictionary<int, MasterElement>();
    }

    public void ReleaseManager()
    {
        masterMap.Clear();
    }

    public void AddMasterByIndex(int index,MasterElement master)
    {
        masterMap.Add(index, master);
    }

    public void RemoveMasterByIndex(int index)
    {
        if (masterMap.ContainsKey(index))
        {
            masterMap.Remove(index);
        }
    }

    public MasterElement GetMasterByIndex(int index)
    {
        MasterElement masterElement = null;
        if (masterMap.TryGetValue(index,out masterElement))
        {
            return masterElement;
        }
        return null;
    }

    public int GetIndexByObj(MasterElement masterElement)
    {
        foreach (var item in masterMap)
        {
            if (masterElement == item.Value)
            {
                return item.Key;
            }
        }

        return -1;
    }
}