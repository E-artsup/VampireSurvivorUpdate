using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Reload the active scene
    /// </summary>
    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //For the moment it only reload the active scene. If you want to reload anther scene like main menu or something else just use the build index
    }
}
