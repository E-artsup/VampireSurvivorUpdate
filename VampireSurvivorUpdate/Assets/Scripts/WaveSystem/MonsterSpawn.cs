using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject enemyPrefab;  // the enemy prefab to spawn
    public PolygonCollider2D polygonCollider;  // reference to the polygon collider created by PolygonCreator script
    public MonsterPulled monsterPulled;
    public WaveSystem waveSystem;
    public Bounds bounds;
    public int i = 0, n;
    public float spawnRate = 2;
    public int numberOfEnnemies;
    public int maxEnnemies;
    public Vector2 randomPoint;
    public GameObject monsterToPull;
    public void Start()
    {
        monsterPulled = this.gameObject.GetComponent<MonsterPulled>();
        waveSystem = GameObject.Find("WaveManager").GetComponent<WaveSystem>();
        InvokeRepeating("WaveSpawner", 0.1f, 1f);
    }

    public void WaveSpawner()
    {
        // Gets the Spawn position inside the PolygonCollider
        while (EnnemySpawn() == false)
        {
            EnnemySpawn();
        }

        // Checks which monster needs to be pulled then activates him and teleport the monster to its desired location
        monsterToPull = monsterPulled.TypeOfMonsterPulled();
        waveSystem.Activate(monsterToPull);
        monsterToPull.transform.position = randomPoint;


        numberOfEnnemies++;

        // If all the ennemies we wanted to spawn spawned then stop spawning monster in this wave
        if(numberOfEnnemies >= maxEnnemies)
        {
            waveSystem.NextWave();
        }
    }

    public bool EnnemySpawn()
    {
        bounds = polygonCollider.bounds;

        // Select a random point inside the polygon collider bounds
        randomPoint = new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));

        // Check if the random point is inside the polygon collider using Physics2D.OverlapPoint method
        if (polygonCollider.OverlapPoint(randomPoint))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}