using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerMoving : MonoBehaviour
{
    private float r = 2.5f;
    private float ang = 270f;
    private float deltaX;
    private bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            ang += 1f;
        }
        else
        {
            ang -= 1f;
        }

        transform.position = new Vector3(r * Mathf.Sin(Mathf.Deg2Rad * ang) + deltaX, r * Mathf.Cos(Mathf.Deg2Rad * ang));

        if (ang >= 630f)
        {
            deltaX = -2f * r;
            ang = 90f;
            flag = !flag;
        }
        else if (ang <= -270f)
        {
            flag = !flag;
            ang = 270f;
            deltaX = 0;
        } 
    }
}
