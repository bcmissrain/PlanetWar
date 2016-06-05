using UnityEngine;
using System.Collections;
using System.Xml;

public class GameLevelManager : MonoBehaviour {
    public GameObject masterPrefab;
    public GameObject starPrefab;
    public GameObject shipPrefab;

    void Awake()
    {
        MasterPoolManager.instance.InitManager();
        ShipPoolManager.instance.InitManager(shipPrefab);
        StarPoolManager.instance.InitManager();
    }

	void Start () {
        LoadLevelByFile("Test/level0-0");
    }

    void Update () {
        IfGameWin();
    }

    void OnDestroy()
    {
        ShipPoolManager.instance.ReleaseManager();
        StarPoolManager.instance.ReleaseManager();
        MasterPoolManager.instance.ReleaseManager();
    }

    bool IfGameWin()
    {
        var masterMap = MasterPoolManager.masterMap;
        foreach (var item in masterMap.Values)
        {
            if (item.m_ControllerType == ControllerType.Computer)
            {
                if (!item.masterUpdater.IfLoseGame())
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void LoadLevelByFile(string name)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(name, typeof(TextAsset));
        if (textAsset)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(textAsset.text);
            XmlNode xRoot = xDoc.SelectSingleNode("level");
            XmlNode xLevel = xRoot.SelectSingleNode("levelDefine");
            XmlNode xMasters = xRoot.SelectSingleNode("masterList");
            XmlNode xStars = xRoot.SelectSingleNode("starList");

            //生成Master
            foreach (XmlElement master in xMasters.ChildNodes)
            {
                var masterObj = GameObject.Instantiate(masterPrefab) as GameObject;
                masterObj.transform.parent = this.transform;
                var elementScript = masterObj.GetComponent<MasterElement>();
                var updaterScript = masterObj.GetComponent<MasterUpdater>();

                masterObj.name = master.SelectSingleNode("masterName").InnerText;
                elementScript.m_Index = int.Parse(master.SelectSingleNode("masterId").InnerText);
                elementScript.m_EnemyIndex = int.Parse(master.SelectSingleNode("enemyId").InnerText);
                elementScript.m_ThemeColor = master.SelectSingleNode("theme").InnerText;
                elementScript.m_ControllerType = master.SelectSingleNode("controllerType").InnerText;

                updaterScript.masterType = master.SelectSingleNode("aiType").InnerText;
                updaterScript.thinkTime = float.Parse(master.SelectSingleNode("thinkTime").InnerText);

                //注册到缓冲池
                MasterPoolManager.instance.AddMasterByIndex(elementScript.m_Index, elementScript);
            }

            //生成Star
            foreach (XmlElement star in xStars.ChildNodes)
            {
                var starObj = GameObject.Instantiate(starPrefab) as GameObject;
                var elementScript = starObj.GetComponent<StarElement>();
                var updaterScript = starObj.GetComponent<StarUpdater>();
                var senderScript = starObj.GetComponent<ShipSender>();
                var materialScript = starObj.GetComponent<StarMaterial>();

                starObj.name = star.SelectSingleNode("starDes").InnerText;
                var xPos = star.SelectSingleNode("position");
                float posX = int.Parse(xPos.SelectSingleNode("x").InnerText);
                float posY = int.Parse(xPos.SelectSingleNode("y").InnerText);
                float posZ = int.Parse(xPos.SelectSingleNode("z").InnerText);
                starObj.transform.position = new Vector3(posX, posY, posZ);

                elementScript.m_StarType = StarType.TroopStar;
                elementScript.m_Index = int.Parse(star.SelectSingleNode("starId").InnerText);
                elementScript.m_MasterIndex = int.Parse(star.SelectSingleNode("masterId").InnerText);
                elementScript.m_StarType = star.SelectSingleNode("starType").InnerText;
                elementScript.m_StarLevel = int.Parse(star.SelectSingleNode("starLevel").InnerText);
                elementScript.m_MaxTroop = int.Parse(star.SelectSingleNode("maxTroop").InnerText);
                elementScript.m_StartTroopNum = int.Parse(star.SelectSingleNode("startTroopNum").InnerText);
                elementScript.m_BornTime = float.Parse(star.SelectSingleNode("bornTime").InnerText);
                elementScript.m_BornNum = int.Parse(star.SelectSingleNode("bornNum").InnerText);
                elementScript.m_DetectScope = float.Parse(star.SelectSingleNode("detectScope").InnerText);
                elementScript.m_ShipFlySpeed = float.Parse(star.SelectSingleNode("shipFlySpeed").InnerText);
                elementScript.m_ShipShowType = star.SelectSingleNode("shipShowType").InnerText;

                senderScript.ringLength = float.Parse(star.SelectSingleNode("ringLength").InnerText);
                senderScript.ringDepth = float.Parse(star.SelectSingleNode("ringDepth").InnerText);
                senderScript.ringAddition = float.Parse(star.SelectSingleNode("ringAddition").InnerText);
                senderScript.sendShipTime = float.Parse(star.SelectSingleNode("sendShipTime").InnerText);
                senderScript.sendShipNum = int.Parse(star.SelectSingleNode("sendShipNum").InnerText);

                updaterScript.thinkTime = float.Parse(star.SelectSingleNode("thinkTime").InnerText);

                //注册到缓冲池中
                StarPoolManager.instance.CacheStar(elementScript.m_Index, starObj);

                var master = MasterPoolManager.instance.GetMasterByIndex(elementScript.m_MasterIndex);
                var starMat = materialScript.GetMaterialByStar(master.m_ThemeColor);
                if (starMat)
                {
                    starObj.renderer.material = starMat;
                }
                master.AddStarElement(elementScript);
            }
        }
    }
}
