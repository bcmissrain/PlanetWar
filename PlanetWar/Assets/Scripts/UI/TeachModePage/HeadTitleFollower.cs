using UnityEngine;
using System.Collections.Generic;

public class HeadTitleFollower : MonoBehaviour {
    public Camera uiCamera;
    public GameObject labelPrefab;
    public Dictionary<int, UILabel> labelMap = new Dictionary<int, UILabel>();
	void Start () {
        foreach (int i in StarPoolManager.starMap.Keys)
        {
            var titleLabel = GameObject.Instantiate(labelPrefab) as GameObject;
            labelMap.Add(i, titleLabel.GetComponent<UILabel>());
        }

        UpdateTitlePos();
	}
	
	void Update () {
        UpdateTitlePos();
    }

    void UpdateTitlePos()
    {
        foreach (int i in StarPoolManager.starMap.Keys)
        {
            var starElement = StarPoolManager.starMap[i].GetComponent<StarElement>();
            var titleLabel = labelMap[i];
            if (titleLabel)
            {
                titleLabel.text = starElement.m_TroopNum+"/"+starElement.m_MaxTroop;
                var starPos = starElement.transform.position;
                starPos.y += 1;
                var screenPointToTitle = Camera.main.WorldToScreenPoint(starPos);
                screenPointToTitle.z = 10;
                var resultPos = uiCamera.ScreenToWorldPoint(screenPointToTitle);
                titleLabel.transform.position = resultPos;
            }
        }
    }
}
