using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //Singleton de la clase GameManager

    [Header("Configuracion De Oleada")]
    [SerializeField]internal int esperaDuracionOleadasMin;
    [SerializeField]internal int esperaDuracionOleadasSeg;
    [SerializeField]internal Etapa etapa;

 
    public event EventHandler<Dialogos.Texto> OnDialogo; 
    public event EventHandler<Etapa> OnEnemySpawn;
    public event EventHandler<Etapa> OnOleada;

    internal bool enDialogo;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        etapa.cantidadEtapas = etapa.spawnsManagers.Length;
        StartCoroutine(COleada());
    }

    IEnumerator COleada()
    {
        yield return new WaitForSeconds(2f);
        Oleada(this, etapa);
    }

    public void DialogoDuranteJuego(object sender, Dialogos.Texto e)//Funcion que invoca los dialogos
    {
        OnDialogo?.Invoke(sender, e);
    }

    public void ControlSpawnEnemigos(object sender, Etapa e)//Funcion que invoca los dialogos
    {
        OnEnemySpawn?.Invoke(sender, e);
    }
    public void Oleada(object sender, Etapa e)//Funcion que invoca las Oleadas
    {

        for (var i = 0; i < etapa.spawnsManagers.Length; i++)
        {
            if (i == etapa.spawnsManagers.Length - etapa.cantidadEtapas)
            {
                etapa.spawnsManagers[i].GetComponent<spawnmanager>().EventSubscribe(true);
                etapa.spawnVacio = etapa.spawnsManagers[i].transform.childCount;
            }
            else
            {
                etapa.spawnsManagers[i].GetComponent<spawnmanager>().EventSubscribe(false);
            }
        }
        etapa.enEspera = true;
        OnOleada?.Invoke(sender, e);
    }



    [System.Serializable]
    public class Etapa : EventArgs
    {
        public int enemigosPorSpawn;
        internal int cantidadEtapas, spawnVacio;
        public GameObject[] spawnsManagers;
        internal bool enEspera;
    }


}


