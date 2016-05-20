using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipElement))]
public class ShipMover : MonoBehaviour {
    public ShipElement shipElement;

    public void Start()
    {
        shipElement._Init();
    }

    public void Update()
    {
        shipElement._Update();
    }
}
