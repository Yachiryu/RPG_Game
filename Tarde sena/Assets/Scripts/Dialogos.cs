using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Texto",menuName = "Dialogos/Texto")]
[System.Serializable]
public class Dialogos : ScriptableObject 
{
    public Texto dialogo = new Texto();

    [System.Serializable]
    public class Texto : EventArgs
    {
        public Contenedor[] contenedor;

        [System.Serializable]
        public class Contenedor
        {
            public string texto;
            public AudioClip audioTexto;
        }
    }

}

