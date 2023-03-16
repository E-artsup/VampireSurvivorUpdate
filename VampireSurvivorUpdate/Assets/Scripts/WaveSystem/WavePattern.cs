using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

// this script makes you able to edit your waves: the shape of the wave, the ennemy pool that spawn in it 
public class WavePattern : MonoBehaviour
{
    [SerializeField] private float polygonFormula;
    [SerializeField] public bool snailShape, elipse, rectangle, star, diagonalStar, innerPolygonRectangle;
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

        if (elipse)
        {
            n = 2;
        }

        if (rectangle)
        {
            n = 3;
        }

        if (star)
        {
            n = 4;
        }

        if (diagonalStar)
        {
            n = 5;
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
                polygonFormula = 20 + (float)i * (1 / (float)rayCount) * 20;
                break;
            
            case 2:
                polygonFormula = 20 + Mathf.Cos((float)i / (rayCount / 12)) * 5
                                    - Mathf.Abs(Mathf.Clamp(i - rayCount / 12, 0, rayCount / 3) - rayCount / 6) * 0.1f
                                    - Mathf.Abs(Mathf.Clamp(i - rayCount / 1.7f, 0, rayCount / 3) - rayCount / 6) * 0.1f;
                break;

            case 3:
                polygonFormula = 20 + Mathf.Cos((float)i / (rayCount / 12)) * 5
                                    + Mathf.Abs(Mathf.Clamp(i + rayCount / 12, 0, rayCount / 6) - rayCount / 12) * 0.3f 
                                    + Mathf.Abs(Mathf.Clamp(i - rayCount / 12, 0, rayCount / 3) - rayCount / 6) * 0.1f
                                    + Mathf.Abs(Mathf.Clamp(i - rayCount / 2.4f, 0, rayCount / 6) - rayCount / 12) * 0.3f
                                    + Mathf.Abs(Mathf.Clamp(i - rayCount / 1.7f, 0, rayCount / 3) - rayCount / 6) * 0.1f
                                    + Mathf.Abs(Mathf.Clamp(i - rayCount / 1.08f, 0, rayCount / 6) - rayCount / 13) * 0.3f;
                break;

            case 4:
                polygonFormula = 20 + Mathf.Cos((float)i / (rayCount / 12)) * 5 + Mathf.Cos((float)i / 6) * 20;
                break;

            case 5:
                polygonFormula = 20 + Mathf.Cos((float)i / (rayCount / 12)) * 3
                                    + Mathf.Clamp(Mathf.Cos((float)i / (rayCount / 24)), 0.5f, 0.5f) * 15
                                    + Mathf.Clamp(Mathf.Cos((float)i / (rayCount / 24)), -1, -0.5f) * 20;
                break;

            default: 
                break;
        }
        return polygonFormula;
    }

    public float InnerPolygonRectangleCreator(int i, int rayCount)
    {
        if (innerPolygonRectangle)
        {
            polygonFormula = 20 + Mathf.Cos((float)i / (rayCount / 12)) * 5
            + Mathf.Abs(Mathf.Clamp(i + rayCount / 12, 0, rayCount / 6) - rayCount / 12) * 0.3f
            + Mathf.Abs(Mathf.Clamp(i - rayCount / 12, 0, rayCount / 3) - rayCount / 6) * 0.1f
            + Mathf.Abs(Mathf.Clamp(i - rayCount / 2.4f, 0, rayCount / 6) - rayCount / 12) * 0.3f
            + Mathf.Abs(Mathf.Clamp(i - rayCount / 1.7f, 0, rayCount / 3) - rayCount / 6) * 0.1f
            + Mathf.Abs(Mathf.Clamp(i - rayCount / 1.08f, 0, rayCount / 6) - rayCount / 13) * 0.3f;
        }
        else
        {
            ViewDistanceCalculator(i,rayCount);
        }
        return polygonFormula;
    }
}

