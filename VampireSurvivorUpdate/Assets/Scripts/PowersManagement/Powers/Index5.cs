using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Index5 : Power
{
    //
    //VARIABLE
    //
    private List<GameObject> ennemiInTornado = new();
    [SerializeField] private VisualEffect tornadoVFX;
    private Vector3 forwardPlayer = Vector2.up;
    //
    //MONOBEHAVOUR
    //
    private void Start()
    {
        Attack();
        StartCoroutine(DamageFonction(true, powerData.HitBoxDelay));
    }

    private void Update()
    {

        if (!PowerUtils.IsPositionOnViewport(transform.position))
        {
            print("tornade is out off screen");
            Attack();
        }
        //Movement
        transform.position = Vector3.MoveTowards(transform.position, transform.position + forwardPlayer, powerData.ProjectileSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        ennemiInTornado.Add(other.gameObject);
        if (other.TryGetComponent<AIBehavior>(out AIBehavior script))
        {
            script.TakeDamage(powerData.GetDamageCalcul(currentLevel));
        }
        else
        {
            if (other.transform.parent != null)
            {
                if (other.transform.parent.TryGetComponent<AIBehavior>(out AIBehavior enemyParent))
                {
                    // Deals damage to the enemy
                    enemyParent.TakeDamage(powerData.GetDamageCalcul(currentLevel));
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (ennemiInTornado.Contains(other.gameObject))
        {
            ennemiInTornado.Remove(other.gameObject);
        }
    }
    //
    //FONCTION
    //
    public override void Attack()
    {
        if (InputManager.instance.move.ReadValue<Vector2>() != Vector2.zero)
        {
            forwardPlayer = new(InputManager.instance.move.ReadValue<Vector2>().normalized.x, 0, InputManager.instance.move.ReadValue<Vector2>().normalized.y);
        }
        transform.position = PowersManager.instance.getPlayer().transform.position + forwardPlayer;
        tornadoVFX.Play();
    }
    private IEnumerator DamageFonction(bool repeat, float delai)
    {
        yield return new WaitForSeconds(delai);
        //RaycastHit[] ennemiTouch = Physics.SphereCastAll(transform.position, 2, Vector3.zero, 0, gameObject.layer);
        foreach (GameObject ennemi in ennemiInTornado)
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
