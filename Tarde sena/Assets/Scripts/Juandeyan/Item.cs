using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int iD;
    public string type;
    public string description;
    public Sprite icon;

    [HideInInspector]
    public bool pickedUp;
    
    [HideInInspector]
    public bool equipped;

    private void Update()
    {
        if(equipped)
        {

        }
    }

    public void ItemUsage()
    {
        if (type == "Weapon")
        {

        }
    }
}
