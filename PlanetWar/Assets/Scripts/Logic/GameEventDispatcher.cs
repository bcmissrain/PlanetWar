using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class EventNameList
{
    //松手 
    public static string GAME_INPUT_RELEASE_EVENT   = "input_release";
    //飞船爆炸
    public static string LEVEL_SHIP_BOOM_EVENT      = "ship_boom";
}

public class EventData
{
    public int intData1,intData2 = 0;
    public bool boolData1, boolData2 = false;
    public string stringData1,stringData2 = "";
    public object objData1,objData2 = null;
}

/// <summary>
/// 游戏消息分发管理器
/// </summary>
public class GameEventDispatcher{

    public static readonly GameEventDispatcher instance = new GameEventDispatcher();
    private GameEventDispatcher() { }
    public delegate void EventDealer(EventData eventData);
    public Dictionary<string,EventDealer> eventMap = new Dictionary<string, EventDealer>();

    public void RegistEventHandler(string eventName, EventDealer eventDealer)
    {
        if (eventMap.ContainsKey(eventName))
        {
            eventMap[eventName] += eventDealer;
        }
        else
        {
            eventMap.Add(eventName, new EventDealer(eventDealer));
        }
    }

    public void RemoveEventHandler(string eventName, EventDealer eventDealer)
    {
        if (eventMap.ContainsKey(eventName))
        {
            eventMap[eventName] -= eventDealer;
            if (eventMap[eventName] == null)
            {
                eventMap.Remove(eventName);
            }
        }
    }

    public void RemoveEventHandlerByName(string eventName)
    {
        if (eventMap.ContainsKey(eventName))
        {
            eventMap[eventName] = null;
            eventMap.Remove(eventName);
        }
    }

    public void Clear()
    {
        foreach (var key in eventMap.Keys)
        {
            RemoveEventHandlerByName(key);
        }
        eventMap.Clear();
    }

    public bool InvokeEvent(string eventName,EventData eventData)
    {
        if (eventMap.ContainsKey(eventName))
        {
            eventMap[eventName].Invoke(eventData);
            return true;
        }
        return false;
    }
}
