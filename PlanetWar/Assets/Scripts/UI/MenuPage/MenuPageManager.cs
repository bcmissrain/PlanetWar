using UnityEngine;
using System.Collections;

public class MenuPageManager : MonoBehaviour {
    public GameObject AboutPanelPrefab;
    public GameObject AudioSourcePrefab;
    public GameObject UIRootObj;

    void Awake()
    {
        if (GameObject.Find("AudioMusicSource") == null)
        {
            GameObject audioSourceObj = GameObject.Instantiate(AudioSourcePrefab) as GameObject;
            audioSourceObj.name = "AudioMusicSource";
        }
    }

	void Start () {
        //播放音乐
        GameEventDispatcher.instance.InvokeEvent(EventNameList.MENU_BEGIN_MUSIC_PLAY, null);
	}
	
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            OnBackButtonClick();
        }
	}

    public void OnBackButtonClick()
    {
        Application.Quit();
    }

    //临时
    public void OnDisableButtonClick()
    {
        GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_NO_EVENT, null);
    }

    public void OnAboutButtonClick()
    {
        GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_OK_EVENT, null);
        NGUITools.AddChild(UIRootObj,AboutPanelPrefab);
    }

    public void OnMusicRestButtonClick()
    {
        GameEventDispatcher.instance.InvokeEvent(EventNameList.MUSIC_RESET_EVENT, null);
    }

    public void OnSoundResetButtonClick()
    {
        GameEventDispatcher.instance.InvokeEvent(EventNameList.SOUND_RESET_EVENT, null);
    }
}
