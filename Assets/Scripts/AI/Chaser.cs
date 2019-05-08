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

        Destroy(gameObject, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        float lowestDistance = float.MaxValue;
        target = null;
        for (int i = 0; i < players.Length; i++) //i += 1 //i = i + 1
        {
            if (players[i])
            {
                Vector3 playerPosition = players[i].transform.position;
                float distance = Vector3.Distance(transform.position, playerPosition);
                if (distance < lowestDistance)
                {
                    lowestDistance = distance;
                    target = players[i];
                }
            }
        }

        if (target)
            GetComponent<NavMeshAgent>().destination = target.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Kill player here
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
