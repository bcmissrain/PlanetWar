using UnityEngine;
using System.Collections.Generic;

public class TestAutoBornShip : MonoBehaviour {
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

	void Start () {
        shipList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
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
                    AddNewStar(30,shipList.Count,1.0f);
                }
                else if(shipList.Count < 90)
                {
                    AddNewStar(60,shipList.Count - 30,1.5f);
                }
                else
                {
                    AddNewStar(120, shipList.Count - 30, 2.0f);
                }
            }
        }
    }

    void UpdateRotate()
    {
        for (int i = 0; i < shipList.Count; i++)
        {
            var rotateCache = shipList[i].transform.rotation;
            Vector3 rotateRef = new Vector3(0,1,0.5f);
            shipList[i].transform.RotateAround(parentStar.transform.position, rotateRef, rotateSpeed * Time.deltaTime);
            shipList[i].transform.rotation = rotateCache;
        }
    }

    void AddNewStar(int total,int beginIndex,float scale)
    {
        int deg = 360 / total;
  
        var newShip = GameObject.Instantiate(shipPrefab) as GameObject;
        var newPos = parentStar.transform.position;

        float posX = ringLength * Mathf.Sin(Mathf.Deg2Rad * deg * beginIndex) * scale;
        float posY = ringDepth * Mathf.Cos(Mathf.Deg2Rad * deg * beginIndex) * scale;
        float posZ = ringLength * Mathf.Cos(Mathf.Deg2Rad * deg * beginIndex) * scale;

        newPos.x += posX;
        newPos.y += posY;
        newPos.z += posZ;

        newShip.transform.position = newPos;
        newShip.transform.parent = parentStar.transform;
        shipList.Add(newShip);
    }
}
