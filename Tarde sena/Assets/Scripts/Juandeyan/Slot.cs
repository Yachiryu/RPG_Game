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

    //public int iD;
    //public string type;
    //public string nombre;
    //public string description;

    public bool empty;
    //public Sprite icon;

    public Transform slotIconGameObject;

    public int numberOfObjects;

    public TextMeshProUGUI numberObjText;

    //public ItemProperties.Tipo type;

    public ItemProperties slotProperties;
    public ItemProperties slotVacio;

    private void Start()
    {
        inventario = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        slotIconGameObject = transform.GetChild(0);
        slotProperties = slotVacio;
        UpdateSlot();
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
            inventario.RemoveItem(slotProperties, 1);
        }
    }

    public void UpdateNumberObj()
    {
        numberObjText.text = numberOfObjects.ToString();
    }

    public void UseItem()
    {
        inventario.UseItem(slotProperties);
    }

    /*public void OnClickBotton()
    {
        UseItem();
    }*/
}
