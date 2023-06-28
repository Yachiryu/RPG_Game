using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using RPGCharacterAnims;
using RPGCharacterAnims.Lookups;
using RPGCharacterAnims.Actions;

public class DIalogoNPC : MonoBehaviour
{
    
    public TextMeshProUGUI texto;
    public AudioSource fuenteAudioTexto;
    [SerializeField] float tiempoDuracionTexto;
    public GameObject image;

    public bool tutorial;
    public GameObject[] muros;
    void Start()
    {
        GameManager.Instance.OnDialogo += ActivarDialogo; // Se subscribe al evento OnDialogo
        //GameManager.Instance.DialogoDuranteJuego(this, dialogoIntro.dialogo); //Llama a la funcion que invoca el evento OnDialogo
    }

    public void ActivarDialogo(object sender, Dialogos.Texto e) //Funcion que esta subscrita al evento OnDialogo 
    {
        StartCoroutine(ReproducirDialogo(e));
    }

    //corrutina que reproduce los dialogos
    IEnumerator ReproducirDialogo(Dialogos.Texto lista)
    {
        GameManager.Instance.enDialogo = true;
        if (tutorial)
            MurosTutorial(true);
        image.SetActive(true);
        yield return new WaitForSeconds(1);
        foreach (var item in lista.contenedor)
        {
            texto.text = item.texto;
            fuenteAudioTexto.clip = item.audioTexto;
            //tiempoDuracionTexto = item.audioTexto.length;
            yield return new WaitForSeconds(tiempoDuracionTexto);
        }
        texto.text = "";
        if (tutorial)
            MurosTutorial(false);
        image.SetActive(false);
        GameManager.Instance.enDialogo = false;
    } 

    void MurosTutorial(bool activar)
    {
        foreach (var item in muros)
        {
            item.SetActive(activar);
        }
    }
}
