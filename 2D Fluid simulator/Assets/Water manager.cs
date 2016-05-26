using UnityEngine;
using System.Collections;

public class Watermanager : MonoBehaviour {
    float[] xPos;
    float[] yPos;
    float[] veloc;
    float[] accel;
    LineRenderer Body;
    GameObject[] meshobj;
    Mesh[] meshes;
    GameObject Colliders;
    const float springconst = 0.02f;
    const float damping = 0.04f;
    const float spread = 0.05f;
    const float z = -1f;
    float baseheight;
    float left;
    float bottom;
    public GameObject splash;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
