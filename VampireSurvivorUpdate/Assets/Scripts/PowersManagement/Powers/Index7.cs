using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Index7 : Power
{
    //========
    //VARIABLE
    //========
    [SerializeField] private Transform zapVFX;

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
        try { attackSound.Play(); } catch { }
        if (zapVFX == null) return;
        Transform playerRef = PowersManager.instance.getPlayer().transform;

        Vector3 previousLocation = playerRef.position;
        (Vector3, GameObject) nextLocation = PowerUtils.GetNearestEnemyLocation(playerRef.position);
        if (nextLocation.Item2 == null) return;

        List<GameObject> ennemisTouchByTheAttack = new();
        for (int i = 0; i <= 4 + GetCurrentLevel; i++)
        {
            Transform newVFX = Instantiate(zapVFX, Vector3.zero, Quaternion.identity);
            newVFX.GetChild(1).position = new(previousLocation.x, 1, previousLocation.z);
            newVFX.GetChild(2).position = new(previousLocation.x, 1, previousLocation.z);
            newVFX.GetChild(3).position = new(nextLocation.Item1.x, 1, nextLocation.Item1.z);
            newVFX.GetChild(4).position = new(nextLocation.Item1.x, 1, nextLocation.Item1.z);

            if (nextLocation.Item2.TryGetComponent<AIBehavior>(out AIBehavior script))
            {
                script.TakeDamage(powerData.GetDamageCalcul(currentLevel));
            }
            nextLocation.Item2.tag = "Untagged";
            ennemisTouchByTheAttack.Add(nextLocation.Item2);

            previousLocation = nextLocation.Item1;
            nextLocation = PowerUtils.GetNearestEnemyLocation(previousLocation);

            if (nextLocation.Item2 == null) return;
        }
        foreach (GameObject ennemi in ennemisTouchByTheAttack)
        {
            ennemi.tag = "Enemy";
        }
    }
}
