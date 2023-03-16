using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PowersManager : MonoBehaviour
{
    public static PowersManager instance;
    public PlayerStats playerStats;
    [SerializeField]
    private GameObject player;
    private List<Power> powers = new();


    //<summary> Get the powers array </summary>
    //<returns> The powers array </returns>
    public List<Power> getPowers()
    {
        return powers;
    }

    //<summary> Register all powers actually active in the scene </summary>
    //<param name="power"> The power object to register </param>
    public void RegisterSpawnedPowers(){
        Power[] _powers = FindObjectsOfType<Power>();
        foreach(Power _power in _powers){
            try{
                RegisterPower(_power);
            } catch(System.Exception e){
                Debug.LogError(e.Message);
            }
        }
    }

    //<summary> Register a power to the powers manager </summary>
    //<param name="power"> The power object to register </param>
    public void RegisterPower(Power power)
    {
        powers.Add(power);
        power.ResetLevel();
        // If the power is not already registered
        /*if(!hasPower(power.PowerData.Name)){
            // Create a new array with one more element
            Power[] _powers = new Power[powers.Length+1];
            // Copy the powers array to the new array
            powers.CopyTo(_powers, 0);
            // Add the new power to the new array
            _powers[_powers.Length-1] = power;
            // Set the powers array to the new array
            powers = _powers;
        } else {
            // throw error if power already registered
            throw new System.Exception("Power already registered");
        }*/
    }

    //<summary> Check if the powers manager has a power </summary>
    //<param name="power"> The name of the power to check </param>
    //<returns> True if the powers manager has the power, false otherwise </returns>
    public bool hasPower(string power)
    {
        // For each power in the powers array
        foreach (Power p in powers)
        {
            // If the power is the one we are looking for
            if (p.PowerData.Name == power)
            {
                return true;
            }
        }
        return false;
    }

    public void removePower(Power power){
        // If the power is registered
        /*if(hasPower(power.PowerData.Name)){
            // Create a new array with one less element
            List<Power> _powers = new Power[powers.Count-1];
            // Create a counter
            int _counter = 0;
            // For each power in the powers array
            foreach(Power _power in powers){
                // If the power is not the one we want to remove
                if(_power.PowerData.Name != power.PowerData.Name){
                    // Add the power to the new array
                    _powers[_counter] = _power;
                    // Increment the counter
                    _counter++;
                }
            }
            // Set the powers array to the new array
            powers = _powers;
            GameObject.Destroy(power.gameObject);
        } else {
            // throw error if power not registered
            throw new System.Exception("Power not registered");
        }*/
    }

    //<summary> Instantiate and register a power </summary>
    //<param name="pathPrefab"> The path of the prefab of the power to instantiate </param>
    //<returns> The power object instantiated </returns>
    public Power InstantiateAndRegisterPower(GameObject powerPrefab){
        // Instantiate the power
        GameObject _power = Instantiate(powerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        // Register the power
        RegisterPower(_power.GetComponent<Power>());
        return _power.GetComponent<Power>();
    }

    // Method apealed when creating the object
    private void Awake()
    {
        // If the instance is null
        if (instance == null)
        {
            // Set the instance to this
            instance = this;
        }
        else
        {
            // Destroy the new object if an instance already exists
            Destroy(gameObject);
        }
    }



    //<summary> Method get the player game object </summary>
    //<returns> The player game object </returns>
    public GameObject getPlayer()
    {
        return player;
    }
}
