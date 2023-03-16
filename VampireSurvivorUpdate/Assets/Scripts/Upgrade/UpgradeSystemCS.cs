using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Make the Upgrade Button Spawn When you Level up and Assigne them the Event
/// </summary>
public class UpgradeSystemCS : MonoBehaviour
{
    //===========
    //VARIABLE
    //===========

    [Header("Button in the Scene")]
    //We get the button the player have to click when he want to upgrade
    [SerializeField] private List<Button> listOfButtonForUpgrade = new();
    [SerializeField] private List<Button> listOfButtonAfterThePlayerHaveUpgradeEverythings = new();

    [SerializeField] private List<WeaponSC> weaponPullInTheGame;

    private static UpgradeSystemCS instance;

    //===========
    //MONOBEHAVIOUR
    //===========

    private void Awake()
    {
        //We the Upgrade System is in the Scene, we add a static ref to him
        instance = this;

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        foreach (Button button in listOfButtonForUpgrade)
        {
            //We reset the button event
            button.onClick.RemoveAllListeners();
            //We reactivate the button
            button.gameObject.SetActive(false);
        }
        try 
        {
            RemoveMaxLevelWeaponsFromWeaponPull();
            RemoveSpecialityIfInventoryIsFull();
        } catch { }

        int numberOfWeaponToGenerate = listOfButtonForUpgrade.Count;

        List<WeaponSC> weaponsSelectedForTheButton = new();

        weaponsSelectedForTheButton = GetRandomWeaponThePlayerCanUpgrade(numberOfWeaponToGenerate);

        if (weaponsSelectedForTheButton.Count <= 0)
        {
            //If their is no more weapon the player can have, put the money button

            //We activate the others
            foreach (Button button in listOfButtonAfterThePlayerHaveUpgradeEverythings)
            {
                button.gameObject.SetActive(true);
            }
            return;
        }

        for(int i = 0; i < weaponsSelectedForTheButton.Count; i++)
        {
            WeaponSC weaponToUpgrade = weaponsSelectedForTheButton[i];

            Button button = listOfButtonForUpgrade[i];

            button.gameObject.SetActive(true);

            button.onClick.AddListener(weaponToUpgrade.AddTheWeaponToTheSceneAndTheInventory);

            if (button.TryGetComponent<ChangeTextAndIconOfUpgradeButton>(out ChangeTextAndIconOfUpgradeButton script))
            {
                script.SetUIToWeapon(weaponToUpgrade);
            }
        }
        GiveTheButtonTheEventToQuitTheMenu();
    }
    //===========
    //FONCTION
    //===========

    /// <summary>
    /// Spawn The UpgradeMenu / UI and Stop The Time
    /// </summary>
    public static void SpawnUpgradeMenu()
    {
        //We check if the menu is in the scene
        if (instance != null)
        {
            //if no add the menu to the scene;
            Debug.LogError("The Upgrade UI is not in the Scene");
        }
        //if yes, we configure the button and set the Update canvas to Active
        else
        {

            instance.gameObject.SetActive(true);

            //We stop the time to let the player chose
            Time.timeScale = 0;
            //Maybe for the animation, we gonna have to find a another way to do it
        }
    }
    /// <summary>
    /// Exit The Upgrade Menu if open
    /// </summary>
    private void QuitTheUpgradeMenu()
    {
        //Null check
        if (instance == null) return;
        //We get the parent of the Upgrade Menu

        //We deactive the Gameobject in the scene
        instance.gameObject.SetActive(false);
    }
    /// <summary>
    /// Play In Awake,subscribe the quit upgrade menu to all the button clicked event
    /// </summary>
    private void GiveTheButtonTheEventToQuitTheMenu()
    {
        //foreach Button in the list of Button
        foreach (Button button in listOfButtonForUpgrade)
        {
            //We get the clicked event
            button.onClick.AddListener(QuitTheUpgradeMenu);
            //We subscribe the Quit Upgrade menu Methode
        }
    }
    /// <summary>
    /// Return X number of different random Weapon The Player Can Upgrade
    /// </summary>
    private List<WeaponSC> GetRandomWeaponThePlayerCanUpgrade(int numberOfWeaponToGenerate)
    {
        //We get the list of Weapon the player can have
        List<WeaponSC> weaponThePlayerCanGet = new();

        weaponThePlayerCanGet.AddRange(weaponPullInTheGame);

        if (weaponPullInTheGame.Count <= numberOfWeaponToGenerate) numberOfWeaponToGenerate = weaponPullInTheGame.Count;

        //We select randomly a Weapon in the new Weapon List
        List<WeaponSC> randomWeapons = new();

        for(int i = 0; i < numberOfWeaponToGenerate; i++)
        {
            WeaponSC newWeaponToAdd = weaponThePlayerCanGet[Random.Range(0, weaponThePlayerCanGet.Count)];
            Debug.Log("Selected" + newWeaponToAdd.nameOfWeapon);

            //if(!randomWeapons.Contains(newWeaponToAdd))
            randomWeapons.Add(newWeaponToAdd);
            weaponThePlayerCanGet.Remove(newWeaponToAdd);
        }

        return randomWeapons;
    }

    private void RemoveSpecialityIfInventoryIsFull()
    {
        foreach(WeaponSC weaponSC in weaponPullInTheGame)
        {
            if(InventoryManager.IsInventoryOfThisSpecialityComplete(weaponSC.speciality))
            {
                weaponPullInTheGame.Remove(weaponSC);
            }
        }
    }

    private void RemoveMaxLevelWeaponsFromWeaponPull()
    {
        foreach (WeaponSC weaponSC in weaponPullInTheGame)
        {
            if (weaponSC.IsThisWeaponInTheScene(out Power power))
            {
                if (power.IsMaxLevel) 
                { 
                    weaponPullInTheGame.Remove(weaponSC);
                    Debug.Log(weaponSC.name + "is getting removed from the pull at" + power.GetCurrentLevel + "LV");
                }
            }
        }
    }
}
