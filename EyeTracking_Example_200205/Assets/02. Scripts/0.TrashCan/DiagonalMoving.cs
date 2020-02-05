using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//y = 1.5, z = 8.6
public class DiagonalMoving : MonoBehaviour
{
    float wSpeed = 5f;
    float hSpeed = 2f;
    const int LEFT = 0;
    const int RIGHT = 1;
    int direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = Random.Range(0, 2);
        if (direction == LEFT)
            transform.position = new Vector3(transform.position.x, transform.position.y, -6.5f);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, 8.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //좌상우하
        if (direction == LEFT)
        {
            transform.Translate(Vector3.forward * wSpeed * Time.deltaTime);
            transform.Translate(Vector3.down * hSpeed * Time.deltaTime);
        }
        //우상좌하
        else
        {
            transform.Translate(Vector3.back * wSpeed * Time.deltaTime);
            transform.Translate(Vector3.down * hSpeed * Time.deltaTime);
        }

        if (transform.position.z > 8.5 || transform.position.z < -6.5)
        {
            wSpeed *= -1;
            hSpeed *= -1;
        }
    }
}
