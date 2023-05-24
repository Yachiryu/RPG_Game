using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    private bool inventoryEnabled;

    public GameObject inventory;

    private int allSlots;

    private int enabledSlots;

    [SerializeField]private GameObject[] slot;

    public GameObject slotHolder;

    void Start()
    {
        allSlots = slotHolder.transform.childCount;

        slot = new GameObject[allSlots];

        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;

            if (slot[i].GetComponent<Slot>().item == null)
            {
                slot[i].GetComponent<Slot>().empty = true;
            }
        }
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        { 
            inventoryEnabled = !inventoryEnabled;
        }

        if (inventoryEnabled)
        {
            inventory.SetActive(true); 
        }
        else
        {
            inventory.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            print("Hola");
            GameObject itemPickedUp = other.gameObject;

            Item item = itemPickedUp.GetComponent<Item>();

            AddItem(itemPickedUp, item.iD, item.type, item.description, item.icon);
        }
    }

    public void AddItem(GameObject itemObject, int itemID, string itemType,string itemDescription, Sprite itemIcon)
    {
        for (int i = 0; i < allSlots; i++)
        {
            Slot slotAdd = slot[i].GetComponent<Slot>();

            if (slotAdd.empty)
            {
                itemObject.GetComponent<Item>().pickedUp = true;

                slotAdd.item = itemObject;
                slotAdd.iD = itemID;
                slotAdd.type = itemType;
                slotAdd.description = itemDescription;
                slotAdd.icon = itemIcon;

                itemObject.transform.parent = slot[i].transform;
                itemObject.SetActive(false);

                slotAdd.UpdateSlot();

                slotAdd.empty = false;
                break;
            }
        }
    }
}
