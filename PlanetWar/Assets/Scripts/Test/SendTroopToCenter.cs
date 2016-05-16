using UnityEngine;
using System.Collections.Generic;

public class SendTroopToCenter : MonoBehaviour {
    public List<GameObject> shipList;
    public GameObject centerStar;

    public float sendSpeed;
    private float sendCounter;

    void Start () {
	    
	}
	
	void Update () {
        sendCounter += Time.deltaTime;
        if (sendCounter >= sendSpeed)
        {
            sendCounter = 0;

            if (shipList.Count > 0)
            {
                var delStar = shipList[shipList.Count - 1];
                delStar.transform.parent = null;
                var tempScale = delStar.transform.localScale;
                tempScale.x *= 2;
                delStar.transform.localScale = tempScale;
                var moveScript = delStar.GetComponent<MoveToCenter>();

                if (moveScript != null)
                {
                    moveScript.center = centerStar.GetComponent<AddNewShip>();
                    moveScript.enabled = true;
                }

                delStar.transform.position = GetShootPoint(centerStar.transform.position);

                //var trailScript = delStar.GetComponent<TrailRenderer>();
                //if (trailScript)
                //{
                //    trailScript.enabled = true;
                //}

                shipList.RemoveAt(shipList.Count - 1);
            }
        }
    }

    public Vector3 GetShootPoint(Vector3 targetPos)
    {
        var deltaPos = this.transform.position - targetPos;
        deltaPos.z = 0;
        return this.transform.position - deltaPos.normalized ;
    }
}
