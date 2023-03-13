using UnityEngine;

[CreateAssetMenu(fileName = "PowerData", menuName = "Powers/PowerData")]
public class PowerData : ScriptableObject
{
    [SerializeField]
    [Tooltip("Name of the power")]
    private new string name;
    [SerializeField]
    [Tooltip("Type of the power\n0 = unknown\n1 = Fire\n2 = Water\n3 = Air\n4 = Lightning\n5 = Light\n6 = Darkness")]
    private int type;
    [SerializeField]
    [Tooltip("Description of the power")]
    private string description;
    [SerializeField]
    [Tooltip("Max level of the power")]
    private int maxLevel;
    [SerializeField]
    [Tooltip("Damage that the players will deal with this power at level 1")]
    private int baseDamage;
    [SerializeField]
    [Tooltip("Damage multiplier per level of the power")]
    private int levelDamageMultiplier;
    [SerializeField]
    [Tooltip("Type of damage zone of the power")]
    private DamageTypeZone damageTypeZone;
    [SerializeField]
    [Tooltip("Speed of the projectiles of the power")]
    private float projectileSpeed;
    [SerializeField]
    [Tooltip("Max amount of projectiles that can be fired at the same time")]
    private int maxFireAmountAtSameTime;
    [SerializeField]
    [Tooltip("Duration of the power")]
    private float duration;
    [SerializeField]
    [Tooltip("Cooldown of the power")]
    private float cooldown;
    [SerializeField]
    [Tooltip("Delay after an enemy is hit and the time he can be hit again")]
    private float hitBoxDelay;
    [SerializeField]
    [Tooltip("If the power can pierce through enemies")]
    private bool piercing;
    [SerializeField]
    [Tooltip("Knockback's strength")]
    private float knockback;
    [SerializeField]
    [Tooltip("Max amount of projectiles that can be on screen at the same time (-1 = infinite, if there are too many projectiles on the scene, the ones with the longest passed lifetime will be destroyed)")]
    private int maxOnScreenAtSameTime;
    [SerializeField]
    [Tooltip("If the power is blocked by walls")]
    private bool blockedByWalls;


    public string Name { get => name; }
    public int Type { get => type; }
    public string Description { get => description; }
    public int MaxLevel { get => maxLevel; }
    public int BaseDamage { get => baseDamage; }
    public int LevelDamageMultiplier { get => levelDamageMultiplier; }
    public DamageTypeZone DamageTypeZone { get => damageTypeZone; }
    public float ProjectileSpeed { get => projectileSpeed; }
    public int MaxFireAmountAtSameTime { get => maxFireAmountAtSameTime; }
    public float Duration { get => duration; }
    public float Cooldown { get => cooldown; }
    public float HitBoxDelay { get => hitBoxDelay; }
    public bool Piercing { get => piercing; }
    public float Knockback { get => knockback; }
    public int MaxOnScreenAtSameTime { get => maxOnScreenAtSameTime; }
    public bool BlockedByWalls { get => blockedByWalls; }
}
