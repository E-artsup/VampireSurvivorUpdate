using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Index11 : Power
{
    //========
    //VARIABLE
    //========
    [SerializeField] private List<ParticleSystem> spookyParticules = new();
    //========
    //MONOBEHAVIOUR
    //========
    public void Update()
    {
        if (this.cooldownRemaining <= 0)
        {
            Attack();
            this.cooldownRemaining = powerData.Cooldown;
        }
    }

    //========
    //FONCTION
    //========
    public override void Attack()
    {
        attackSound.Play();
        List<GameObject> listOfEnnemysTouch = new();
        for (int i = 0; i <= 3; i++)
        {
            GameObject randomEnnemy = PowerUtils.GetRandomEnemyObject(false);
            if (randomEnnemy == null) continue;/*
            if (randomEnnemy.TryGetComponent<AIBehavior>(out AIBehavior script))
            {
                script.FreezeForSeconds(3);
                script.TakeDamage(powerData.GetDamageCalcul(currentLevel));
            }*/
            //Feedback
            if (spookyParticules.Count > i)
            {
                spookyParticules[i].transform.position = new(randomEnnemy.transform.position.x, 1, randomEnnemy.transform.position.z);
                spookyParticules[i].Play(true);
            }
            listOfEnnemysTouch.Add(randomEnnemy);
            randomEnnemy.tag = "Untagged";
        }
        foreach (GameObject enemy in listOfEnnemysTouch)
        {
            enemy.tag = "Enemy";
        }
    }
}
