using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script makes you able to edit your waves: the shape of the wave, the ennemy pool that spawn in it 
public class WavePattern : MonoBehaviour
{
    [SerializeField] private float polygonFormula;
    [SerializeField] public bool snailShape;
    [SerializeField] private int n;

    // Initialize the shape of the wave
    public void Start()
    {
        TypeOfShape();
    }

    // Initialize which shape of polygon we want
    public int TypeOfShape()
    {
        if (snailShape)
        {
            n = 1;
        }

        return n;
    }

    // Modifies the polygon shape accordingly to the initialization
    public float ViewDistanceCalculator(int i, int rayCount)
    {
        switch (n)
        {
            case 0:
                polygonFormula = 20;
                break;

            case 1:
                polygonFormula = (float)i * (1 / (float)rayCount) * 20;
                break;

            default: 
                break;
        }
        return polygonFormula;
    }
}

