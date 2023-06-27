using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PortalPasarNivel : MonoBehaviour
{
    internal bool habilitado;
    //public TextMeshProUGUI mensaje;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            habilitado = true;
            //mensaje.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            habilitado = false;
            //mensaje.gameObject.SetActive(false);
        }
    }
}
