using UnityEngine;
using System.Collections;

public class Watermanager : MonoBehaviour {
    //The physic array
    float[] xPos;
    float[] yPos;
    float[] veloc;
    float[] accel;

    //The renderer that'll make the top of the water visable
    LineRenderer Body;

    //Mesh and colliders.
    GameObject[] meshobj;
    Mesh[] meshes;
    GameObject[] Col;

    //our constants
    const float springconst = 0.02f;
    const float damping = 0.04f;
    const float spread = 0.05f;
    const float z = -1f;

    //the properties of the water
    float baseheight;
    float left;
    float bot;

    // Particles
    public GameObject splash;

    public Material mat;

    public GameObject watermesh;


	// Use this for initialization
	void Start () {
	
	}

    public void Spawnwater(float Left, float width, float top, float bottom)
    {
        int edgecount = Mathf.RoundToInt(width) * 5;
        int nodecount = edgecount + 1;

        Body = gameObject.AddComponent<LineRenderer>();
        Body.material = mat;
        Body.material.renderQueue = 1000;
        Body.SetVertexCount(nodecount);
        Body.SetWidth(0.1f, 0.1f);

        xPos = new float[nodecount];
        yPos = new float[nodecount];
        veloc = new float[nodecount];
        accel = new float[nodecount];

        meshobj = new GameObject[edgecount];
        meshes = new Mesh[edgecount];
        Col = new GameObject[edgecount];

        baseheight = top;
        bot = bottom;
        left = Left;

        for (int i = 0; i < nodecount; i++)
        {
            yPos[i] = top;
            xPos[i] = Left + width * i / edgecount;
            accel[i] = 0;
            veloc[i] = 0;
            Body.SetPosition(i, new Vector3(xPos[i], yPos[i], z));
        }

        for (int i = 0; i < edgecount; i++)
        {
            meshes[i] = new Mesh();

            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(xPos[i], yPos[i], z);
            Vertices[1] = new Vector3(xPos[i + 1], yPos[i + 1], z);
            Vertices[2] = new Vector3(xPos[i], bottom, z);
            Vertices[3] = new Vector3(xPos[i + 1], bottom, z);

            Vector2[] UVs = new Vector2[4];
            UVs[0] = new Vector2(0, 1);
            UVs[1] = new Vector2(1, 1);
            UVs[2] = new Vector2(0, 0);
            UVs[3] = new Vector2(1, 0);

        }


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
