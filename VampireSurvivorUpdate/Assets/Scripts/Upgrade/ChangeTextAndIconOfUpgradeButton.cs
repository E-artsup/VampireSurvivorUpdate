using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeTextAndIconOfUpgradeButton : MonoBehaviour
{
    //===========
    //VARIABLE
    //===========

    [SerializeField] private Text nameOfTheWeapon;
    [SerializeField] private Text descriptionOfTheWeapon;
    [SerializeField] private Image iconOfTheWeapon;

    //===========
    //FONCTION
    //===========
    public void SetUIToWeapon(WeaponSC weapon)
    {
        nameOfTheWeapon.text = weapon.nameOfWeapon;

        if (!weapon.IsThisWeaponInTheScene(out Power power))
            descriptionOfTheWeapon.text = weapon.descriptionOfTheWeapon;
        else 
            descriptionOfTheWeapon.text = weapon.descriptionOfTheLevelUp;

        iconOfTheWeapon.sprite = weapon.iconImage;
    }
}
