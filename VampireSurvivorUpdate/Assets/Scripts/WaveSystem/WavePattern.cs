using System.Collections.Generic;
using UnityEngine;

public class WavePattern : MonoBehaviour
{
    public int maxEnemies = 10;
    public float spawnRate = 1f;
    public float raycastDistance = 10f;
    public float spawnDelay = 1f;

    public List<GameObject> monsterPool;
    public Vector2[] polygonVertices;
    public int polygonVertexCount;

    private void Start()
    {
        // Set up the polygon shape using raycasts from the player
        //Put the points in the array to have better control over the area, simplify it?
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.up, raycastDistance);
        polygonVertexCount = hits.Length + 1;
        polygonVertices = new Vector2[polygonVertexCount];
        for (int i = 0; i < hits.Length; i++)
        {
            polygonVertices[i] = hits[i].point - (Vector2)transform.position;
        }
        polygonVertices[polygonVertexCount - 1] = polygonVertices[0];
    }

    private void OnWaveStart()
    {
        //1st of all, get the poly,2nd of all use the other script for enemy spawning 
    }

    // make a clear system with the monster pool and what can spawn.
    private GameObject GetMonsterFromPool()
    {
        foreach (GameObject monster in monsterPool)
        {
            if (!monster.activeInHierarchy)
            {
                return monster;
            }
        }

        return null;
    }

    // Calculate the area of the polygon shape using the shoelace formula
    private float GetPolygonArea()
    {
        float area = 0f;
        for (int i = 0; i < polygonVertexCount - 1; i++)
        {
            area += polygonVertices[i].x * polygonVertices[i + 1].y - polygonVertices[i + 1].x * polygonVertices[i].y;
        }
        return Mathf.Abs(area / 2f);
    }
}

