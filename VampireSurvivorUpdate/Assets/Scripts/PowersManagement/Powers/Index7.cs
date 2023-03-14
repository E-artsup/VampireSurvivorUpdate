using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Index7 : Power
{
    //========
    //VARIABLE
    //========
    [SerializeField] private LineRenderer lineBetweenEnnemis;

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
        if (lineBetweenEnnemis == null) return;
        Transform playerRef = PowersManager.instance.getPlayer().transform;

        Vector3 previousLocation = playerRef.position;
        (Vector3, GameObject) nextLocation = PowerUtils.GetNearestEnemyLocation(playerRef.position);
        if (nextLocation.Item2 == null) return;

        List<GameObject> ennemisTouchByTheAttack = new();
        for (int i = 0; i <= 4 + currentLevel; i++)
        {
            LineRenderer newLine = Instantiate<LineRenderer>(lineBetweenEnnemis, Vector3.zero, Quaternion.identity);
            newLine.SetPosition(0, new(previousLocation.x, 1, previousLocation.z));
            newLine.SetPosition(1, new(nextLocation.Item1.x, 1, nextLocation.Item1.z));

            /*if (nextLocation.Item2.TryGetComponent<AIBehavior>(out AIBehavior script))
            {
                script.TakeDamage(powerData.GetDamageCalcul(currentLevel));
            }*/
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
