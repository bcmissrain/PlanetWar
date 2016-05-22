using UnityEngine;
using System.Collections;

public class TestCollideEffect : MonoBehaviour {
    public GameObject effPrefab;

	void Start () {
        GameEventDispatcher.instance.RegistEventHandler(EventNameList.LEVEL_SHIP_BOOM_EVENT,OnShipDestroy);
	}
	
	void Update () {
        
	}

    void OnDestroy()
    {
        GameEventDispatcher.instance.RemoveEventHandler(EventNameList.LEVEL_SHIP_BOOM_EVENT, OnShipDestroy);
    }

    void OnShipDestroy(EventData eventData)
    {
        if (effPrefab)
        {
            if (eventData.intData1 == -1)
            {
                var effObj = GameObject.Instantiate(effPrefab) as GameObject;
                effObj.transform.position = (Vector3)eventData.objData1;
            }
        }        
    }
}
