using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject {
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBtwSpawn = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject getEnemyPrefab() { return enemyPrefab; }

    public List<Transform> getWaypoints() 
    {
        var waveWaypoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public float getTimeBtwSpawn() { return timeBtwSpawn; }

    public float getSpawnRandomFactor() { return spawnRandomFactor;  }

    public int getNumberOfEnemies() { return numberOfEnemies;  }

    public float getMoveSpeed() { return moveSpeed; }


}
