using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Chaser : Enemy
{
    GameObject[] players;
    GameObject target;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float lowestDistance = float.MaxValue;
        int currentLowest = 0;
        for (int i = 0; i < players.Length; i++) //i += 1 //i = i + 1
        {
            Vector3 playerPosition = players[i].transform.position;
            float distance = Vector3.Distance(transform.position, playerPosition);
            if (distance < lowestDistance)
            {
                lowestDistance = distance;
                currentLowest = i;
            }
        }

        target = players[currentLowest];

        GetComponent<NavMeshAgent>().destination = target.transform.position;
    }
}
