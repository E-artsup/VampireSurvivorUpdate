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
            this.cooldownRemaining = powerData.Cooldown - 0.3f * currentLevel;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<IABehavior>(out IABehavior script))
        {
            script.TakeDamage(powerData.GetDamageCalcul(currentLevel) * powerData.HitBoxDelay);
        }
    }
    //========
    //FONCTION
    //========
    public override void Attack()
    {
        Vector3 ennemyPositionInViewport = PowerUtils.GetRandomEnemyPosition(false).Item1;
        transform.position = ennemyPositionInViewport;

        particleFeedback.Play();
    }
}
