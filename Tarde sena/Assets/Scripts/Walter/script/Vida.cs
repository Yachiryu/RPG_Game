using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RPGCharacterAnims;
using RPGCharacterAnims.Lookups;
using RPGCharacterAnims.Actions;

public class Vida : MonoBehaviour
{
    public RPGCharacterController rpgCharacterController;

    public int vida = 100;
    [SerializeField]private int contadorVida;
    public Slider barravida;

    void Start()
    {
        if (CompareTag("Player"))
        {
            rpgCharacterController = GetComponent<RPGCharacterController>();
            barravida.maxValue = vida;
            barravida.value = vida;
            contadorVida = vida;
        }
    }

    public void ManejoVida(int cantidad)
    {
        contadorVida += cantidad;
        barravida.value = contadorVida;
        contadorVida = Mathf.Clamp(contadorVida,0,vida);
        if (contadorVida > 0)
        {
            rpgCharacterController.StartAction(HandlerTypes.GetHit, new HitContext());
        }
        else
        {
            StartCoroutine(CRecargarEscenaEnMuerte());
        }
    }

    IEnumerator CRecargarEscenaEnMuerte()
    {
        rpgCharacterController.StartAction(HandlerTypes.Knockdown, new HitContext((int)KnockdownType.Knockdown1, Vector3.back));
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
