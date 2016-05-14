using UnityEngine;
using System.Collections.Generic;

public class TestBornDiffShip : MonoBehaviour
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
    private float timeCounter;

    void Start()
    {
        shipList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBorn();
        //UpdateRotate();
    }

    void UpdateBorn()
    {
        if (shipList.Count < maxTroopNum)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= bornTime)
            {
                timeCounter = 0;
                if (shipList.Count < 30)
                {
                    AddNewStar(30, shipList.Count, 0f);
                }
                else if (shipList.Count < 60)
                {
                    AddNewStar(30, shipList.Count - 30, 90f);
                }
                else if (shipList.Count < 90)
                {
                    AddNewStar(30, shipList.Count - 60, 45f);
                }
                else
                {
                    AddNewStar(30, shipList.Count - 90, 135f);
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

    void AddNewStar(int total, int beginIndex, float degree)
    {
        int deg = 360 / total;

        var newShip = GameObject.Instantiate(shipPrefab) as GameObject;
        var newPos = parentStar.transform.position;

        float posX = ringLength * Mathf.Sin(Mathf.Deg2Rad * deg * beginIndex);
        float posY = ringDepth * Mathf.Cos(Mathf.Deg2Rad * deg * beginIndex);
        float posZ = ringLength * Mathf.Cos(Mathf.Deg2Rad * deg * beginIndex);

        newPos.x += posX;
        newPos.y += posY;
        newPos.z += posZ;
        
        newShip.transform.position = newPos;
        newShip.transform.RotateAround(parentStar.transform.position,Vector3.forward,degree);
        newShip.transform.rotation = new Quaternion();
        newShip.transform.parent = parentStar.transform;
        shipList.Add(newShip);
    }
}
