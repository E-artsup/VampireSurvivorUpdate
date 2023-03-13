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

    [SerializeField] private List<WeaponSC> weaponPullInTheGame = new();

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
            button.gameObject.SetActive(true);
        }

        GiveTheButtonTheEventToQuitTheMenu();

        int numberOfWeaponToGenerate = listOfButtonForUpgrade.Count;

        List<WeaponSC> weaponsSelectedForTheButton = new();

        weaponsSelectedForTheButton = GetRandomWeaponThePlayerCanUpgrade(numberOfWeaponToGenerate);

        if (weaponsSelectedForTheButton == null)
        {
            //If their is no more weapon the player can have, put the money button

            //We deactivate all the normal buttons
            foreach(Button button in listOfButtonForUpgrade)
            {
                button.gameObject.SetActive(false);
            }
            //We activate the others
            foreach (Button button in listOfButtonAfterThePlayerHaveUpgradeEverythings)
            {
                button.gameObject.SetActive(true);
                return;
            }
        }

        for(int i = 0; i < listOfButtonForUpgrade.Count; i++)
        {
            WeaponSC weaponToUpgrade = weaponsSelectedForTheButton[i];

            if (weaponsSelectedForTheButton == null)
            {
                //If their is no valid weapon the player can have, we deactivate the button
                listOfButtonForUpgrade[i].gameObject.SetActive(false);
            }
            Button button = listOfButtonForUpgrade[i];

            button.onClick.AddListener(weaponToUpgrade.AddTheWeaponToTheSceneAndTheInventory);

            if (button.TryGetComponent<ChangeTextAndIconOfUpgradeButton>(out ChangeTextAndIconOfUpgradeButton script))
            {
                script.SetUIToWeapon(weaponToUpgrade);
            }
        }
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
    /// Set the list of Weapon The Player can have in the Game
    /// </summary>
    /// <param name="newWeaponPull"></param>
    public void SetWeaponPull(List<WeaponSC> newWeaponPull)
    {
        weaponPullInTheGame = newWeaponPull;
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

        foreach (WeaponSC weapon in weaponThePlayerCanGet)
        {
            //We remove the Weapon the player have already max level
            if (weapon.IsTheWeaponMaxLevel())
            {
                weaponThePlayerCanGet.Remove(weapon);
                continue;
            }

            //We check if the inventory of the speciality of the Weapon (actif/passif) if full
            if(InventoryManager.IsInventoryOfThisSpecialityComplete(weapon.speciality))
            {
                //if yes, we remove it
                weaponThePlayerCanGet.Remove(weapon);
                continue;
            }
        }

        //We select randomly a Weapon in the new Weapon List
        List<WeaponSC> randomWeapons = new();

        for(int i = 0; i < numberOfWeaponToGenerate; i++)
        {
            WeaponSC newWeaponToAdd = weaponThePlayerCanGet[Random.Range(0, weaponThePlayerCanGet.Count)];
            Debug.Log("Selected" + newWeaponToAdd.nameOfWeapon);

            randomWeapons.Add(newWeaponToAdd);
            weaponThePlayerCanGet.Remove(newWeaponToAdd);
        }

        return randomWeapons;
    }
}
