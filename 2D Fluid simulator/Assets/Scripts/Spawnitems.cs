using UnityEngine;
using System.Collections;

public class Spawnitems : MonoBehaviour {

    public GameObject Brick;
    public GameObject Barrel;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject brick = Instantiate(Brick, Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10)), Brick.transform.rotation) as GameObject;
            brick.transform.Rotate(0, 0, UnityEngine.Random.Range(0, 0));
            brick.transform.localScale = new Vector3(UnityEngine.Random.Range(1f, 2f), 0.6f, 1) * 0.4f;
            brick.GetComponent<Rigidbody2D>().mass = brick.transform.localScale.x * brick.transform.localScale.y * 5f;
            Destroy(brick, 1.4f);
        }
        if (Input.GetMouseButton(1))
        {
            GameObject barrel = Instantiate(Barrel, Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10)), Barrel.transform.rotation) as GameObject;
            barrel.transform.Rotate(0, 0, UnityEngine.Random.Range(0, 0));
            barrel.transform.localScale = new Vector3(UnityEngine.Random.Range(1f, 2f), 0.6f, 1) * 0.4f;
            barrel.GetComponent<Rigidbody2D>().mass = barrel.transform.localScale.x * barrel.transform.localScale.y * 10f;
            Destroy(barrel, 1.4f);
        }
      
    }
}
