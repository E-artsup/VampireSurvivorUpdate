using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is managing the ennemy spawn ( push/pull method )
public class WaveSystem : MonoBehaviour
{
    [SerializeField] private GameObject _monsterPool1, _monsterPool2, _monsterPool3;

    [SerializeField] public static List<GameObject> monsterPool1, monsterPool2, monsterPool3;

    public void Start()
    {
        MonsterPoolSetup();
    }

    // Set the pool of ennemies 
    public void MonsterPoolSetup()
    {
        _monsterPool1 = GameObject.Find(nameof(monsterPool1));
        _monsterPool2 = GameObject.Find(nameof(monsterPool2));
        _monsterPool3 = GameObject.Find(nameof(monsterPool3));


        monsterPool1 = new List<GameObject>();
        monsterPool2 = new List<GameObject>();
        monsterPool3 = new List<GameObject>();

        for (int j = 0; j < _monsterPool1.transform.childCount; j++)
        {
            monsterPool1.Add(_monsterPool1.transform.GetChild(j).gameObject);
        }

        for (int j = 0; j < _monsterPool2.transform.childCount; j++)
        {
            monsterPool2.Add(_monsterPool2.transform.GetChild(j).gameObject);
        }

        for (int j = 0; j < _monsterPool3.transform.childCount; j++)
        {
            monsterPool3.Add(_monsterPool3.transform.GetChild(j).gameObject);
        }
    }
}
