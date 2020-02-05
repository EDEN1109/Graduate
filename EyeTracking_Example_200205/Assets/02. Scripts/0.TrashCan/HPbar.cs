using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPbar : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Awake()
    {
        Transform parent = GetComponentInParent<Transform>();
        transform.position = parent.position;
        player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
    }
}
