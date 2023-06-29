using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public bool inventoryEnabled;
    public Crafteo crafteo_Scrip;

    public GameObject inventory;

    public RPGCharacterWeaponController weaponManager;
    public Transform padreArmas;
    public Vida vida;
    private int allSlots;

    private int[] allSlot;
    private int enabledSlots;

    internal ItemProperties.Tipo[] tipoSlot;
    public Dictionary<ItemProperties.Tipo, GameObject[]> slots;
    
    public GameObject[] slotHolders;
    private const int maxNumberObj = 10;

    void Start()
    {
        slots = new Dictionary<ItemProperties.Tipo, GameObject[]>();
        allSlot = new int[slotHolders.Length];
        
        tipoSlot = (ItemProperties.Tipo[])System.Enum.GetValues(typeof(ItemProperties.Tipo));

        for (int i = 0; i < slotHolders.Length; i++)
        {
            allSlot[i] = slotHolders[i].transform.childCount;
            slots.Add(tipoSlot[i], new GameObject[allSlot[i]]);
        }

        for (int i = 0; i < allSlot.Length; i++)
        {
            for (int j = 0; j < allSlot[i]; j++)
            {
                slots[tipoSlot[i]][j] = slotHolders[i].transform.GetChild(j).gameObject;

                if (slots[tipoSlot[i]][j].GetComponent<Slot>().item == null && slots[tipoSlot[i]][j].GetComponent<Slot>().numberOfObjects == 0)
                {
                        slots[tipoSlot[i]][j].GetComponent<Slot>().empty = true;
                }
            }
        }
        foreach (var item in slotHolders)
        {
            item.transform.parent.gameObject.SetActive(false);
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
            crafteo_Scrip.inventoryBool = true;
            inventory.SetActive(true);
        }
        else
        {
            crafteo_Scrip.inventoryBool = false;
            inventory.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            AddItem(item.itemProperties, item.objeto);

        }
    }

    public void AddItem(ItemProperties item, GameObject itemObject = null )
    {


        for (int i = 0; i < slots[item.type].Length; i++)
        {

            Slot slotAdd = slots[item.type][i].GetComponent<Slot>();

            print(slotAdd.transform.parent);
            print(slotAdd.slotProperties.name);
            print(item.name);
            if (slotAdd.slotProperties.nombre == item.nombre && slotAdd.numberOfObjects < maxNumberObj)
            {
                slotAdd.numberOfObjects++;
                if (itemObject != null)
                {
                    Destroy(itemObject);
                }
                slotAdd.UpdateNumberObj();
                print($"El mismo {slotAdd.numberOfObjects}");
                break;
            }
            else if (slotAdd.empty)
            {
                print("Empty");
                slotAdd.slotProperties = item;
                slotAdd.numberOfObjects++;

                if (itemObject!=null)
                {
                    Destroy(itemObject);
                }

                slotAdd.UpdateSlot();
                slotAdd.UpdateNumberObj();
                slotAdd.empty = false;
                break;
            }
        }
    }

   
    public void RemoveItem(ItemProperties item, int cantidad = 0)
    {
        for (int j = 0; j < cantidad; j++)
        {
            for (int i = 0; i < slots[item.type].Length; i++)
            {
                Slot slotAdd = slots[item.type][i].GetComponent<Slot>();

                if (slotAdd.slotProperties.nombre == item.nombre)
                {
                    if (slotAdd.numberOfObjects > 1)
                    {
                        slotAdd.numberOfObjects--;
                        slotAdd.UpdateNumberObj();
                        break;
                    }
                    else
                    {
                        slotAdd.slotProperties = slotAdd.slotVacio;
                        slotAdd.empty = true;
                        slotAdd.numberOfObjects--;
                        slotAdd.UpdateSlot();
                        slotAdd.UpdateNumberObj();
                        break;
                    }
                }
            }
        }
    }

    public void UseItem(Slot item)
    {
        ItemProperties itempro = item.slotProperties;
        print(itempro.name);
        switch (itempro.type)
        {
            case ItemProperties.Tipo.weapon:
                for (int i = 0; i < padreArmas.childCount; i++)
                {
                    if (padreArmas.GetChild(i).gameObject.name == itempro.objetoAsociado.name)
                    {
                        weaponManager.twoHandSword = padreArmas.GetChild(i).gameObject;
                        weaponManager.twoHandSword.GetComponent<Item>().slot = item; 
                    }
                }
                break;
            case ItemProperties.Tipo.resources:
                break;
            case ItemProperties.Tipo.item:
                if (itempro.nombre == "Posion")
                {
                    vida.ManejoVida(itempro.regeneracionVida);
                    RemoveItem(itempro, 1);
                }
                else if (itempro.nombre == "Torreta")
                {
                    GameObject[] trampas = GameObject.FindGameObjectsWithTag("ColocarTrampas");
                    foreach (var i in trampas)
                    {
                        if (i.GetComponent<ColocarTrampa>().habilitado && i.transform.childCount==0)
                        {
                            GameObject newTorreta = Instantiate(itempro.objetoAsociado,i.transform.position,i.transform.rotation);
                            newTorreta.transform.parent = i.transform;
                            RemoveItem(itempro, 1);
                        }
                    }
                }
                else if (itempro.nombre == "Cristal")
                {
                    GameObject pasarNivel = GameObject.FindGameObjectWithTag("PortalPasarNivel");
                    if (pasarNivel.GetComponent<PortalPasarNivel>().habilitado)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
                    }
                }
                break;
        }
    }
}
