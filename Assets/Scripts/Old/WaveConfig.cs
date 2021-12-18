using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Wave Config | Caleb A. Collar | 10.7.21
[CreateAssetMenu(menuName = "WaveConfig")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject
        enemyPrefab,
        pathPrefab;

    [SerializeField] private float
        timeBetweenSpawn = 1f,
        spawnRandomFactor = .5f,
        moveSpeed = 5f;

    [SerializeField] private int
        numEnemies;

    public GameObject GetEnemy()
    {
        return enemyPrefab;
    }
    
    public List<Transform> GetWaypoints()
    {
        var waypoints = new List<Transform>();
        foreach (Transform waypoint in pathPrefab.transform)
        {
            waypoints.Add(waypoint);
        }
        return waypoints;
    }
    
    public float GetTimeBetweenSpawn()
    {
        return timeBetweenSpawn;
    }
    
    public float GetSpawnRandomFactor()
    {
        return spawnRandomFactor;
    }
    
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    
    public int GetNumEnemies()
    {
        return numEnemies;
    }
}
