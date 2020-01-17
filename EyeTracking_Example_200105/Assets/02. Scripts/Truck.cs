using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Truck: MonoBehaviour
{
    private NavMeshAgent nav;
    public Transform goal; //추적 타겟
    // Use this for initialization
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(goal.position);
    }
}
