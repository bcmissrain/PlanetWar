using UnityEngine;
using System.Xml;
using System.Collections.Generic;

/// <summary>
/// 尝试使用XML配置文件
/// </summary>
public class UseXML : MonoBehaviour
{
    class TempStar
    {
        public int id;
        public float rotateSpeed;
        public int fatherId;
        public Vector3 position;
    }

    public GameObject starPrefab;
    public List<GameObject> starList = new List<GameObject>();

    string showStr = "";

    void Start()
    {
        StarPoolManager.instance.InitManager();
        //注意不要后缀名
        TextAsset textAsset = (TextAsset)Resources.Load("Test/testStarConfig", typeof(TextAsset));
   
        if (textAsset != null)
        {
            showStr = textAsset.text;
            XmlDocument xDox = new XmlDocument();
            xDox.LoadXml(textAsset.text);
            XmlNode xRoot = xDox.SelectSingleNode("starList");

            List<TempStar> stars = new List<TempStar>();

            //读取星球信息
            foreach (XmlElement item in xRoot.ChildNodes)
            {
                TempStar s = new TempStar();
                s.id = int.Parse(item.SelectSingleNode("id").InnerText);
                s.rotateSpeed = float.Parse(item.SelectSingleNode("rotate").InnerText);
                s.fatherId = int.Parse(item.SelectSingleNode("father").InnerText);
                s.position = new Vector3(float.Parse(item.SelectSingleNode("pos_x").InnerText), float.Parse(item.SelectSingleNode("pos_y").InnerText), float.Parse(item.SelectSingleNode("pos_z").InnerText));
                stars.Add(s);
            }


            //组装星球
            foreach (var item in stars)
            {
                if (starPrefab)
                {
                    var newStar = GameObject.Instantiate(starPrefab) as GameObject;
                    newStar.name = item.id + "";

                    newStar.transform.position = item.position;

                    if (item.fatherId > 0)
                    {
                        var fatherStar = GameObject.Find(item.fatherId.ToString()) as GameObject;
                        if (fatherStar)
                        {
                            TestAutoRotate rotateComponent = newStar.GetComponent<TestAutoRotate>();
                            newStar.transform.parent = fatherStar.transform;
                            newStar.transform.localPosition = item.position;
                            if (rotateComponent)
                            {
                                rotateComponent.center = fatherStar.transform;
                                rotateComponent.rotateSpeed = item.rotateSpeed;
                            }
                        }
                    }
                    StarPoolManager.instance.CacheStar(item.id, newStar);
                    starList.Add(newStar);
                }
            }
        }
    }

    void OnGUI()
    {
        GUILayout.Label(showStr);
    }
}
