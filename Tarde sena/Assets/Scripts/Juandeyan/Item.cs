using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //public int iD;
    //public string type;
    // public string nombre;
    // public string description;
    // public Sprite icon;

    [HideInInspector]
    public bool pickedUp;

    [HideInInspector]
    public bool equipped;

    [HideInInspector]
    public GameObject weaponManager;

    [HideInInspector]
    public GameObject weapon;

    public bool playersWeapon;

    //public Tipo type;

    public ItemProperties itemProperties;

    public GameObject objeto;

    private void Start()
    {
        // weaponManager = GameObject.FindWithTag("WeaponManager");

        // if (!playersWeapon)
        // {
        //     int allWeapeons = weaponManager.transform.childCount;

        //     for (int i = 0; i < allWeapeons; i++)
        //     {
        //         if (weaponManager.transform.GetChild(i).gameObject.GetComponent<Item>().iD == iD)
        //         {
        //             weapon = weaponManager.transform.GetChild(i).gameObject;
        //         }
        //     }
        // }
    }

    private void Update()
    {
        // if(equipped)
        // {
        //     if (Input.GetKey(KeyCode.E))
        //     {
        //         equipped = false;
        //     }
        //     if(equipped == false)
        //     {
        //         gameObject.SetActive(false);
        //     }
        // }
    }

    public void ItemUsage()
    {
        int allWeapeons = weaponManager.transform.childCount;
        for (int i = 0; i < allWeapeons; i++)
        {
            if (weaponManager.transform.GetChild(i).gameObject.activeInHierarchy == true)
            {
                weaponManager.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        // if (type.ToString() == "Weapon Cube")
        // {
        //     weapon.SetActive(true);
        //     weapon.GetComponent<ItemProperties.Tipo>().equipped = true;
        // }
        // else if (type == "Weapon Sphere")
        // {
        //     weapon.SetActive(true);
        //     weapon.GetComponent<Item>().equipped = true;
        // }
        // else if(type == "Weapon Cylinder")
        // {
        //     weapon.SetActive(true);
        //     weapon.GetComponent<Item>().equipped = true;
        // }
    }




    // public enum Tipo
    // {
    //     weapon, trap, fragment 
    // }
}
