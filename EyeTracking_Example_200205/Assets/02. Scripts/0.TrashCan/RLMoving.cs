using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//startY = -2.3, startZ = -6.5
public class RLMoving : MonoBehaviour
{
    float speed = 3f;
    bool flag = true;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x >= 8.0f || transform.position.x <= -6.5f) { speed *= -1; }
    }
}
