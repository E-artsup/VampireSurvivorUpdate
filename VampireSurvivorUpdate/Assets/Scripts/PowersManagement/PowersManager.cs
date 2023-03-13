using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersManager : MonoBehaviour
{
    public static PowersManager instance;
    //public PlayerStats playerStats;
    private GameObject player;
    private Power[] powers;


    private Power[] getPowers()
    {
        return powers;
    }

    private bool hasPower(string power)
    {
        foreach (Power p in powers)
        {
            if (p.PowerData.Name == power)
            {
                return true;
            }
        }
        return false;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject getPlayer()
    {
        return player;
    }
}
