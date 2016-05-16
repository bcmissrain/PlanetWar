using UnityEngine;
using System.Collections.Generic;

public class TestSendTroop : MonoBehaviour
{
    public float beginRing;
    public float endRing;
    public float ringLength;
    public float ringDepth;

    public float rotateSpeed;
    public GameObject parentStar;
    public GameObject shipPrefab;
    public int maxTroopNum;
    public List<GameObject> shipList;
    public float bornTime;
    public GameObject centerStar;
    public SendTroopToCenter sendTroopScript;
    
    private float timeCounter;

    void Start()
    {
        shipList = new List<GameObject>();
        centerStar = GameObject.Find("CenterStar");
        sendTroopScript = this.GetComponent<SendTroopToCenter>();
        sendTroopScript.centerStar = centerStar;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBorn();
        //UpdateRotate();
    }

    void UpdateBorn()
    {
        if (shipList.Count <= maxTroopNum)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= bornTime)
            {
                timeCounter = 0;

                //if (shipList.Count < 10)
                //{
                //    AddNewShip(10, shipList.Count, 0f);
                //}
                //else if (shipList.Count < 20)
                //{
                //    AddNewShip(10, shipList.Count - 20, 90f);
                //}
                //else if (shipList.Count < 30)
                //{
                //    AddNewShip(10, shipList.Count - 30, 45f);
                //}
                //else if (shipList.Count < 40)
                //{
                //    AddNewShip(10, shipList.Count - 40, 135f);
                //}
                //else
                //{
                //    SendShip(0.25f);
                //}

                if (shipList.Count < 30)
                {
                    AddNewShip(30, shipList.Count, 0f);
                }
                else
                {
                    SendShip(0.1f);
                }
            }
        }
    }

    void UpdateRotate()
    {
        for (int i = 0; i < shipList.Count; i++)
        {
            var rotateCache = shipList[i].transform.rotation;
            Vector3 rotateRef = new Vector3(0, 1, 0.5f);
            shipList[i].transform.RotateAround(parentStar.transform.position, rotateRef, rotateSpeed * Time.deltaTime);
            shipList[i].transform.rotation = rotateCache;
        }
    }

    void AddNewShip(int total, int beginIndex, float degree)
    {
        int deg = 360 / total;

        var newShip = ShipPool.GetAShip();//GameObject.Instantiate(shipPrefab) as GameObject;
        var newPos = parentStar.transform.position;

        float posX = ringLength * Mathf.Sin(Mathf.Deg2Rad * deg * beginIndex);
        float posY = ringDepth * Mathf.Cos(Mathf.Deg2Rad * deg * beginIndex);
        float posZ = ringLength * Mathf.Cos(Mathf.Deg2Rad * deg * beginIndex);

        newPos.x += posX;
        newPos.y += posY;
        newPos.z += posZ;

        newShip.transform.position = newPos;
        newShip.transform.RotateAround(parentStar.transform.position, Vector3.forward, degree);
        newShip.transform.rotation = new Quaternion();
        newShip.transform.parent = parentStar.transform;
        shipList.Add(newShip);
    }

    void SendShip(float percent)
    {
        //计算派遣数目
        int sendNum = (int)(shipList.Count * percent);
        if (sendNum == 0)
        {
            if (percent > 0 && shipList.Count > 0)
            {
                sendNum = 1;
            }
        }

        //移除派遣星球
        if (sendNum > 0)
        {
            int totalCount = shipList.Count;
            for (int i = totalCount - 1; i >= totalCount - sendNum; i--)
            {
                var delStar = shipList[i];
                sendTroopScript.shipList.Add(delStar);
                shipList.RemoveAt(i);
            }
        }
    }
}
