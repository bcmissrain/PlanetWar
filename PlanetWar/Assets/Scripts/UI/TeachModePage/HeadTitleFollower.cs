using UnityEngine;
using System.Collections.Generic;

public class HeadTitleFollower : MonoBehaviour {
    public Camera uiCamera;
    public GameObject troopLabelPrefab;
	public GameObject transLabelPrefab;

	private UILabel transLabel;

    public Dictionary<int, UILabel> labelMap = new Dictionary<int, UILabel>();
	void Start () {
        foreach (int i in StarPoolManager.starMap.Keys)
        {
            var titleLabel = GameObject.Instantiate(troopLabelPrefab) as GameObject;
            labelMap.Add(i, titleLabel.GetComponent<UILabel>());
        }
		transLabel = (GameObject.Instantiate (transLabelPrefab) as GameObject).GetComponent<UILabel>();
		transLabel.gameObject.SetActive (false);

        UpdateTitlePos();
	}
	
	void Update () {
        UpdateTitlePos();
		UpdateTransLabel ();
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

	void UpdateTransLabel()
	{
		if (transLabel) 
		{
			var beginPos = GameInputManager.touchWorldPos;
			beginPos.x += 1;
			beginPos.y += 1;
			var screenPointToTitle = Camera.main.WorldToScreenPoint(beginPos);
			var resultPos = uiCamera.ScreenToWorldPoint(screenPointToTitle);

			if (GameInputManager.starFromIndex != -1)
			{
				var shipCount = StarPoolManager.instance.GetStarByIndex(GameInputManager.starFromIndex).GetComponent<StarElement>().shipSender.shipList.Count;

				//计算派遣兵力
				float percent = SharedGameData.TroopSendPercent;
				//容错
				if (percent > 1.0f)
				{
					percent = 1.0f;
				}
				//计算派遣数目
				int sendNum = (int)(shipCount * percent);
				if (sendNum == 0)
				{
					//如果有飞船且计算的值为0，则置为1
					if (percent > 0 && shipCount > 0)
					{
						sendNum = 1;
					}
				}

				transLabel.transform.position = resultPos;
				transLabel.text = sendNum.ToString();
				if(!transLabel.gameObject.activeSelf)
				{
					transLabel.gameObject.SetActive(true);
				}
			}
			else
			{
				if(transLabel.gameObject.activeSelf)
				{
					transLabel.gameObject.SetActive(false);
				}
			}
		}
	}
}
