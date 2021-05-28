using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text weaponLabel;

    public static UIManager sharedInstance;

    private void Awake()
    {
        sharedInstance = this;
    }

    public void SetWeaponLabel(string weaponName)
    {
        weaponLabel.text = weaponName;
    }
}
