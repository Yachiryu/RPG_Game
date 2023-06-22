using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Inventory inventario;
    public GameObject item;
    public GameObject panelCraft;
    public int muricionArma;
    internal GameObject armaTwohandSword;

    public bool empty;

    public Transform slotIconGameObject;

    public int numberOfObjects;

    public TextMeshProUGUI numberObjText;


    public ItemProperties slotProperties;
    public ItemProperties slotVacio;

    private void Start()
    {
        inventario = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        slotIconGameObject = transform.GetChild(0);
        slotProperties = slotVacio;
        UpdateSlot();
        UpdateNumberObj();
    }

    public void UpdateSlot()
    {
        //slotIconGameObject.GetComponent<Image>().sprite = icon;
        slotIconGameObject.GetComponent<Image>().sprite = slotProperties.icon;
        muricionArma = slotProperties.desgasteArma;
    }

    public void UpdateUsoArma(int cantidad)
    {
        muricionArma -= cantidad;
        if (muricionArma <= 0)
        {
            armaTwohandSword = null;
            inventario.RemoveItem(slotProperties, 1);
        }
    }

    public void UpdateNumberObj()
    {
        if (numberOfObjects > 0)
        {
            numberObjText.text = numberOfObjects.ToString();
        }
        else
        {
            numberObjText.text = "";
        }
    }

    public void UseItem()
    {
        
        inventario.UseItem(this);
    }

    /*public void OnClickBotton()
    {
        UseItem();
    }*/
}
