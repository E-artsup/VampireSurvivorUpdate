using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Index17 : Power
{
    //===========
    //VARIABLE
    //==========

    private float berserkPower = 0;
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
        berserkPower = powerData.BaseDamage + powerData.LevelDamageMultiplier * currentLevel;

        PowersManager.instance.playerStats.attackCooldown += berserkPower;
        StartCoroutine(DurationOfTheBersekerEffect(powerData.Duration + currentLevel));
    }
    private IEnumerator DurationOfTheBersekerEffect(float time)
    {
        yield return new WaitForSeconds(time);
        PowersManager.instance.playerStats.attackCooldown = 0;
    }
}
