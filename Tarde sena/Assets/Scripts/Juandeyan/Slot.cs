using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public GameObject item;
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
        slotIconGameObject = transform.GetChild(0);
        slotProperties = slotVacio;
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        //slotIconGameObject.GetComponent<Image>().sprite = icon;
        slotIconGameObject.GetComponent<Image>().sprite = slotProperties.icon;
    }

    public void UpdateNumberObj()
    {
        numberObjText.text = numberOfObjects.ToString();
    }

    public void UseItem()
    {
        item.GetComponent<Item>().ItemUsage();
    }

    public void OnClickBotton()
    {
        UseItem();
    }
}
