using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LaserPower : Power 
{
    //========
    //VARIABLE
    //========

    [SerializeField] private LineRenderer laserRender;
    [SerializeField] private LayerMask laserDetectionLayer;
    private Vector3 forwardPlayer = Vector2.up;
    [SerializeField] private VisualEffect laserVFX;

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
            this.cooldownRemaining = powerData.HitBoxDelay;
        }
        LaserRenderer();
    }

    //========
    //FONCTION
    //========
    public override void Attack()
    {
        try { attackSound.Play(); } catch { }
        // Gets the objets hit by the laser
        RaycastHit[] hits = Physics.SphereCastAll(
            PowersManager.instance.getPlayer().transform.position,
            0.5f + (0.1f * currentLevel),
            forwardPlayer,
            5 + (0.5f * currentLevel), laserDetectionLayer
        );

        if (hits.Length > 0)
        {
            laserRender.SetPosition(1, new(hits[0].transform.position.x, 1, hits[0].transform.position.z));
            foreach (RaycastHit hit in hits)
            {
                // If the object hit is an enemy
                if (hit.collider.gameObject.TryGetComponent<AIBehavior>(out AIBehavior enemy))
                {
                    // Deals damage to the enemy
                    enemy.TakeDamage(powerData.GetDamageCalcul(currentLevel));
                    continue;
                }
                else
                {
                    if (hit.transform.parent != null)
                    {
                        if (hit.transform.parent.TryGetComponent<AIBehavior>(out AIBehavior enemyParent))
                        {
                            // Deals damage to the enemy
                            enemyParent.TakeDamage(powerData.GetDamageCalcul(currentLevel));
                            continue;
                        }
                    }
                }
                if(hit.collider.gameObject.TryGetComponent<Drop>(out Drop dropScript))
                {
                    hit.collider.gameObject.SetActive(false);
                    FastTextManager.instance.MakeTextAtLocation("BREAK !", hit.collider.transform.position); //Feedback Of The Damage
                }
            }
        }

        else
        {
            //laserRender.SetPosition(1, forwardPlayer * (5 + (0.5f * this.currentLevel)) + PowersManager.instance.getPlayer().transform.position);
        }
    }
    
    private void LaserRenderer()
    {
        if (InputManager.instance.move.ReadValue<Vector2>() != Vector2.zero)
        {
            forwardPlayer = new(InputManager.instance.move.ReadValue<Vector2>().normalized.x, 0, InputManager.instance.move.ReadValue<Vector2>().normalized.y);
        }
        this.gameObject.transform.position = PowersManager.instance.getPlayer().transform.position;
        //transform.GetChild(0).rotation = PowersManager.instance.getPlayer().transform.GetChild(0).rotation;
        this.laserVFX.SetVector3("Orientation", new Vector3(forwardPlayer.x, 0, forwardPlayer.z));
        //laserRender.SetPosition(0, PowersManager.instance.getPlayer().transform.position);
    }
}