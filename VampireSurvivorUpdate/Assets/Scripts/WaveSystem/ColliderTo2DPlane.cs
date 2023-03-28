using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTo2DPlane : MonoBehaviour
{
    [SerializeField] public CircleCollider2D circleCollider;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider = this.gameObject.GetComponent<CircleCollider2D>();
        circleCollider.radius = 1;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.parent.position.x, this.gameObject.transform.parent.position.z, 0);
    }
}
