using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.AI;

public class AIBehavior : MonoBehaviour
{
    #region Variables

        #region Stats
        private float healthPoint;
        [SerializeField] [Tooltip("IA's max health.")] private float maxHealthPoint = 5;
        [SerializeField] [Tooltip("IA's attack stat.")] private int baseAtk = 2;
        [SerializeField] [Tooltip("IA's speed stat.")] private float baseSpd = 3.5f;
        [SerializeField] [Tooltip("IA's acceleration.")] private float baseAcn = 8;
        #endregion

        #region Behavior
        private NavMeshAgent agent;
        private GameObject target;
        [Tooltip("This IA can move ?")] public bool canMove = true;
        #endregion

    #endregion

    /// <summary>
    /// Initialize some Statistics :
    /// HP <- MaxHP
    /// Target <- Player
    /// Agent <- NavMeshAgent
    /// 
    /// Agent.Speed <- base Speed
    /// Agent Acceleration <- base Acceleration
    /// </summary>
    private void Awake() {
        healthPoint = maxHealthPoint; // Set HP into the same amount as the MaxHP.
        target = GameObject.FindGameObjectWithTag("Player"); // Set the player as target.
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent.

        agent.speed = baseSpd; // Set Agent Speed into base Speed.
        agent.acceleration = baseAcn; // Set Agent Acceleration into base Acceleration.
    }

    /// <summary>
    /// Removes a certain amount of HP from the player
    /// </summary>
    public void AttackThePlayer()
    {
        target.GetComponent<PlayerStats>().currentHealth = target.GetComponent<PlayerStats>().currentHealth - baseAtk;
    }

    /// <summary>
    /// Function for testing damaging to the AI
    /// </summary>
    [ContextMenu("Take Damage")]
    public void DamageTest()
    {
        TakeDamage(3);
    }

    /// <summary>
    /// Removes a certain amount of hit points from the AI and returns the true amount of damage received by the AI.
    /// </summary>
    public float TakeDamage(float damage)
    {
        // print(healthPoint + " / " + maxHealthPoint);

        float _healthPointAfterDamage = healthPoint - damage; // Calculate the HP after taking damage.
        float _actualDamageTaken = healthPoint - _healthPointAfterDamage; // Compare the HP between before and after taking damage, to save the actual taken damage.

        healthPoint = _healthPointAfterDamage; // Apply damage.

        if(healthPoint <= 0) // Check if HP are below or equal to zero
        {
            Death(); // 
        }
        
        // print(healthPoint + " / " + maxHealthPoint);
        // print("Acutal Damage Taken : " + _actualDamageTaken);
        return _actualDamageTaken;
    }

    /// <summary>
    /// Disable the IA to simulate death
    /// </summary>
    private void Death()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Check if the AI can move, she will move to the target.
    /// </summary>
    private void Update() {
        
        if(canMove) // Check if can move.
        {
            agent.SetDestination(target.transform.position); // Move toward the target.
        }

    }

}
