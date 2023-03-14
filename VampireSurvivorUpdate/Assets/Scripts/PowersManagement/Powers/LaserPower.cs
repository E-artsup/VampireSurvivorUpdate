using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPower : Power 
{
    //========
    //VARIABLE
    //========

    [SerializeField] private LineRenderer laserRender;
    [SerializeField] private LayerMask laserDetectionLayer;

    //========
    //MONOBEHAVIOUR
    //========
    public void Update()
    {
        if (this.cooldownRemaining > 0)
        {
            this.cooldownRemaining -= Time.deltaTime;
        }
        else
        {
            Attack();
            this.cooldownRemaining = 0;
        }
        LaserRenderer();
    }

    //========
    //FONCTION
    //========
    public override void Attack()
    {
        // Gets the objets hit by the laser
        RaycastHit[] hits = Physics.SphereCastAll(
            PowersManager.instance.getPlayer().transform.position,
            0.5f+(0.1f*this.currentLevel),
            PowersManager.instance.getPlayer().transform.forward,
            5+(0.5f*this.currentLevel), laserDetectionLayer
        );

        if(hits.Length > 0)
        {
            laserRender.SetPosition( 1, new(hits[0].transform.position.x, 1 ,hits[0].transform.position.z));
            foreach (RaycastHit hit in hits)
            {
                // If the object hit is an enemy
                if (hit.collider.gameObject.TryGetComponent<IABehavior>(out IABehavior enemy))
                {
                    // Deals damage to the enemy
                    enemy.TakeDamage(powerData.GetDamageCalcul(currentLevel));
                }
            }
        }

        else
        {
            laserRender.SetPosition( 1, PowersManager.instance.getPlayer().transform.forward * (5 + (0.5f * this.currentLevel)));
        }
    }
    private void LaserRenderer()
    {
        laserRender.SetPosition(0, PowersManager.instance.getPlayer().transform.position);
    }
}