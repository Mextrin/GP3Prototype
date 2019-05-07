using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Creep : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NavMeshAgent>().destination = GameObject.Find("Goal").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
