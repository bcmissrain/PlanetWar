using UnityEngine;
using System.Collections;

public class GameInputUtil : MonoBehaviour {
    public GameObject ringBeginPrefab;
    public GameObject ringInvalidPrefab;
    public GameObject ringEndPrefab;
    public GameObject linePrefab;

    private GameObject ringBeginObj;
    private GameObject ringInvalidObj;
    private GameObject ringEndObj;
    private LineRenderer lineRen;

	void Start () {
        ringBeginObj = GameObject.Instantiate(ringBeginPrefab) as GameObject;
        ringEndObj = GameObject.Instantiate(ringEndPrefab) as GameObject;
        ringInvalidObj = GameObject.Instantiate(ringInvalidPrefab) as GameObject;
        var lineObj = GameObject.Instantiate(linePrefab) as GameObject;
        lineRen = lineObj.GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (GameInputManager.starFromIndex != -1)
        {
            ringBeginObj.SetActive(true);
            var starPos = StarPoolManager.instance.GetStarByIndex(GameInputManager.starFromIndex).transform.position;
            starPos.z -= 0.01f;
            ringBeginObj.transform.position = starPos;
        }
        else
        {
            ringBeginObj.SetActive(false);
        }

        if (GameInputManager.starToIndex != -1)
        {
            ringEndObj.SetActive(true);
            var starPos = StarPoolManager.instance.GetStarByIndex(GameInputManager.starToIndex).transform.position;
            starPos.z -= 0.01f;
            ringEndObj.transform.position = starPos;
        }
        else
        {
            ringEndObj.SetActive(false);
        }

        if (GameInputManager.starInvalid != -1)
        {
            ringInvalidObj.SetActive(true);
            var starPos = StarPoolManager.instance.GetStarByIndex(GameInputManager.starInvalid).transform.position;
            starPos.z -= 0.01f;
            ringInvalidObj.transform.position = starPos;
        }
        else
        {
            ringInvalidObj.SetActive(false);
        }

        if (ringBeginObj.activeSelf)
        {
            lineRen.gameObject.SetActive(true);
            if (ringEndObj.activeSelf)
            {
                var beginPos = ringBeginObj.transform.position;
                beginPos.z = 100f;
                var endPos = ringEndObj.transform.position;
                endPos.z = 100f;
                lineRen.SetPosition(0,beginPos);
                lineRen.SetPosition(1, endPos);
            }
            else
            {
                var beginPos = ringBeginObj.transform.position;
                beginPos.z = 100f;
                lineRen.SetPosition(0, beginPos);
                lineRen.SetPosition(1, GameInputManager.touchWorldPos);
            }
        }
        else
        {
            lineRen.gameObject.SetActive(false);
        }
    }
}
