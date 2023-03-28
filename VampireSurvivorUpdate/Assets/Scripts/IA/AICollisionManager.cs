using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollisionManager : MonoBehaviour
{
    private AIBehavior behavior;

    private void Awake() {
        behavior = GetComponentInParent<AIBehavior>(); // Get the AI Behavior
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
