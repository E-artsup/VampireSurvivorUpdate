using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Index3 : Power
{
    //========
    //VARIABLE
    //========

    private List<GameObject> ennemiInHellFire = new();
    private Vector3 forwardPlayer = Vector3.up;
    //========
    //MONOBEHAVIOUR
    //========
    private void Start()
    {
        StartCoroutine(DamageFonction(true, powerData.HitBoxDelay));
        transform.parent = PowersManager.instance.getPlayer().transform.GetChild(0);
        transform.localPosition = - Vector3.forward * 1.5f;
        transform.rotation = Quaternion.Euler(0, 190, 0);
    }
    public void Update()
    {
        if (ennemiInHellFire == null) return;
        foreach (GameObject ennemi in ennemiInHellFire)
        {
            if(!ennemi.activeInHierarchy)
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
        if (ennemiInHellFire.Contains(other.gameObject))
        {
            ennemiInHellFire.Remove(other.gameObject);
        }
    }
    //========
    //FONCTION
    //========
    public override void Attack()
    {
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
        if (repeat) StartCoroutine(DamageFonction(true, powerData.HitBoxDelay));
    }
}
