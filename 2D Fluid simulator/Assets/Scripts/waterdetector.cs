﻿using UnityEngine;
using System.Collections;

public class WaterDetector : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D Hit)
    {
        if (Hit.GetComponent<Rigidbody2D>() != null)
        {
            transform.parent.GetComponent<Watermanager>().Splosh(transform.position.x, Hit.GetComponent<Rigidbody2D>().velocity.y * Hit.GetComponent<Rigidbody2D>().mass / 40f);
            Debug.Log("Block hit");
        }
    }
}
