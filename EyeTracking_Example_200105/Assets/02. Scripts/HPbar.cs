using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPbar : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        Transform parent = GetComponentInParent<Transform>();
        transform.position = parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
    }
}
