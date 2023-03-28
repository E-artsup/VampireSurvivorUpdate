using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Index2 : Power
{
    //========
    //VARIABLE
    //========
    [SerializeField] private ParticleSystem particleFeedback;
    [SerializeField] private Collider damageTrigger;
    //========
    //MONOBEHAVIOUR
    //========
    public void Update()
    {
        if (this.cooldownRemaining <= 0)
        {
            Attack();
            this.cooldownRemaining = powerData.Cooldown - 0.3f * GetCurrentLevel;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.TryGetComponent<AIBehavior>(out AIBehavior script))
        {
            script.TakeDamage(powerData.GetDamageCalcul(currentLevel) * powerData.HitBoxDelay);
        }
        else
        {
            if (other.transform.parent != null)
            {
                if (other.transform.parent.TryGetComponent<AIBehavior>(out AIBehavior enemyParent))
                {
                    // Deals damage to the enemy
                    enemyParent.TakeDamage(powerData.GetDamageCalcul(currentLevel));
                }
            }
        }
    }
    //========
    //FONCTION
    //========
    public override void Attack()
    {
        try { attackSound.Play(); } catch { }
        Vector3 ennemyPositionInViewport = PowerUtils.GetRandomEnemyPosition(false).Item1;
        transform.position = ennemyPositionInViewport;

        particleFeedback.Play();
    }
}
