using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftProcess : MonoBehaviour
{
    public Slot slot;
    public bool can_Search;
    private bool can_Craft = false;

    public Inventory inventory_slot;

    private void Start()
    {
        inventory_slot = FindObjectOfType<Inventory>();
        
    }
    public void Update()
    {
        Review_of_conditions();
    }
    public void SearchMaterials()
    {
        int contadorItems=0;
        for (int j = 0; j < slot.slotVacio.materialsToCraft.Length; j++)
        {
            for (int i = 0; i < inventory_slot.slots[slot.slotVacio.materialsToCraft[j].material.type].Length; i++)
            {
                if (inventory_slot.slots[slot.slotVacio.materialsToCraft[j].material.type][i].GetComponent<Slot>().slotProperties.iD == slot.slotVacio.materialsToCraft[j].material.iD )
                {
                    Debug.Log(slot.slotVacio.materialsToCraft[j].material.nombre);
                    if (inventory_slot.slots[slot.slotVacio.materialsToCraft[j].material.type][i].GetComponent<Slot>().numberOfObjects >= slot.slotVacio.materialsToCraft[j].cantidad)
                    {
                        contadorItems++;
                    }
                }
            }
        }
        Debug.Log(contadorItems);

        if (slot.slotVacio.materialsToCraft.Length > 0 && contadorItems == slot.slotVacio.materialsToCraft.Length)
        {
            slot.panelCraft.SetActive(false);
            can_Craft = true;
        }
        else
        {
            slot.panelCraft.SetActive(true);
        }
    }
    public void Review_of_conditions()
    {
        if (can_Search)
        {
            SearchMaterials();
        }
    }
    public void Buttom()
    {
        if (can_Craft)
        {
            inventory_slot.AddItem(slot.slotVacio);
            foreach (var item in slot.slotVacio.materialsToCraft)
            {
                inventory_slot.RemoveItem(item.material,item.cantidad);
            }
        }
    }
}
