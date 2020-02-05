using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    private float moveSpeed = 15.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float fMove = Time.deltaTime * moveSpeed;
        transform.Translate(Vector3.back * fMove);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MaxBoundary")
        {
            Destroy(this.gameObject);
        }
    }
}
