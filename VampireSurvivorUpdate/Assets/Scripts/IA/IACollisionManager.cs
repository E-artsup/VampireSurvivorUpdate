using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACollisionManager : MonoBehaviour
{
    private IABehavior behavior;

    private void Awake() {
        behavior = GetComponentInParent<IABehavior>();
    }

    private void OnTriggerEnter(Collider col)
    {
        
        if(col.CompareTag("Player"))
        {
            Debug.Log("IA : PlayerInRange = true");

            behavior.AttackThePlayer();
        }

    }
}
