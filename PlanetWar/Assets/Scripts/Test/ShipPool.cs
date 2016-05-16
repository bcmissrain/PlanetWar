using UnityEngine;
using System.Collections.Generic;

public class ShipPool : MonoBehaviour {
    public GameObject shipPrefab;
    public static GameObject staticShipPrefab;
    public static List<GameObject> shipList = new List<GameObject>();
	// Use this for initialization
	void Start () {
        staticShipPrefab = shipPrefab;
        for (int i = 0; i < 1000; i++)
        {
            var ship = GameObject.Instantiate(shipPrefab) as GameObject;
            ship.SetActive(false);
            //ship.transform.position = new Vector3(20000, 20000, 20000);
            shipList.Add(ship);
        }
	}

    public static GameObject GetAShip()
    {
        for (int i = 0; i < shipList.Count; i++)
        {
            if(!shipList[i].activeSelf)
            //if (shipList[i].transform.position.x > 10000)
            {
                shipList[i].SetActive(true);
                //shipList[i].transform.position = new Vector3(10000, 10000, 10000);
                return shipList[i];
            }
        }

        var ship = GameObject.Instantiate(staticShipPrefab) as GameObject;
        ship.transform.position = new Vector3(20000, 20000, 20000);
        shipList.Add(ship);
        return ship;
    }

    public static void ReturnShip(GameObject ship)
    {
        ship.transform.localScale = staticShipPrefab.transform.localScale;
        ship.transform.parent = null;
        ship.transform.rotation = new Quaternion();
        var moveScript = ship.GetComponent<MoveToCenter>();
        if (moveScript)
        {
            moveScript.enabled = false;
        }

        ship.SetActive(false);
        //ship.transform.position = new Vector3(20000, 20000, 20000);
    }
}
