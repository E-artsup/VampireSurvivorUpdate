using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Index11 : Power
{
    //========
    //VARIABLE
    //========
    [SerializeField] private List<VisualEffect> spookyParticules = new();
    [SerializeField] private List<Transform> ennemisTouchs = new();
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
        try
        {
            if (ennemisTouchs.Count <= 0) return;
            for (int i = 0; i <= ennemisTouchs.Count; i++)
            {
                spookyParticules[i].transform.position = new(ennemisTouchs[i].transform.position.x, 3, ennemisTouchs[i].transform.position.z);
            }
        }
        catch { }
    }

    //========
    //FONCTION
    //========
    public override void Attack()
    {
        try { attackSound.Play(); } catch { }
        List<GameObject> listOfEnnemysTouch = new();
        ennemisTouchs.Clear();
        for (int i = 0; i <= 3; i++)
        {
            GameObject randomEnnemy = PowerUtils.GetRandomEnemyObject(false);
            if (randomEnnemy == null) continue;
            if (randomEnnemy.TryGetComponent<AIBehavior>(out AIBehavior script))
            {
                StartCoroutine(LateAttaque(3, script));
                //script.FreezeForSeconds(3);
                //script.TakeDamage(powerData.GetDamageCalcul(currentLevel));
            }
            //Feedback
            if (spookyParticules.Count > i)
            {
                ennemisTouchs.Add(randomEnnemy.transform);
                spookyParticules[i].transform.position = new(randomEnnemy.transform.position.x, 3, randomEnnemy.transform.position.z);
                spookyParticules[i].Play();
            }
            listOfEnnemysTouch.Add(randomEnnemy);
            randomEnnemy.tag = "Untagged";
        }
        foreach (GameObject enemy in listOfEnnemysTouch)
        {
            enemy.tag = "Enemy";
        }
    }
    private IEnumerator LateAttaque(float delai, AIBehavior ennemi)
    {
        yield return new WaitForSeconds(delai);
        ennemi.FreezeForSeconds(3);
        ennemi.TakeDamage(powerData.GetDamageCalcul(currentLevel));
    }
}
