using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPower : Power {

    LaserPower(PowersManager powersManager) : base(powersManager){}

    public override void Attack()
    {
        // Gets the objets hit by the laser
        RaycastHit[] hits = Physics.SphereCastAll(
            this.powersManager.getPlayer().transform.position,
            0.5f+(0.1f*this.currentLevel),
            this.powersManager.getPlayer().transform.forward,
            5+(0.5f*this.currentLevel)
        );
        if(hits.Length > 0){
            foreach(RaycastHit hit in hits){
                // If the object hit is an enemy
                if(hit.collider.gameObject.TryGetComponent<IABehavior>(out IABehavior enemy)){
                    // Deals damage to the enemy
                    enemy.TakeDamage(
                        this.powerData.BaseDamage+(this.powerData.LevelDamageMultiplier*this.currentLevel)
                    );
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
            this.cooldownRemaining = 0;
        }
    }
}