using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject HPBar;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MEnemy" && HPBar.transform.GetChild(1).localScale.x > 0f)
        {
            HPBar.transform.GetChild(1).localScale -= new Vector3(HPBar.transform.GetChild(0).localScale.x / 10f, 0.0f, 0.0f);
            Destroy(other.gameObject);
        }
    }
}
