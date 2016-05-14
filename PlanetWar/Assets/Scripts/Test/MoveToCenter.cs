using UnityEngine;
using System.Collections;

public class MoveToCenter : MonoBehaviour {
    public GameObject center;
    public float moveSpeed;
  
    void Start () {
	
	}
	
	void Update () {
        if (center != null)
        {
            if ((center.transform.position - this.transform.position).magnitude > 1.2f)
            {
                var tempPos = center.transform.position - this.transform.position;
                tempPos.z = 0;
                this.transform.right = (tempPos).normalized;
                var deltaPos = this.transform.right * moveSpeed * Time.deltaTime;

                this.transform.position += deltaPos;
            }
            else
            {
                GameObject.Destroy(this.gameObject);
            }
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
