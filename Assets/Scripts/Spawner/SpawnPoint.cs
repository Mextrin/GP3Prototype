using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnPoint
{
    public SpawnPoint(Vector3 location)
    {
        position = location;
    }

    public List<Enemy> enemiesToSpawn = new List<Enemy>();
    public List<int> enemySpawnChance = new List<int>();
    public int pointChoiceChance = 1;
    public Vector3 position;
}
