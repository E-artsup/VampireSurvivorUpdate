using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Index2 : Power
{
    //========
    //VARIABLE
    //========
    private List<GameObject> ennemiInHellFire = new();

    //========
    //MONOBEHAVIOUR
    //========
    private void Awake()
    {
        StartCoroutine(DamageFonction(true, powerData.HitBoxDelay));
    }
    public void Update()
    {
        if (this.cooldownRemaining <= 0)
        {
            Attack();
            this.cooldownRemaining = powerData.Cooldown - 0.3f * currentLevel;
        }
        if (ennemiInHellFire == null) return;
        foreach (GameObject ennemi in ennemiInHellFire)
        {
            if (!ennemi.activeInHierarchy)
            {
                ennemiInHellFire.Remove(ennemi.gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        ennemiInHellFire.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if(ennemiInHellFire.Contains(other.gameObject))
        {
            ennemiInHellFire.Remove(other.gameObject);
        }
    }
    //========
    //FONCTION
    //========
    public override void Attack()
    {
        try { attackSound.Play(); } catch { }
        Vector3 ennemyPositionInViewport = PowerUtils.GetRandomEnemyPosition(false).Item1;
        transform.position = ennemyPositionInViewport;
        DamageFonction(false, 0);
    }
    private IEnumerator DamageFonction(bool repeat, float delai)
    {
        yield return new WaitForSeconds(delai);
        //RaycastHit[] ennemiTouch = Physics.SphereCastAll(transform.position, 2, Vector3.zero, 0, gameObject.layer);
        foreach (GameObject ennemi in ennemiInHellFire)
        {
            print(ennemi.name);
            if (ennemi.TryGetComponent<AIBehavior>(out AIBehavior script))
            {
                script.TakeDamage(powerData.GetDamageCalcul(currentLevel));
            }
            else
            {
                if (ennemi.transform.parent != null)
                {
                    if (ennemi.transform.parent.TryGetComponent<AIBehavior>(out AIBehavior enemyParent))
                    {
                        // Deals damage to the enemy
                        enemyParent.TakeDamage(powerData.GetDamageCalcul(currentLevel));
                    }
                }
            }
        }
        if(repeat) StartCoroutine(DamageFonction(true, powerData.HitBoxDelay));
    }
}

