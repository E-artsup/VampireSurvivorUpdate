using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTextManager : MonoBehaviour
{
    //========
    //VARIABLE
    //========
    public static FastTextManager instance;
    [SerializeField] private GameObject prefabOfText = new();
    [SerializeField] private float heightOfDamageText = 1;
    [SerializeField] private Color damageColorOfText = Color.red;

    //========
    //MONOBEHAVIOUR
    //========
    private void Awake()
    {
        //Singleton
        instance = this;
    }
    //========
    //FONCTION
    //========
    /// <summary>
    /// Make A Text Mesh in the location with the affilied text
    /// </summary>
    /// <param name="number"></param>
    /// <param name="worldposition"></param>
    /// <param name="color"></param>
    public void MakeTextAtLocation(string text, Vector3 worldposition)
    {
        print(worldposition);
        GameObject randomText = Instantiate(prefabOfText, worldposition, Quaternion.identity);
        randomText.transform.position = new( worldposition.x, worldposition.y + heightOfDamageText, worldposition.z);
        randomText.GetComponentInChildren<TextMesh>().text = text;
        randomText.GetComponentInChildren<TextMesh>().color = damageColorOfText;
    }
}
