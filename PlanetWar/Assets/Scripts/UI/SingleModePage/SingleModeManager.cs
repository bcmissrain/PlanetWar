using UnityEngine;
using System.Collections;

public class SingleModeManager : MonoBehaviour {
    public GameObject WinPanelPrefab;
    public GameObject UIRootObj;

    void Awake()
    {
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.LEVEL_PLAYER_WIN_EVENT, OnGameWin);
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.LEVEL_PLAYER_LOSE_EVENT, OnGameLose);
    }

    void Start()
    {
        //播放音乐
        GameEventDispatcher.instance.InvokeEvent(EventNameList.LEVEL_BEGIN_MUSIC_PLAY, null);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //Application.Quit();
            Application.LoadLevel("MainScene");
        }
    }

    void OnDestroy()
    {
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.LEVEL_PLAYER_WIN_EVENT, OnGameWin);
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.LEVEL_PLAYER_LOSE_EVENT, OnGameLose);
    }

    void OnGameWin(EventData eventData)
    {
        var winPanel = NGUITools.AddChild(UIRootObj, WinPanelPrefab);
        winPanel.GetComponent<WinPanelManager>().SetWin(true);
    }

    void OnGameLose(EventData eventData)
    {
        var winPanel = NGUITools.AddChild(UIRootObj, WinPanelPrefab);
        winPanel.GetComponent<WinPanelManager>().SetWin(false);
    }
}
