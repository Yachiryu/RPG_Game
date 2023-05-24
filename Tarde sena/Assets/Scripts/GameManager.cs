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

    [Header("Eventos")]
    public UnityEvent OnCronometro;
    public event EventHandler<Dialogos.Texto> OnDialogo; 
    public event EventHandler<Etapa> OnEnemySpawn; 

    
    internal bool enDialogo;

    
    private void Awake()
    {
        etapa.cantidadEtapas *= 2;
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
        OnCronometro?.Invoke();
    }

    public void DialogoDuranteJuego(object sender, Dialogos.Texto e)//Funcion que invoca los dialogos
    {
        OnDialogo?.Invoke(sender, e);
    }

    public void ControlSpawnEnemigos(object sender, Etapa e)//Funcion que invoca los dialogos
    {
        OnEnemySpawn?.Invoke(sender, e);
    }

    [System.Serializable]
    public class Etapa : EventArgs
    {
        public int cantidadEtapas, enemigosPorSpawn;
       
        public bool BoolEstapa()
        {
            if (cantidadEtapas % 2 == 0)
                return false;
            else
                return true;
        }
    }

}


