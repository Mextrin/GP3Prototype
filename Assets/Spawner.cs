using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [HideInInspector] public List<Vector3> spawnPoints = new List<Vector3>();
    public List<int> spawnChance = new List<int>();
    public GameObject enemyToSpawn;

    public float cooldownTime;
    float cooldownRemaining = 0;

    void Start()
    {
        
    }

    void Update()
    {
        cooldownRemaining -= Time.deltaTime;
        if (cooldownRemaining <= 0 && spawnPoints.Count > 0)
        {
            //Reset cooldown timer
            cooldownRemaining = cooldownTime;

            //Spawn
            List<int> SpawnID = new List<int>();
            for (int i = 0; i < spawnChance.Count; i++)
            {
                for (int j = 0; j < spawnChance[i]; j++)
                {
                    SpawnID.Add(i);
                }
            }
            int randomSpawner = Random.Range(0, SpawnID.Count);
            Instantiate(enemyToSpawn, spawnPoints[SpawnID[randomSpawner]], Quaternion.identity);
        }
    }
}
