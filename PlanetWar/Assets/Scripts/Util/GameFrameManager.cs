using UnityEngine;
using System.Collections;

public class GameFrameManager : MonoBehaviour {

    void Awake()
    {
        Application.targetFrameRate = SharedGameData.FrameRate;
    }

    void Start () {
	
	}
	
	void Update () {
	
	}

    public void ResetRate()
    {
        if (SharedGameData.FrameRate == 60)
        {
            SharedGameData.FrameRate = 30;
        }
        else
        {
            SharedGameData.FrameRate = 60;
        }

        if (SharedGameData.FrameRate == 60)
        {
            GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_OK_EVENT, null);
        }
        else
        {
            GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_NO_EVENT, null);
        }

        Application.targetFrameRate = SharedGameData.FrameRate;

        GameEventDispatcher.instance.InvokeEvent(EventNameList.OPTION_BUTTON_RESET_EVENT, null);
    }
}
