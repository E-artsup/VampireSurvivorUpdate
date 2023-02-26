using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonCreator : MonoBehaviour
{
    [SerializeField] public LayerMask layerMask;
    [SerializeField] public Mesh mesh;
    [SerializeField] public PolygonCollider2D polygonCollider;

    [SerializeField] public int rayCount;
    [SerializeField] public float fov, angle, viewDistance, innerDistance;
    [SerializeField] private int VertexIndex, TriangleIndex, outerVertexIndex, innerVertexIndex, quadIndex, outerIndex0, outerIndex1, innerIndex0, innerIndex1;

    [SerializeField] private Vector2 direction;
    [SerializeField] private Vector3 vertex, origin, innerPolygonScale;

    [SerializeField] private List<Vector2> outerPoints, innerPoints;

    [SerializeField] private RaycastHit2D hit;
    [SerializeField] private Vector3[] vertices;
    [SerializeField] private int[] triangles;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        polygonCollider = this.gameObject.GetComponent<PolygonCollider2D>();
        polygonCollider.pathCount = 2;

        outerPoints = new List<Vector2>();
        innerPoints = new List<Vector2>();
    }

    public void LateUpdate()
    {
        outerPoints.Clear();
        innerPoints.Clear();

        // Create vertices and triangles for a donut polygon
        vertices = new Vector3[(rayCount + 1) * 2];
        triangles = new int[rayCount * 2 * 2 * 3];

        origin = transform.position;
        outerVertexIndex = 0;
        innerVertexIndex = rayCount + 1;
        vertices[outerVertexIndex] = origin;
        vertices[innerVertexIndex] = origin;
        VertexIndex = 1;
        TriangleIndex = 0;

        for (int i = 0; i <= rayCount; i++)
        {
            // Set the angle and direction for the vertex and raycast
            angle = i * (fov / rayCount);
            direction = new Vector2(Mathf.Cos(angle * (Mathf.PI / 180f)), Mathf.Sin(angle * (Mathf.PI / 180f)));
            hit = Physics2D.Raycast(this.transform.position, direction, viewDistance, layerMask);
            vertex = origin + new Vector3(direction.x, direction.y) * viewDistance;


            // Create a different scale of the inner polygon based off the outer polygon then add vertices for polygon mesh and collider
            innerPolygonScale = this.transform.position + (vertex - this.transform.position - origin) * innerDistance;
            vertices[innerVertexIndex + VertexIndex] = vertex - innerPolygonScale;
            innerPoints.Add(vertex - innerPolygonScale);

            // If a monster is inside the collider, the vertex will adjust itself so no monsters can spawn in that area
            if (hit.collider != null && hit.collider.CompareTag("Finish") && hit.distance >= viewDistance - viewDistance*innerDistance)
            {
                Debug.Log(hit.distance);
                Debug.Log(viewDistance - viewDistance / innerDistance);
                vertex = hit.point + new Vector2((this.transform.position.x - hit.transform.position.x) / hit.distance, (this.transform.position.y - hit.transform.position.y) / hit.distance);
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
