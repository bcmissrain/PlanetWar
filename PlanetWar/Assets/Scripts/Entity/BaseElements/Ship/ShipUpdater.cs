﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipElement))]
[RequireComponent(typeof(ShipMover))]
public class ShipUpdater : MonoBehaviour {
    public ShipElement shipElement;
    public ShipMover shipMover;

	void Start () {
    
    }
	
	void Update () {
        shipElement._Update();
    }

    void OnDestroy()
    {

    }
}
