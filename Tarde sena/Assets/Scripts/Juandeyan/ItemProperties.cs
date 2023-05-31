using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ItemProperties/Item")]
public class ItemProperties : ScriptableObject
{
    public int iD;
    public string nombre;
    public string description;
    public float porcentajeDeSalida;
    public int regeneracionVida;
    public GameObject objetoAsociado;
    public Sprite icon;
    public Tipo type;
    public Materials[] materialsToCraft;

    [System.Serializable]
    public class Materials
    {
        public ItemProperties material;
        public int cantidad;
    }
    public enum Tipo
    {
        weapon, resources, item
    }

}
