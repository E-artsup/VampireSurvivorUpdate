using UnityEngine;

[CreateAssetMenu(fileName = "PowerData", menuName = "Powers/PassiveEffectData")]
public class PassiveEffectData: ScriptableObject {
    
    [SerializeField]
    [Tooltip("The type that will be affected by this effect\n0 = unknown\n1 = Fire\n2 = Water\n3 = Air\n4 = Lightning\n5 = Light\n6 = Darkness")]
    private int type;
    [SerializeField]
    [Tooltip("The base pourcentage of damage that will be added to the attacks damages (0 = 0%, 100 = 100%)\nIf the value is negative that will decrease the damage instead of increasing it")]
    private int baseDamagePourcentageModifier; // don't forget to convert it to a float pourcentage
    [SerializeField]
    [Tooltip("The pourcentage of damage that will be added to the attacks damages by level of the passive (0 = 0%, 100 = 100%)\nIf the value is negative that will decrease the damage instead of increasing it")]
    private int perLevelDamagePourcentageModifier; // don't forget to convert it to a float pourcentage

    public int Type { get => type; }
    public int BaseDamagePourcentageModifier { get => baseDamagePourcentageModifier; }
    public int PerLevelDamagePourcentageModifier { get => perLevelDamagePourcentageModifier; }

}