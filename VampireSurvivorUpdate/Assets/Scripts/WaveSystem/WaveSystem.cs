using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public float spawnDelay = 1f;
    public int startingWave = 1;
    public int currentWave;
    public int maxActiveMonsters = 20;
    public float spawnRate = 2f;
    public List<GameObject> monsterPool;
    public List<PolygonCollider2D> wavePatterns;
    public Transform playerTransform;

    public int currentActiveMonsters = 0;
    public bool isWaveActive = false;

    //start the first wave
    private void Start()
    {
        currentWave = startingWave;
        StartCoroutine(SpawnWave());
    }

    //how the spawn of the wave work, it just takes the different selected amount of ennmies and teleport them inside the selected spawning zone, and the checks if it can spawn the next wave.
    private IEnumerator SpawnWave()
    {
        isWaveActive = true;

        while (currentActiveMonsters < maxActiveMonsters)
        {
            //get pattern in random, and every X amount of random patter a set boss pattern, there will be a fixed index, make an ARRAY containing all the different poly/spawning zone of every wave, that will be reused with different type of enemies.
            int patternIndex = Random.Range(0, wavePatterns.Count);
            PolygonCollider2D wavePattern = wavePatterns[patternIndex];

            StartCoroutine(SpawnEnemies());
            currentActiveMonsters++;
            yield return new WaitForSeconds(spawnRate);
        }

        //make a better check for the end of the wave, check the time/ the amount
        isWaveActive = false;
        currentActiveMonsters = 0;
        currentWave++;
        StartCoroutine(SpawnWave());
    }

    // how the ennemis will spawn
    private IEnumerator SpawnEnemies()
    {
        //you can do a bunch of things to make this part se the other script and modify it
        int enemyCount = 0;
        while (enemyCount < maxActiveMonsters)
        {
            // Wait for the spawn delay before spawning the next enemy
            yield return new WaitForSeconds(spawnDelay);

            // Spawn an enemy in a random position within the polygon shape
            //Vector2 spawnPosition = GetSpawnPosition();

            //faire un tp des ennemis---------------------------

            //GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemyCount++;
            monsterPool[1].SetActive(true);

        }
    }


    //try to valid the spawn position of the enemy inside the spawning zone
    private Vector2 GetSpawnPosition(PolygonCollider2D wavePattern)
    {
        Vector2 spawnPosition = Vector2.zero;

        bool isPositionValid = false;
        while (!isPositionValid)
        {
            //change this with wave pattern script, make the random point AT THE LIMIT, if it’s valid, good, if not, go towards the inside. That way we don’t have to create weird bound with multiple polys 
            float randomX = Random.Range(wavePattern.bounds.min.x, wavePattern.bounds.max.x);
            float randomY = Random.Range(wavePattern.bounds.min.y, wavePattern.bounds.max.y);

            spawnPosition = new Vector2(randomX, randomY);

            //change overlap to the MAX bounds with the script.
            if (wavePattern.OverlapPoint(spawnPosition))
            {
                isPositionValid = true;
            }
        }

        return spawnPosition;
    }
}
