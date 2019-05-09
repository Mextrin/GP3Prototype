using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [HideInInspector] public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    public float cooldownTime = 3f;
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

            int randomSpawner;
            //Spawn
            {
                int[] pointChoices = new int[spawnPoints.Count];
                for (int i = 0; i < spawnPoints.Count; i++)
                {
                    pointChoices[i] = spawnPoints[i].pointChoiceChance;
                }

                randomSpawner = RandomByChance(pointChoices);
            }

            SpawnPoint chosenSpawner = spawnPoints[randomSpawner];
            if (chosenSpawner.enemiesToSpawn.Count > 0)
            {
                int randomEnemy = RandomByChance(chosenSpawner.enemySpawnChance.ToArray());
                if (chosenSpawner.enemiesToSpawn[randomEnemy])
                {
                    Instantiate(chosenSpawner.enemiesToSpawn[randomEnemy], spawnPoints[randomSpawner].position, Quaternion.identity);
                }
            }
        }
    }

    int RandomByChance(int[] chances)
    {
        List<int> chanceID = new List<int>();
        for (int i = 0; i < chances.Length; i++)
        {
            for (int j = 0; j < chances[i]; j++)
            {
                chanceID.Add(i);
            }
        }
        return chanceID[Random.Range(0, chanceID.Count)];
    }
}
