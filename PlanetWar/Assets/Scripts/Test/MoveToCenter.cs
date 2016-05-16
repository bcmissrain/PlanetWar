using UnityEngine;
using System.Collections;

public class MoveToCenter : MonoBehaviour {
    public AddNewShip center;
    public float moveSpeed;
    public float acc;
    public float accTime;
    private float currentSpeed;
    private float timeCounter;

    void Start () {
	
	}
	
	void Update () {
        timeCounter += Time.deltaTime;
        if (currentSpeed < moveSpeed)
        {
            if (timeCounter >= accTime)
            {
                currentSpeed += acc;
            }
        }

        if (center != null)
        {
            if ((center.transform.position - this.transform.position).magnitude > 1.2f)
            {
                var tempPos = center.transform.position - this.transform.position;
                tempPos.z = 0;
                this.transform.right = (tempPos).normalized;
                var deltaPos = this.transform.right * currentSpeed * Time.deltaTime;

                this.transform.position += deltaPos;
            }
            else
            {
                center.AddShip();
                ShipPool.ReturnShip(this.gameObject);
                //GameObject.Destroy(this.gameObject);
            }
        }
        else
        {
            ShipPool.ReturnShip(this.gameObject);
            //GameObject.Destroy(this.gameObject);
        }
    }
}
