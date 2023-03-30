using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] public GameObject enemyPrefab;  // the enemy prefab to spawn
    [SerializeField] public PolygonCollider2D polygonCollider;  // reference to the polygon collider created by PolygonCreator script
    [SerializeField] public MonsterPulled monsterPulled;
    [SerializeField] public WaveSystem waveSystem;
    [SerializeField] public Bounds bounds;
    [SerializeField] public int i = 0, n, tries, maxTries, waveTimeLength;
    [SerializeField] public float spawnRate, spawnRange, spawnOffset;
    [SerializeField] public int currentNumberOfEnnemies, numberOfEnnemies, numberOfEnnemiesPerGroup;
    [SerializeField] public bool XZPlan, EnnemyGroup;
    [SerializeField] public Vector3 randomPoint;
    [SerializeField] public GameObject monsterToPull;
    public void Start()
    {
        monsterPulled = this.gameObject.GetComponent<MonsterPulled>();
        waveSystem = GameObject.Find("WaveManager").GetComponent<WaveSystem>();
        spawnRate = (float)waveTimeLength / (float)numberOfEnnemies;
        InvokeRepeating("WaveSpawner", 1f, spawnRate);
    }

    public void WaveSpawner()
    {
        // Gets the Spawn position inside the PolygonCollider
        while (EnnemySpawn() == false && tries < maxTries)
        {
            EnnemySpawn();
        }

        tries = 0;

        if(XZPlan) 
        {
            randomPoint = new Vector3(randomPoint.x, 0, randomPoint.y);
        }

        SingleMonsterSpawn();

        // If we try to spawn a group of ennemies, it will spawn them around the "Leader" of the group
        if (EnnemyGroup)
        {
            for(i = 0; i < numberOfEnnemiesPerGroup; i++)
            {
                spawnOffset = Random.Range(1, spawnRange + 1);
                randomPoint = new Vector3(randomPoint.x + spawnOffset * ((int)Random.Range(0, 1.99f) * 2 - 1),
                                          0, 
                                          randomPoint.z + (spawnRange - (spawnOffset - 1) + 1) * ((int)Random.Range(0, 1.99f) * 2 - 1));
                SingleMonsterSpawn();
            }
        }

        // If all the ennemies we wanted to spawn spawned then stop spawning monster in this wave and go to the next wave
        if(currentNumberOfEnnemies >= numberOfEnnemies)
        {
            CancelInvoke();
            waveSystem.NextWave();
        }
    }

    public bool EnnemySpawn()
    {
        bounds = polygonCollider.bounds;

        // Select a random point inside the polygon collider bounds
        randomPoint = new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));

        tries++;
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

    public void SingleMonsterSpawn()
    {
        // Checks which monster needs to be pulled then activates him and teleport the monster to its desired location
        monsterToPull = monsterPulled.TypeOfMonsterPulled();
        if(monsterToPull != null)
        {
            waveSystem.Activate(monsterToPull);
            monsterToPull.transform.position = randomPoint;
            currentNumberOfEnnemies++;
        }
    }
}