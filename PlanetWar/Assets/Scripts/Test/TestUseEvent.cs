using UnityEngine;
using System.Collections;

public class TestUseEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.GAME_INPUT_RELEASE_EVENT,SendTroop);
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnDestroy()
    {
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.GAME_INPUT_RELEASE_EVENT, SendTroop);
    }

    public void SendTroop(EventData eventData)
    {
        int beginIndex = eventData.intData1;
        int endIndex = eventData.intData2;

        var beginStar = StarPoolManager.instance.GetStarByIndex(beginIndex);
        var endStar = StarPoolManager.instance.GetStarByIndex(endIndex);

        beginStar.transform.localScale *= 0.75f;
        foreach (Transform child in beginStar.transform)
        {
            child.transform.localScale /= 0.75f;
        }

        endStar.transform.localScale *= 1.5f;
        foreach (Transform child in endStar.transform)
        {
            child.transform.localScale /= 1.5f;
        }
    }
}
