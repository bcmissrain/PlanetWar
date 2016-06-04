using UnityEngine;
using System.Collections;

public class TestChangePic : MonoBehaviour {
    public GameObject gameObj;
    public Material originMaterial;
    public Material tempMaterial;

    private int counter;
	void Start () {
	    
	}
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            counter++;

            if (counter % 2 == 0)
            {
                gameObj.renderer.material = originMaterial;
            }
            else
            {
                gameObj.renderer.material = tempMaterial;
            }
        }
	}
}
