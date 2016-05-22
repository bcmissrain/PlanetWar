using UnityEngine;
using System.Collections.Generic;

public class TestUIInfo : MonoBehaviour {
    public Camera uiCamera;
    public GameObject troopNumLabel;
    public List<GameObject> starList = new List<GameObject>();
    public List<StarElement> starInfoList = new List<StarElement>();
    public List<UILabel> labelList = new List<UILabel>();
	void Start () {
        //var stars = StarPoolManager.starMap;
        //foreach (var value in stars.Values)
        //{
        //    var newLabel = GameObject.Instantiate(troopNumLabel) as GameObject;
        //    labelList.Add(newLabel.GetComponent<UILabel>());
        //    starList.Add(value);
        //    starInfoList.Add(value.GetComponent<StarElement>());
        //}

        //测试
        for (int i = 0; i < starList.Count; i++)
        {
            var newLabel = GameObject.Instantiate(troopNumLabel) as GameObject;
            labelList.Add(newLabel.GetComponent<UILabel>());
        }
        UpdateTitlePos();
	}
	
	void Update () {
	
	}

    void LateUpdate()
    {
        UpdateTitlePos();
    }

    void UpdateTitlePos()
    {
        for (int i = 0; i < starList.Count; i++)
        {
            labelList[i].text = starInfoList[i].m_TroopNum.ToString();
            var starPoint = starList[i].transform.position;
            var screenPoint2Title = Camera.main.WorldToScreenPoint(new Vector3(starPoint.x, starPoint.y + 1, starPoint.z));
            var resultPoint = uiCamera.ScreenToWorldPoint(new Vector3(screenPoint2Title.x, screenPoint2Title.y, 10));
            labelList[i].transform.position = resultPoint;
        }
    }
}
