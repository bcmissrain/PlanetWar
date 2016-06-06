using UnityEngine;
using System.Collections;

public class WinPanelManager : MonoBehaviour {
    public UIPlayTween playController;
    public GameObject winLabel;
    public GameObject loseLabel;

    private bool ifClicked = false;

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnBackClick()
    {
        if (!ifClicked)
        {
            ifClicked = true;
            GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_NO_EVENT, null);
            //playController.Play(true);
            //Application.LoadLevel("MainScene");
            Application.Quit();
            AutoRemove();
        }
    }

    public void AutoRemove()
    {
        Destroy(this.gameObject);
    }

    public void SetWin(bool ifWin)
    {
        if (ifWin)
        {
            winLabel.SetActive(true);
            loseLabel.SetActive(false);
        }
        else
        {
            winLabel.SetActive(false);
            loseLabel.SetActive(true);
        }
    }
}
