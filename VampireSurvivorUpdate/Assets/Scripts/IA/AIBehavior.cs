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
        private Rigidbody rb;
        private WaveSystem waveSystem;
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
        rb = GetComponentInChildren<Rigidbody>(); // Get the rigidbody
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent.

        waveSystem = GameObject.Find("WaveManager").GetComponent<WaveSystem>();
        agent.speed = baseSpd; // Set Agent Speed into base Speed.
        agent.acceleration = baseAcn; // Set Agent Acceleration into base Acceleration.
    }

    /// <summary>
    /// Removes a certain amount of HP from the player
    /// </summary>
    public void AttackThePlayer()
    {
        target.GetComponent<PlayerHealth>().TakeDamage(baseAtk);
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

        FastTextManager.instance.MakeTextAtLocation(damage.ToString(), transform.position); //Feedback Of The Damagez

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
        waveSystem.Deactivate(this.gameObject);
    }

    /// <summary>
    /// Check if the AI can move, she will move to the target.
    /// Face always in the player direction
    /// </summary>
    private void Update()
    {

        if(canMove) // Check if can move.
        {
            //transform.position = Vector3.MoveTowards(transform.position,target.transform.position, baseSpd);
            agent.SetDestination(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z)); // Move toward the target.
            
            Vector3 direction = target.transform.position - transform.position;
            
            Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z), Vector3.up);
            transform.rotation = rotation;
        }
        else
        {
            agent.SetDestination(transform.position);
        }

    }

    #region Effects Function

    /// <summary>
    /// Function to slow the speed of the IA by a certain amount for x duration.
    /// </summary>
    public void SlowdownForSeconds(float duration, float slowAmount)
    {
        StartCoroutine(SlowdownCoroutine(duration, slowAmount));
    }
    
    /// <summary>
    /// The coroutine for the slowdown function.
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="slowAmount"></param>
    private IEnumerator SlowdownCoroutine(float duration, float slowAmount)
    {
        baseSpd = baseSpd - slowAmount;
        yield return new WaitForSeconds(duration);
        baseSpd = baseSpd + slowAmount;
    }
    
    /// <summary>
    /// Change the target of the AI for x duration.
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="newTarget"></param>
    public void ChangeTargetForSeconds(float duration, GameObject newTarget)
    {
        StartCoroutine(ChangeTargetCoroutine(duration, newTarget));
    }
    
    /// <summary>
    /// Coroutine for the ChangeTarget function.
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="newTarget"></param>
    /// <returns></returns>
    private IEnumerator ChangeTargetCoroutine(float duration, GameObject newTarget)
    {
        GameObject oldTarget = target;
        target = newTarget;
        
        yield return new WaitForSeconds(duration);

        target = oldTarget;
    }
    
    /// <summary>
    /// Freeze the AI for x seconds.
    /// </summary>
    /// <param name="duration"></param>
    public void FreezeForSeconds(float duration)
    {
        StartCoroutine(FreezeCoroutine(duration));
    }
    
    /// <summary>
    /// Coroutine for the Freeze function.
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator FreezeCoroutine(float duration)
    {
        canMove = false;
        
        yield return new WaitForSeconds(duration);

        canMove = true;
    }
    
    #endregion
    
}
