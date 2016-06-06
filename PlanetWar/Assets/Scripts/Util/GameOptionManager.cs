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

    public static bool OptionEnable = false;

    void Awake()
    {
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
        OptionEnable = !OptionEnable;
        //控制音效
        if (OptionEnable)
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
        if (OptionEnable)
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

        if (OptionEnable)
        {
            optionEnableObj.SetActive(true);
        }
        else
        {
            optionEnableObj.SetActive(false);
        }

        if (GameMusicController.MusicEnable)
        {
            musicEnableObj.SetActive(true);
        }
        else
        {
            musicEnableObj.SetActive(false);
        }

        if (GameMusicController.SoundEnable)
        {
            soundEnableObj.SetActive(true);
        }
        else
        {
            soundEnableObj.SetActive(false);
        }

        if (GameFrameManager.FrameRate == 60)
        {
            qualityEnableObj.SetActive(true);
        }
        else
        {
            qualityEnableObj.SetActive(false);
        }
    }
}
