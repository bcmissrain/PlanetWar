using UnityEngine;
using System.Collections;

public class TestInputRing : MonoBehaviour {
    public GameObject ringBegingPrefab;
    public GameObject ringEndPrefab;
    public GameObject linePrefab;

    private GameObject ringBegin;
    private GameObject ringEnd;
    private LineRenderer linkLine;

    void Start () {
        ringBegin = GameObject.Instantiate(ringBegingPrefab) as GameObject;
        ringEnd = GameObject.Instantiate(ringEndPrefab) as GameObject;
        var lineObj = GameObject.Instantiate(linePrefab) as GameObject;
        linkLine = lineObj.GetComponent<LineRenderer>();
    }
	
	void Update () {

        if (GameInputManager.starFromIndex != -1)
        {
            ringBegin.SetActive(true);
            ringBegin.transform.position = StarPoolManager.instance.GetStarByIndex(GameInputManager.starFromIndex).transform.position;
        }
        else
        {
            ringBegin.SetActive(false);
        }

        if (GameInputManager.starToIndex != -1)
        {
            ringEnd.SetActive(true);
            ringEnd.transform.position = StarPoolManager.instance.GetStarByIndex(GameInputManager.starToIndex).transform.position;
        }
        else
        {
            ringEnd.SetActive(false);
        }

        if (ringBegin.activeSelf)
        {
            linkLine.gameObject.SetActive(true);
            if (ringEnd.activeSelf)
            {
                linkLine.SetPosition(0, ringBegin.transform.position);
                linkLine.SetPosition(1, ringEnd.transform.position);
            }
            else
            {
                linkLine.SetPosition(0, ringBegin.transform.position);
                linkLine.SetPosition(1, GameInputManager.touchWorldPos);
            }
        }
        else
        {
            linkLine.gameObject.SetActive(false);
        }
    }
}
