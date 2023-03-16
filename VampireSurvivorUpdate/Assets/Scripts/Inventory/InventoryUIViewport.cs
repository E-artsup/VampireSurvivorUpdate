using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ref to the UI
/// </summary>
public class InventoryUIViewport : MonoBehaviour
{
    //=======
    //VARIABLES
    //=======
    [Header("Scene ref")]
    [SerializeField] private RectTransform weaponsIcons;
    [SerializeField] private RectTransform passifsIcons;

    [Header("Ref of all the image in Inventory")]
    private List<Image> listOfWeaponsIcon = new(6);
    private List<Image> listOfPassifIcon = new(6);

    [Header("")]
    //Get the data of the
    private int weaponSlot = 0;
    private int passifSlot = 0;
    //=======
    //MONOBEHAVIOUR
    //=======
    private void Awake()
    {
        //Declare himself to the static script
        InventoryManager.SetInventoryUIViewport(this);

        //Get A Refference of all the image in the UI
        listOfWeaponsIcon = GetAllTheChildWithImageList(weaponsIcons);
        listOfPassifIcon = GetAllTheChildWithImageList(passifsIcons);

        InventoryManager.ClearInventory();
    }

    //=======
    //FONCTION
    //=======
    /// <summary>
    /// Add the Weapon Icon to the Inventory UI
    /// </summary>
    /// <param name="iconOfTheWeapon"></param>
    public void AddWeaponToTheUI(WeaponSC newWeapon)
    {
        Sprite iconOfTheWeapon = newWeapon.iconImage;
        //Is The Inventory at this limit
        if(weaponSlot <= listOfWeaponsIcon.Count)
        listOfWeaponsIcon[weaponSlot].sprite = iconOfTheWeapon;

        weaponSlot++;
    }
    /// <summary>
    /// Add the Passif to the Inventory UI
    /// </summary>
    /// <param name="newWeapon"></param>
    public void AddPassifToTheUI(WeaponSC newPassif)
    {
        Sprite iconOfThePassif = newPassif.iconImage;
        //Is The Inventory at this limit
        if (passifSlot <= listOfPassifIcon.Count)
        listOfPassifIcon[passifSlot].sprite = iconOfThePassif;

        passifSlot++;
    }

    //=======
    //PRIVEE
    //=======
    /// <summary>
    /// Get all the child that have the component Image in a Rect Transform
    /// </summary>
    private List<Image> GetAllTheChildWithImageList(RectTransform parent)
    {
        List<Image> listOfImage = new();

        foreach (RectTransform child in parent)
        {
            if(child.TryGetComponent<Image>(out Image imageComponent))
            {
                listOfImage.Add(imageComponent);
            }
        }
        return listOfImage;
    }
}
