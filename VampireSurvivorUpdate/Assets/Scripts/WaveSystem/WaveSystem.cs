using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is managing the ennemy spawn ( push/pull method )
public class WaveSystem : MonoBehaviour
{
    [SerializeField] private GameObject _monsterPool1, _monsterPool2, _monsterPool3;

    [SerializeField] public static List<GameObject> monsterPool1, monsterPool2, monsterPool3;

    [SerializeField] public int waveNumber;
    public void Start()
    {
        this.gameObject.transform.GetChild(waveNumber).gameObject.SetActive(true);

        MonsterPoolSetup();
    }

    // Set the pool of ennemies 
    public void MonsterPoolSetup()
    {
        _monsterPool1 = GameObject.Find(nameof(monsterPool1)).transform.Find("Deactivated").gameObject;
        _monsterPool2 = GameObject.Find(nameof(monsterPool2)).transform.Find("Deactivated").gameObject;
        _monsterPool3 = GameObject.Find(nameof(monsterPool3)).transform.Find("Deactivated").gameObject;


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

    public void Activate(GameObject monster)
    {
        monster.SetActive(true);
        monster.transform.SetParent(monster.transform.parent.parent.Find("Activated"), false);

        switch (monster.transform.parent.parent.name)
        {
            case "monsterPool1":
                monsterPool1.Remove(monster);
                break;

            case "monsterPool2":
                monsterPool2.Remove(monster);
                break;

            case "monsterPool3":
                monsterPool3.Remove(monster);
                break;

            default:
                Debug.Log("Something went wrong");
                break;
        }
    }
    
    public void Deactivate(GameObject monster, int n)
    {
        monster.SetActive(false);
        monster.transform.SetParent(monster.transform.parent.parent.Find("Deactivated"), false);

        switch (monster.transform.parent.parent.name)
        {
            case "monsterPool1":
                monsterPool1.Add(monster);
                break;

            case "monsterPool2":
                monsterPool2.Add(monster);
                break;

            case "monsterPool3":
                monsterPool3.Add(monster);
                break;

            default:
                Debug.Log("Something went wrong");
                break;
        }
    }

    public void NextWave()
    {
        this.gameObject.transform.GetChild(waveNumber).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(waveNumber + 1).gameObject.SetActive(true);
        waveNumber++;
    }
}
