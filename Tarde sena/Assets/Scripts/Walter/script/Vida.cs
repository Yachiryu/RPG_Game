using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPGCharacterAnims;
using RPGCharacterAnims.Lookups;
using RPGCharacterAnims.Actions;

public class Vida : MonoBehaviour
{
    public RPGCharacterController rpgCharacterController;

    public int vida = 100;
    public Slider barravida;

    void Start()
    {
        rpgCharacterController = GetComponent<RPGCharacterController>();
        StartCoroutine(Mientras());
    }

    IEnumerator Mientras()
    {
        if (transform.tag != "Player")
        {
            yield return new WaitForSeconds(5);

            ManejoVida(100);
        }
    
    }

    public void ManejoVida(int cantidad)
    {
        vida += cantidad;
        if (vida > 0)
        {
            rpgCharacterController.StartAction(HandlerTypes.GetHit, new HitContext());
        }
        else
        {
            rpgCharacterController.StartAction(HandlerTypes.Knockdown, new HitContext((int)KnockdownType.Knockdown1, Vector3.back));
        }
    }

   public void EventoMuerteIA(GameObject obj)
    {
        Transform padre = obj.transform.parent;
        obj.transform.parent = null;
        for (var i = 0; i < padre.parent.childCount; i++)
        {
            if (padre.parent.GetChild(i) == padre)
            {
                padre.parent.GetComponent<spawnmanager>().GenerarSpawn(i);
            }
        }
        Destroy(obj, 6f);//colocar tiempo de cuando muere
    }
}
