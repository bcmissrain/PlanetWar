using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class ScoreLabelUpdater : MonoBehaviour {
    public UILabel scoreLabel;
    public List<MasterElement> masterList = new List<MasterElement>();
    private List<string> colorList = new List<string>();
    private StringBuilder stringBuilder = new StringBuilder();
	void Start () {
        foreach (int i in MasterPoolManager.masterMap.Keys)
        {
            if (MasterPoolManager.masterMap[i].m_ControllerType != ControllerType.None)
            {
                masterList.Add(MasterPoolManager.masterMap[i]);
                colorList.Add(GetCodeByColor(MasterPoolManager.masterMap[i].m_ThemeColor));
            }
        }
	}
	
	void Update () {
        stringBuilder.Remove(0, stringBuilder.Length);
        for (int i = 0; i < masterList.Count; i++)
        {
            string code = colorList[i];
            if (code != "")
            {
                stringBuilder.Append(code);
            }
            stringBuilder.Append(masterList[i].m_ShipCount);
            if (code != "")
            {
                stringBuilder.Append("[-]");
            }
            if (i < masterList.Count - 1)
            {
                stringBuilder.Append("-");
            }
        }
        scoreLabel.text = stringBuilder.ToString();
	}

    string GetCodeByColor(string color)
    {
        string code = "";
        switch (color)
        {
            case ThemeColor.Blue:
                code = "[5f63ff]";
                break;
            case ThemeColor.Red:
                code = "[d06666]";
                break;
            case ThemeColor.Yellow:
                code = "[dcde6e]";
                break;
            case ThemeColor.Green:
                code = "[59d570]";
                break;
            case ThemeColor.Orange:
                code = "[eb9c61]";
                break;
            case ThemeColor.Black:
                code = "[000000]";
                break;
        }
        return code;
    }
}
