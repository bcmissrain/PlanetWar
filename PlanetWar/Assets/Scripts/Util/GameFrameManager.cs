using UnityEngine;
using System.Collections;

public class GameFrameManager : MonoBehaviour {

    public static int FrameRate = 60;

    void Awake()
    {
        Application.targetFrameRate = FrameRate;
    }

    void Start () {
	
	}
	
	void Update () {
	
	}

    public void ResetRate()
    {
        if (FrameRate == 60)
        {
            FrameRate = 30;
        }
        else
        {
            FrameRate = 60;
        }

        if (FrameRate == 60)
        {
            GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_OK_EVENT, null);
        }
        else
        {
            GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_NO_EVENT, null);
        }

        Application.targetFrameRate = FrameRate;

        GameEventDispatcher.instance.InvokeEvent(EventNameList.OPTION_BUTTON_RESET_EVENT, null);
    }
}
