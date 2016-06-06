using UnityEngine;
using System.Collections;

public class GamePlayManager : MonoBehaviour {
    public GameObject TeachModeTile;
    public GameObject SingleModeTile;
    public GameObject MultiModeTile;

    public GameObject GameEnableObj;

    public bool IfGameEnable = false;

	void Start () {
        UpdateGameEnable();
	}
	
	void Update () {
	
	}

    public void ResetGameEnable()
    {
        IfGameEnable = !IfGameEnable;

        if (IfGameEnable)
        {
            GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_OK_EVENT, null);
        }
        else
        {
            GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_NO_EVENT, null);
        }

        UpdateGameEnable();
    }

    private void UpdateGameEnable()
    {
        if (IfGameEnable)
        {
            GameEnableObj.SetActive(true);
            TeachModeTile.SetActive(true);
            SingleModeTile.SetActive(true);
            MultiModeTile.SetActive(true);
        }
        else
        {
            GameEnableObj.SetActive(false);
            TeachModeTile.SetActive(false);
            SingleModeTile.SetActive(false);
            MultiModeTile.SetActive(false);
        }
    }

    public void OnTeachModeClick()
    {
        Application.LoadLevel("TeachModeScene");
    }

    public void OnSingleModeClick()
    {

    }

    public void OnMultiModeClick()
    {

    }
}
