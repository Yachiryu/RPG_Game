using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogo : MonoBehaviour
{
    public Dialogos[] triggerDialogos;
    int contadorDeEntradas;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && !GameManager.Instance.enDialogo)
        {
            GameManager.Instance.DialogoDuranteJuego(this,triggerDialogos[contadorDeEntradas].dialogo);
            if (contadorDeEntradas<triggerDialogos.Length-1)
                contadorDeEntradas += 1;
        }
    }
}
