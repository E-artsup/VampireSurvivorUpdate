using UnityEngine;

namespace PowersManagement.Powers
{
    public class LaserPower : Power {

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

        public new void Attack()
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
                    if(hit.collider.gameObject.tag == "Enemy"){
                        // Deals damage to the enemy
    
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
                this.cooldownRemaining = 0;
            }
        }
    }
}