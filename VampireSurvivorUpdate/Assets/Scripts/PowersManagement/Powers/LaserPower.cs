using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserPower : Power {
    [SerializeField]
    private GameObject effect;
    [SerializeField] private Transform player;
    LaserPower(PowersManager powersManager) : base(
        "Laser",
        0,
        "A laser that deals damage to enemies",
        8,
        5,
        2,
        DamageTypeZone.Area,
        0f,
        1,
        0f,
        0f,
        0.8f,
        true,
        0f,
        1,
        true
    ) {
        this.powersManager = powersManager;
    }
    private void Awake()
    {
        this.powersManager = PowersManager.instance;
    }
    public new void Attack()
    {
        // Gets the objets hit by the laser
        RaycastHit[] hits = Physics.SphereCastAll(
             player.position,
            0.5f+(0.1f*this.currentLevel),
             player.forward,
            5+(0.5f*this.currentLevel)
        );
        if(hits.Length > 0){
            foreach(RaycastHit hit in hits){
                // If the object hit is an enemy
                if(hit.collider.gameObject.TryGetComponent<IABehavior>(out IABehavior script)){
                    // Deals damage to the enemy
                    script.TakeDamage(1);
    /* WAITING IMPLEMENTATION OF ENEMY CLASS -> Replace the "Enemy" by the name of the class in the line below */
    
                    //hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(
                    //    this.baseDamage+(this.levelDamageMultiplier*this.currentLevel)
                    //);
                }
            }
        }
    }

    public void Update()
    {
        if(this.cooldownRemaining > 0){
            this.cooldownRemaining -= Time.deltaTime;
        } else {
            Attack();
            this.cooldownRemaining = 1;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(player.position + player.forward * ( 5 + (0.5f * this.currentLevel)), 0.5f + (0.1f * this.currentLevel));
    }
}