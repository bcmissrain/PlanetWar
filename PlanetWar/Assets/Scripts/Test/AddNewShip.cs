using UnityEngine;
using System.Collections.Generic;

public class AddNewShip : MonoBehaviour {
    public float beginRing;
    public float endRing;
    public float ringLength;
    public float ringDepth;

    public float rotateSpeed;
    public GameObject parentStar;
    public int maxTroopNum;
    public List<GameObject> shipList;

    public void AddShip()
    {
        if (shipList.Count < 30)
        {
            AddShip(30, shipList.Count, 30.0f);
        }
        else if (shipList.Count < 60)
        {
            AddShip(30, shipList.Count, 150, 0f);
        }
        else if (shipList.Count < 90)
        {
            AddShip(30, shipList.Count, 270.0f);
        }
        else
        {
            AddShip(30, shipList.Count, 30.0f,2);
        }
    }

    void AddShip(int total, int beginIndex, float degree,float scale = 1)
    {
        int deg = 360 / total;

        var newShip = ShipPool.GetAShip();
            //GameObject.Instantiate(shipPrefab) as GameObject;
        var newPos = parentStar.transform.position;

        float posX = ringLength * Mathf.Sin(Mathf.Deg2Rad * deg * beginIndex) * scale;
        float posY = ringDepth * Mathf.Cos(Mathf.Deg2Rad * deg * beginIndex) * scale;
        float posZ = ringLength * Mathf.Cos(Mathf.Deg2Rad * deg * beginIndex) * scale;

        newPos.x += posX;
        newPos.y += posY;
        newPos.z += posZ;

        newShip.transform.position = newPos;
        newShip.transform.RotateAround(parentStar.transform.position, Vector3.forward, degree);
        newShip.transform.rotation = new Quaternion();
        newShip.transform.parent = parentStar.transform;
        shipList.Add(newShip);
    }
}
