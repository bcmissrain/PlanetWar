using UnityEngine;
using System.Collections;

public class AboutPanelController : MonoBehaviour {
    public UIPlayTween playController;
    private bool ifClicked = false;
	void Start () {
	
	}
	
	void Update () {
	
	}

    public void OnBackClick()
    {
        if (!ifClicked)
        {
            ifClicked = true;
            GameEventDispatcher.instance.InvokeEvent(EventNameList.BUTTON_CLICK_NO_EVENT, null);
            playController.Play(true);
        }
    }

    public void AutoRemove()
    {
        Destroy(this.gameObject);
    }
}
