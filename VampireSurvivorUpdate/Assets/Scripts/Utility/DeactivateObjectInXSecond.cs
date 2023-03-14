using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateObjectInXSecond : MonoBehaviour
{

    [SerializeField] private float timeBeforeDestroy = 0.5f;

    private void Awake()
    {
        Invoke(nameof(AutoDestroy), timeBeforeDestroy);
    }

    private void AutoDestroy()
    {
        Destroy(gameObject);
    }
}