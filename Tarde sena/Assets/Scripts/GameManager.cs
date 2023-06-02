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
    [SerializeField]internal int esperaDuracionOleadasSeg;// duracionOleadasMin, duracionOleadasSeg;// cantidadEtapas;
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
        }

    }

    private void Start()
    {
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
        etapa.enEspera = true;
        etapa.spawnVacio = etapa.spawns.childCount;
        //etapa.currentEnemigosPorSpawn = etapa.enemigosPorSpawn;
        OnOleada?.Invoke(sender, e);
        //if (e.cantidadEtapas > 0)
        //{
        //    OnOleada?.Invoke(sender, e);
        //    etapa.spawnVacio = etapa.spawns.childCount;
        //    etapa.currentEnemigosPorSpawn = etapa.enemigosPorSpawn;
        //}
    }



    [System.Serializable]
    public class Etapa : EventArgs
    {
        public int cantidadEtapas, enemigosPorSpawn, spawnVacio;
        public Transform spawns;
        internal bool enEspera;
    }


}


