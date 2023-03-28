using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterPulled : MonoBehaviour
{
    [SerializeField] public List<GameObject> monsterSelected;
    [SerializeField] public int monsterCount;
    [SerializeField] public bool monsterPoolSelected1;
    [SerializeField] public bool monsterPoolSelected2;
    [SerializeField] public bool monsterPoolSelected3;
    [SerializeField] private int n = 0;

    // Initialize the shape of the wave
    public void Start()
    {
        TypeOfMonster();
    }

    // Initialize the monster pool(s) from which we will pick our monsters
    public void TypeOfMonster()
    {
        monsterSelected.Clear();

        if (monsterPoolSelected1)
        {
            monsterSelected.Add(WaveSystem.monsterPool1[0]);
        }

        if (monsterPoolSelected2)
        {
            monsterSelected.Add(WaveSystem.monsterPool2[0]);
        }

        if (monsterPoolSelected3)
        {
            monsterSelected.Add(WaveSystem.monsterPool3[0]);
        }
    }

    // Gives a monster selected in one of the random monster pool we initialized
    public GameObject TypeOfMonsterPulled()
    {
        TypeOfMonster();

        n++;

        if(n >= monsterSelected.Count) n = 0;

        return monsterSelected[n];
    }
}
