using UnityEngine;
using System.Collections;

public class Spawnitems : MonoBehaviour {

    public GameObject ToxicBarrel;
    public GameObject Barrel;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject toxicbarrel = Instantiate(ToxicBarrel, Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10)), ToxicBarrel.transform.rotation) as GameObject;
            toxicbarrel.transform.Rotate(0, 0, UnityEngine.Random.Range(0, 0));
            toxicbarrel.transform.localScale = new Vector3(UnityEngine.Random.Range(1f, 2f), 0.6f, 1) * 0.4f;
            toxicbarrel.GetComponent<Rigidbody2D>().mass = toxicbarrel.transform.localScale.x * toxicbarrel.transform.localScale.y * 5f;
            Destroy(toxicbarrel, 1.4f);
        }
        if (Input.GetMouseButtonDown(1))
        {
            GameObject barrel = Instantiate(Barrel, Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10)), Barrel.transform.rotation) as GameObject;
            barrel.transform.Rotate(0, 0, UnityEngine.Random.Range(0, 0));
            barrel.transform.localScale = new Vector3(UnityEngine.Random.Range(1f, 2f), 0.6f, 1) * 0.4f;
            barrel.GetComponent<Rigidbody2D>().mass = barrel.transform.localScale.x * barrel.transform.localScale.y * 10f;
            Destroy(barrel, 1.4f);
        }
      
    }
}
