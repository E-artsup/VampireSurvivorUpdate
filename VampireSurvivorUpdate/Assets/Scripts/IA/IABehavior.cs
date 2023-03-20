using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class IABehavior : MonoBehaviour
{
    #region Variables

        #region Variables
        private float healthPoint;
        [SerializeField] private float maxHealthPoint = 5;

        [SerializeField] private int baseATK = 2;
        #endregion

        #region Behavior
        private AIDestinationSetter destinationSetter;
        private GameObject target;
        private WaveSystem waveSystem;
        #endregion

    #endregion

    private void Awake() {
        healthPoint = maxHealthPoint;

        destinationSetter = GetComponent<AIDestinationSetter>();
        waveSystem = GameObject.Find("WaveManager").GetComponent<WaveSystem>();
        target = destinationSetter.target.gameObject;
    }

    public void AttackThePlayer()
    {
        target.GetComponent<PlayerStats>().currentHealth = target.GetComponent<PlayerStats>().currentHealth - baseATK;
    }

    [ContextMenu("Take Damage")]
    public void DamageTest()
    {
        TakeDamage(3);
    }

    
    public float TakeDamage(float damage)
    {
        // print(healthPoint + " / " + maxHealthPoint);

        float _healthPointAfterDamage = healthPoint - damage;
        float _actualDamageTaken = healthPoint - _healthPointAfterDamage;

        healthPoint = _healthPointAfterDamage;

        if(healthPoint <= 0)
        {
            Death();
        }
        
        // print(healthPoint + " / " + maxHealthPoint);
        // print("Acutal Damage Taken : " + _actualDamageTaken);
        return _actualDamageTaken;
    }

    private void Death()
    {
        waveSystem.Deactivate(this.gameObject);
    }

}
