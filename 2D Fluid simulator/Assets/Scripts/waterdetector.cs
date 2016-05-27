using UnityEngine;
using System.Collections;

public class WaterDetector : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2d(Collider2D Hit)
    {
        if (Hit.GetComponent<Rigidbody2D>() != null)
        {
            transform.parent.GetComponent<Watermanager>().Splosh(transform.position.x, Hit.GetComponent<Rigidbody2D>().velocity.y * Hit.GetComponent<Rigidbody2D>().mass / 40f);
        }
    }
}
