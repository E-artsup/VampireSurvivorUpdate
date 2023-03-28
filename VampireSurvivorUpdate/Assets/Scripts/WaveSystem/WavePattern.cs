using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script makes you able to edit your waves: the shape of the wave, the ennemy pool that spawn in it 
public class WavePattern : MonoBehaviour
{
    [SerializeField] public float polygonFormula, rectangleWidth, rectangleLength, rLength, rWidth;
    [SerializeField] public bool snailShape, elipse, rectangle, star, diagonalStar, innerPolygonRectangle;
    [SerializeField] private int n, N;

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
            // Formula for the base circle shape
            case 0:
                polygonFormula = 20;
                break;

            // Formula for a spiral type of shape
            case 1:
                polygonFormula = 20 + (float)i * (1 / (float)rayCount) * 20;
                break;

            // Formula for a eclipse shape (slightly crushed circle + 2 clamp to strech the extremities for a better render)
            case 2:
                polygonFormula = 20 + Mathf.Cos((float)i / (rayCount / 12)) * 5
                                    - Mathf.Abs(Mathf.Clamp(i - rayCount / 12, 0, rayCount / 3) - rayCount / 6) * 0.1f
                                    - Mathf.Abs(Mathf.Clamp(i - rayCount / 1.7f, 0, rayCount / 3) - rayCount / 6) * 0.1f;
                break;
            
            // Formula for a rectangle shape (we take the slightly crushed circle and then push inwards the extremities)
            case 3:
                rLength = rectangleLength / 100 * rayCount;
                rWidth = rectangleWidth / 100 * rayCount;
                polygonFormula = 20 + Mathf.Cos((float)i / (rayCount / 12)) * 5
                                    + Mathf.Abs(Mathf.Clamp(i + rWidth * 0.5f, 0, rWidth) - rWidth/2) * 0.3f 
                                    + Mathf.Abs(Mathf.Clamp(i - rWidth * 0.5f, 0, rLength) - rLength/2) * 0.1f
                                    + Mathf.Abs(Mathf.Clamp(i - rWidth * 0.5f - rLength, 0, rWidth) - rWidth/2) * 0.3f
                                    + Mathf.Abs(Mathf.Clamp(i - rWidth * 1.5f - rLength, 0, rLength) - rLength/2) * 0.1f
                                    + Mathf.Abs(Mathf.Clamp(i - rWidth * 1.5f - rLength * 2, 0, rWidth) - rWidth/2) * 0.3f;
                break;

            // Formula for a star shape (this used with a rectangle for the inside of the shape creates 4 circles on the sides) 
            case 4:
                polygonFormula = 20 + Mathf.Cos((float)i / (rayCount / 12)) * 5 + Mathf.Cos((float)i / 6) * 20;
                break;

            // Formula for a star shape (this used with a rectangle for the inside of the shape creates 4 circles on the diagonals)
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

    // If the option is enabled, we change the shape of the inner shape for the rectangle shape
    public float InnerPolygonRectangleCreator(int i, int rayCount)
    {
        if (innerPolygonRectangle)
        {
            N = n;
            n = 3;
            ViewDistanceCalculator(i,rayCount);
            n = N;
        }
        else
        {
            ViewDistanceCalculator(i,rayCount);
        }
        return polygonFormula;
    }
}

