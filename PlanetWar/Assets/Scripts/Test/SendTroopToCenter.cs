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
                    moveScript.center = centerStar;
                    moveScript.enabled = true;
                }

                //var trailScript = delStar.GetComponent<TrailRenderer>();
                //if (trailScript)
                //{
                //    trailScript.enabled = true;
                //}

                shipList.RemoveAt(shipList.Count - 1);
            }
        }
    }
}
