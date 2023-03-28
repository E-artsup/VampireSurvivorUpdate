using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Index17 : Power
{
    //===========
    //VARIABLE
    //==========

    [SerializeField] private float m;

    //===========
    //MONOBEHAVIOUR
    //==========

    public void Update()
    {
        if (cooldownRemaining > 0)
        {
            cooldownRemaining -= Time.deltaTime;
        }
        else
        {
            Attack();
            cooldownRemaining = powerData.Cooldown - 2 * currentLevel;
        }
    }

    //===========
    //FONCTION
    //==========
    public override void Attack()
    {
        try { attackSound.Play(); } catch { }
        PowersManager.instance.playerStats.attackCooldown -= 10 - 5 * currentLevel;
        StartCoroutine(DurationOfTheBersekerEffect(powerData.Duration + currentLevel));
    }
    private IEnumerator DurationOfTheBersekerEffect(float time)
    {
        yield return new WaitForSeconds(time);
        PowersManager.instance.playerStats.attackCooldown += 10 - 5 * currentLevel;
    }
}
