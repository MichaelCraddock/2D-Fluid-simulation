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
    float bottom;

    // Particles
    public GameObject splash;

    public Material mat;

    public GameObject watermesh;


	// Use this for initialization
	void Start () {
        Spawnwater(-10, 20, 0, -3);
	}

    public void Splosh(float xposi, float velocity)
    {
        if (xposi >= xPos[0] && xposi <= xPos[xPos.Length - 1])
        {
            xposi -= xPos[0];
            int index = Mathf.RoundToInt((xPos.Length - 1) * (xposi / (xPos[xPos.Length - 1] - xPos[0])));
            veloc[index] = velocity;

            float lifetime = 0.93f + Mathf.Abs(velocity) * 0.07f;
            splash.GetComponent<ParticleSystem>().startSpeed = 8 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
            splash.GetComponent<ParticleSystem>().startSpeed = 9 + 2 * Mathf.Pow(Mathf.Abs(velocity), 0.5f);
            splash.GetComponent<ParticleSystem>().startLifetime = lifetime;

            Vector3 position = new Vector3(xPos[index], yPos[index] - 0.3f, 5);
            Quaternion rotation = Quaternion.LookRotation(new Vector3(xPos[Mathf.FloorToInt(xPos.Length / 2)], baseheight + 8, 5) - position);

            GameObject splish = Instantiate(splash, position, rotation) as GameObject;
            Destroy(splish, lifetime + 0.3f);
        }

    } 

    public void Spawnwater(float Left, float width, float top, float Bottom)
{
        gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(Left + width / 2, (top + Bottom) / 2);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(width, top - Bottom);
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

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
        bottom = Bottom;
        left = Left;

        for (int i = 0; i < nodecount; i++)
        {
            yPos[i] = top;
            xPos[i] = Left + width * i / edgecount;
            accel[i] = 0;
            veloc[i] = 0;
            Body.SetPosition(i, new Vector3(xPos[i], top, z));
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

            int[] tris = new int[6] { 0, 1, 3, 3, 2, 0 };

            meshes[i].vertices = Vertices;
            meshes[i].uv = UVs;
            meshes[i].triangles = tris;

            meshobj[i] = Instantiate(watermesh, Vector3.zero, Quaternion.identity) as GameObject;
            meshobj[i].GetComponent<MeshFilter>().mesh = meshes[i];
            meshobj[i].transform.parent = transform;

            Col[i] = new GameObject();
            Col[i].name = "Trigger";
            Col[i].AddComponent<BoxCollider2D>();
            Col[i].transform.parent = transform;

            Col[i].transform.position = new Vector3(Left + width * (i + 0.5f) / edgecount, top - 0.5f, 0);
            Col[i].transform.localScale = new Vector3(width / edgecount, 1, 1);

            Col[i].GetComponent<BoxCollider2D>().isTrigger = true;
            Col[i].AddComponent<WaterDetector>();
            


        }

    }
	
	
	void Updatemesh () {
        for (int i = 0; i < meshes.Length; i++)
        {
            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(xPos[i], yPos[i], z);
            Vertices[1] = new Vector3(xPos[i + 1], yPos[i + 1], z);
            Vertices[2] = new Vector3(xPos[i], bottom, z);
            Vertices[3] = new Vector3(xPos[i + 1], bottom, z);

            meshes[i].vertices = Vertices;
        }
	
	}

    void Fixedupdate()
    {
        for (int i = 0; i < xPos.Length; i++)
        {
            float force = springconst * (yPos[i] - baseheight) + veloc[i] * damping;
            accel[i] = -force;
            yPos[i] += veloc[i];
            veloc[i] += accel[i];
            Body.SetPosition(i, new Vector3(xPos[i], yPos[i], z));
        }


        float[] leftDeltas = new float[xPos.Length];
        float[] rightDeltas = new float[xPos.Length];


        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < xPos.Length; i++)
            {
                if (i > 0)
                {
                    leftDeltas[i] = spread * (yPos[i] - yPos[i - 1]);
                    veloc[i - 1] += leftDeltas[i];
                }
                if (i < xPos.Length - 1)
                {
                    rightDeltas[i] = spread * (yPos[i] - yPos[i + 1]);
                    veloc[i + 1] += rightDeltas[i];
                }
            }
            for (int i = 0; i < xPos.Length; i++)
            {
                if (i > 0)
                {
                    yPos[i - 1] += leftDeltas[i];
                }
                if ( i < xPos.Length - 1)
                {
                    yPos[i + 1] += rightDeltas[i];
                }
            }
        }
        Updatemesh();

    }
}
