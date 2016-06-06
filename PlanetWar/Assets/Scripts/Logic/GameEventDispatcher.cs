using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class EventNameList
{
    //玩家松手 
    public static string GAME_INPUT_RELEASE_EVENT   = "input_release";
    //行星派遣兵力
    public static string STAR_SEND_SHIP             = "send_ship";
    //行星求助
    public static string STAR_ASK_HELP              = "star_help";
    //飞船产生
    public static string LEVEL_SHIP_BORN_EVENT      = "ship_born";
    //飞船爆炸
    public static string LEVEL_SHIP_BOOM_EVENT      = "ship_boom";
    //进入菜单（针对音乐）
    public static string MENU_BEGIN_MUSIC_PLAY      = "menu_music";
    //点击按钮（音效）
    public static string BUTTON_CLICK_OK_EVENT      = "click_ok_sound";
    public static string BUTTON_CLICK_NO_EVENT      = "click_no_sound";
    //重置音乐
    public static string MUSIC_RESET_EVENT          = "music_reset";
    //重置音效
    public static string SOUND_RESET_EVENT          = "sound_reset";
    //游戏开始（针对音乐）
    public static string LEVEL_BEGIN_MUSIC_PLAY     = "level_music";
    //玩家获得胜利
    public static string LEVEL_PLAYER_WIN_EVENT     = "level_win";
    //玩家失败
    public static string LEVEL_PLAYER_LOSE_EVENT    = "level_lose";

    //重置Option按钮状态
    public static string OPTION_BUTTON_RESET_EVENT  = "option_reset";
}

public class EventData
{
    public int intData1,intData2,intData3 = 0;
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
