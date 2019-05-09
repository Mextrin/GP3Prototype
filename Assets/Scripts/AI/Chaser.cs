using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Chaser : Enemy
{
    PlayerController[] players;
    GameObject target;

    private void Start()
    {
        players = GameObject.FindObjectsOfType<PlayerController>();
        Destroy(gameObject, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        float lowestDistance = float.MaxValue;
        target = null;
        for (int i = 0; i < players.Length; i++) //i += 1 //i = i + 1
        {
            if (players[i] && players[i].isAlive)
            {
                Vector3 playerPosition = players[i].transform.position;
                float distance = Vector3.Distance(transform.position, playerPosition);
                if (distance < lowestDistance)
                {
                    lowestDistance = distance;
                    target = players[i].gameObject;
                }
            }

        }

        if (!target)
        {
            target = GameObject.Find("Goal");
        }

        GetComponent<NavMeshAgent>().destination = target.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player && player.isAlive)
        {
            //Kill player here
            player.KillPlayer();
            Destroy(gameObject);
        }
    }
}
