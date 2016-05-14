using UnityEngine;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

/// <summary>
/// 尝试使用XML配置文件
/// </summary>
public class UseXML : MonoBehaviour
{

    public GameObject starPrefab;
    public List<GameObject> starList = new List<GameObject>();

    string showStr = "";

    void Start()
    {
        //注意不要后缀名
        TextAsset textAsset = (TextAsset)Resources.Load("Test/testStarConfig", typeof(TextAsset));
   
        if (textAsset != null)
        {
            showStr = textAsset.text;

            XDocument xDox = XDocument.Parse(textAsset.text);
            XElement root = xDox.Root;

            //读取星球信息
            var stars = from item in root.Elements()
                        select new
                        {
                            id = int.Parse(item.Element("id").Value),
                            rotateSpeed = float.Parse(item.Element("rotate").Value),
                            fatherId = int.Parse(item.Element("father").Value),
                            position = new Vector3(float.Parse(item.Element("pos_x").Value), float.Parse(item.Element("pos_y").Value), float.Parse(item.Element("pos_z").Value))
                        };

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
