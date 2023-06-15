using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Crafteo : MonoBehaviour
{
    public GameObject text;
    public GameObject inventory;

    public Inventory inventory_script;
    public CraftProcess[] craftProcess;

    public bool inventoryBool;
    public bool craft_Zone = false;

    private void Start()
    {
        craftProcess = FindObjectsOfType<CraftProcess>();
    }
    private void Update()
    {
        Craftear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            text.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            craft_Zone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            text.SetActive(false);
            craft_Zone = false;
        }
    }
    private void Craftear()
    {
        if (craft_Zone)
        {
            if (Input.GetButtonDown("Interaction"))
            {
                inventoryBool = !inventoryBool;
            }
            if (inventoryBool == true)
            {
                inventory_script.inventoryEnabled = true;

                for (int i = 0; i < craftProcess.Length; i++)
                {
                    craftProcess[i].can_Search = true;
                }

                text.SetActive(false);
                inventory.SetActive(true);
            }
            else
            {
                inventory_script.inventoryEnabled = false;

                for (int i = 0; i < craftProcess.Length; i++)
                {
                    craftProcess[i].can_Search = false;
                }

                text.SetActive(true);
                inventory.SetActive(false);
            }
        }
    }
}
