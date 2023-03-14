using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PolygonCreator : MonoBehaviour
{
    [SerializeField] public LayerMask layerMask;
    [SerializeField] public Mesh mesh;
    [SerializeField] public PolygonCollider2D polygonCollider;
    [SerializeField] private WavePattern wavePattern;
    [SerializeField] private MonsterSpawn monsterSpawn;

    [SerializeField] public int rayCount;
    [SerializeField] public float fov, angle, viewDistance, innerDistance, areaOffset, refreshRate;
    [SerializeField] private int VertexIndex, outerVertexIndex, innerVertexIndex, quadIndex, outerIndex0, outerIndex1, innerIndex0, innerIndex1, XZ;

    [SerializeField] private Vector2 direction;
    [SerializeField] private Vector3 vertex, origin, innerPolygonScale;
    [SerializeField] public Vector3 innerPosition, playerPosition, playerPositionXZ;

    [SerializeField] private List<Vector2> outerPoints, innerPoints;

    [SerializeField] private RaycastHit2D hit;
    [SerializeField] private Vector3[] vertices;
    [SerializeField] private int[] triangles;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        transform.GetChild(0).GetComponent<MeshFilter>().mesh = mesh;
        polygonCollider = this.gameObject.GetComponent<PolygonCollider2D>();
        polygonCollider.pathCount = 2;
        wavePattern = this.gameObject.GetComponent<WavePattern>();
        monsterSpawn= this.gameObject.GetComponent<MonsterSpawn>();

        outerPoints = new List<Vector2>();
        innerPoints = new List<Vector2>();

        // Enables the ability to switch from XY view to XZ view
        XZ = 0;
        if (this.gameObject.GetComponent<MonsterSpawn>().XZPlan)
        {
            XZ = 1;
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        InvokeRepeating("Polygon", 0.99f, monsterSpawn.spawnRate / refreshRate);
    }

    public void Polygon()
    {
        outerPoints.Clear();
        innerPoints.Clear();

        // Create vertices and triangles for a donut polygon
        vertices = new Vector3[(rayCount + 1) * 2];
        triangles = new int[rayCount * 2 * 2 * 3];

        playerPosition = GameObject.Find("Target").gameObject.transform.position;
        playerPositionXZ = new Vector3(playerPosition.x, (playerPosition.y * (1 -XZ)) + (playerPosition.z * (XZ)), 0f);

        origin = playerPositionXZ;
        Debug.Log(origin);
        outerVertexIndex = 0;
        innerVertexIndex = rayCount + 1;
        vertices[outerVertexIndex] = origin;
        vertices[innerVertexIndex] = origin;
        VertexIndex = 1;

        for (int i = 0; i <= rayCount; i++)
        {
            viewDistance = wavePattern.ViewDistanceCalculator(i, rayCount);
            // Set the angle and direction for the vertex and raycast
            angle = i * (fov / rayCount);
            direction = new Vector2(Mathf.Cos(angle * 0.01745f), Mathf.Sin(angle * 0.01745f));
            hit = Physics2D.Raycast(origin + (origin * (1 -XZ)), direction, viewDistance, layerMask);
            Debug.DrawRay(origin + (origin * (1 - XZ)), direction, Color.red);
            vertex = origin + new Vector3(direction.x, direction.y) * viewDistance;

            // Create a different scale for the inner polygon then add vertices for polygon mesh and collider
            innerPolygonScale = origin + (vertex - origin) * innerDistance;
            vertices[innerVertexIndex + VertexIndex] = innerPolygonScale + innerPosition;
            innerPoints.Add(innerPolygonScale + innerPosition);

            // If a monster is inside the collider, the vertex will adjust itself so no monsters can spawn in that area
            if (hit.collider != null && hit.collider.CompareTag("Finish") && hit.distance > viewDistance * innerDistance)
            {
                vertex = origin + (vertex - origin) * ((hit.distance + areaOffset) / viewDistance);
            }

            // Add vertices for the outer polygon mesh and collider after the check so the edge will adjust itself if needed
            vertices[outerVertexIndex + VertexIndex] = vertex;
            outerPoints.Add(vertex);

            // Checks if a polygon is created and create it
            if (i > 0) 
            {
                quadIndex = (i - 1) * 8;
                outerIndex0 = outerVertexIndex + VertexIndex - 1;
                outerIndex1 = outerVertexIndex + VertexIndex;
                innerIndex0 = innerVertexIndex + VertexIndex - 1;
                innerIndex1 = innerVertexIndex + VertexIndex;
                // Create a quad of triangles for the outer polygon
                triangles[quadIndex + 0] = outerIndex0;
                triangles[quadIndex + 1] = innerIndex0;
                triangles[quadIndex + 2] = innerIndex1;
                triangles[quadIndex + 3] = outerIndex0;
                triangles[quadIndex + 4] = innerIndex1;
                triangles[quadIndex + 5] = outerIndex1;
                // Create a quad of triangles for the inner polygon
                triangles[quadIndex + 6] = innerIndex0;
                triangles[quadIndex + 7] = outerIndex0;
                triangles[quadIndex + 8] = outerIndex1;
                triangles[quadIndex + 9] = innerIndex0;
                triangles[quadIndex + 10] = outerIndex1;
                triangles[quadIndex + 11] = innerIndex1;
                VertexIndex++;
            }
        }
        // Create the mesh of the donut polygon
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        // Set the path for the outer and inner polygon in the collider
        polygonCollider.SetPath(0, outerPoints.ToArray());
        polygonCollider.SetPath(1, innerPoints.ToArray());
    }
}
