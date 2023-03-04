using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPulled : MonoBehaviour
{
    [SerializeField] public GameObject monsterSelected;
    [SerializeField] public int monsterCount;
    [SerializeField] public bool monsterPoolSelected1;
    [SerializeField] public bool monsterPoolSelected2;
    [SerializeField] public bool monsterPoolSelected3;
    [SerializeField] private int n;

    // Initialize the shape of the wave
    public void Start()
    {
        TypeOfMonster();
    }

    // Initialize which shape of polygon we want
    public int TypeOfMonster()
    {
        if (monsterPoolSelected1)
        {
            n = 0;
        }

        if (monsterPoolSelected2)
        {
            n = 1;
        }

        if (monsterPoolSelected3)
        {
            n = 2;
        }

        return n;
    }

    // Modifies the polygon shape accordingly to the initialization
    public GameObject TypeOfMonsterPulled(int i)
    {
        switch (n)
        {
            case 0:
                monsterSelected = WaveSystem.monsterPool1[i];
                monsterCount = WaveSystem.monsterPool1.Count;
                break;

            case 1:
                monsterSelected = WaveSystem.monsterPool2[i];
                monsterCount = WaveSystem.monsterPool2.Count;
                break;

            case 2:
                monsterSelected = WaveSystem.monsterPool3[i];
                monsterCount = WaveSystem.monsterPool3.Count;
                break;

            default:
                break;
        }
        return monsterSelected;
    }
}
