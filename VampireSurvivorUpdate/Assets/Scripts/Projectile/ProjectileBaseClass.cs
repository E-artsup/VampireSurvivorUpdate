using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBaseClass : MonoBehaviour
{
    //============
    //VARIABLES
    //============

    [Header("Settings")]
    [SerializeField] private float hitboxDelai = 0.1f;
    [SerializeField] private float projectileDamage = 0.1f;
    [SerializeField] private float projectileDuration = 0.1f;


    /// <summary>
    /// Can the Projectile pierce into multiple ennemy
    /// </summary>
    [SerializeField] private bool pierce = false;
    [SerializeField] private bool warningSign = false;
    [SerializeField] private bool CanHitPlayer = false;
    /// <summary>
    /// The delai between the warning Sign and the Attack
    /// </summary>
    [SerializeField] private float delaiWarning = 0.5f;

    [SerializeField] private ProjectileMovementType movementType;

    //============
    //MONOBEHAVIOUR
    //============
    private void Awake()
    {
        if(warningSign)
        {
            //Make the Warning Sign Pop
        }
    }
    private void FixedUpdate()
    {
        UpdateMovement();
    }
    private void OnTriggerEnter(Collider other)
    {

    }
    private void OnTriggerStay(Collider other)
    {

    }
    //============
    //FONCTION
    //============
    /// <summary>
    /// Destroy The Projectile
    /// </summary>
    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    public void DoDamageToGameObject(GameObject damagedObject)
    {

    }
    /// <summary>
    /// When We Make Variente of the Projectile, we have to this Fonction
    /// </summary>
    public virtual void UpdateMovement()
    {

    }
    public void UpdateProjectileClass(float damage)
    {

    }
}
public enum ProjectileMovementType
{
    Static, Forward
}
