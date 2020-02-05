using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    private RaycastHit hit;
    private float maxDistance = 110f;
    private float timer = 3.0f;
    private GameObject prev;
    public Text timeStr;
    public float alpha = 3.0f;
   
    MovingEnemy m = null;
    //Monster flower = null;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.red, 0.3f);
            if (hit.transform.gameObject.tag == "Enemy")
                Destroy(hit.transform.gameObject);

            if (hit.transform.gameObject.tag == "MEnemy")
            {
                m = hit.transform.GetComponent<MovingEnemy>();
                m.createHPBar();
                if (!m.getIsDie()) timer -= Time.deltaTime;
                else timer = 2.0f;
                timeStr.text = Mathf.Round(timer * 100) / 100 + "s";
            }
            else
            {
                if (m != null) m.recoverHPBar();
                timer = 2.0f; timeStr.text = timer + "s";
            }

            //if (hit.transform.gameObject.tag == "Flower" && prev == hit.transform.gameObject)
            //{
            //    flower = hit.transform.GetComponent<Monster>();
            //    flower.createHPBar();
            //    if (!flower.getIsDie()) timer -= Time.deltaTime;
            //    else timer = 3.0f;
            //    timeStr.text = Mathf.Round(timer * 100) / 100 + "s";
            //}
            //else
            //{
            //    if (flower != null) flower.destroyHPBar();
            //    timer = 3.0f; timeStr.text = timer + "s";
            //}
            prev = hit.transform.gameObject;
        }
    }
}
