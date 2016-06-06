using UnityEngine;
using System.Collections;

public class GameOptionManager : MonoBehaviour {
    public GameObject optionEnableObj;
    public GameObject musicEnableObj;
    public GameObject soundEnableObj;
    public GameObject qualityEnableObj;

    public GameObject musicButton;
    public GameObject soundButton;
    public GameObject qualityButton;

    void Awake()
    {
        SharedGameData.OptionEnable = false;
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.OPTION_BUTTON_RESET_EVENT, ResetEnableButtons);
    }

	void Start () {
        ResetEnableButtons(null);
    }
	
	void Update () {
	
	}

    void OnDestroy()
    {
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.OPTION_BUTTON_RESET_EVENT, ResetEnableButtons);
    }

    public void ResetOptionButton()
    {
        SharedGameData.OptionEnable = !SharedGameData.OptionEnable;
        //控制音效
        if (SharedGameData.OptionEnable)
        {
            GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_OK_EVENT, null);
        }
        else
        {
            GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_NO_EVENT, null);
        }
        GameEventDispatcher.instance.InvokeEvent(EventNameList.OPTION_BUTTON_RESET_EVENT, null);
    }

    public void ResetEnableButtons(EventData eventData)
    {
        //不知道发生了什么的bug
        if (musicButton == null)
        {
            return;
        }

        if (SharedGameData.OptionEnable)
        {
            musicButton.SetActive(true);
            soundButton.SetActive(true);
            qualityButton.SetActive(true);
        }
        else
        {
            musicButton.SetActive(false);
            soundButton.SetActive(false);
            qualityButton.SetActive(false);
        }

        if (SharedGameData.OptionEnable)
        {
            optionEnableObj.SetActive(true);
        }
        else
        {
            optionEnableObj.SetActive(false);
        }

        if (SharedGameData.MusicEnable)
        {
            musicEnableObj.SetActive(true);
        }
        else
        {
            musicEnableObj.SetActive(false);
        }

        if (SharedGameData.SoundEnable)
        {
            soundEnableObj.SetActive(true);
        }
        else
        {
            soundEnableObj.SetActive(false);
        }

        if (SharedGameData.FrameRate == 60)
        {
            qualityEnableObj.SetActive(true);
        }
        else
        {
            qualityEnableObj.SetActive(false);
        }
    }
}
