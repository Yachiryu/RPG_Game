using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlataforme : MonoBehaviour
{
    public bool manual, puertaAbierta;
    [Range(0.001f, 0.8f)] public float velocity;
    public AnimationCurve curvaMovimiento;
    public GameObject plataform;
    public Transform closedPosition;
    public Transform openPosition;
    bool andando, cerrar;

    public AudioSource fuenteAudio;
    //public AudioClip sonidoPuertaFuncionando;


    Vector3 posicionInicial;
    float contador;
    private void Start()
    {
        fuenteAudio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && manual)
        {
            MovePlataform(puertaAbierta);
            puertaAbierta = !puertaAbierta;
        }
    }

    private void Update()
    {
        if (andando)
        {
            //fuenteAudio.clip = sonidoPuertaFuncionando;
            print("Abriendo");
            fuenteAudio.Play();
            contador += velocity;
            if (!cerrar)
            {
                plataform.transform.position = Vector3.Lerp(posicionInicial, openPosition.position, curvaMovimiento.Evaluate(contador));
            }
            if (cerrar)
            {
                plataform.transform.position = Vector3.Lerp(posicionInicial,closedPosition.position, curvaMovimiento.Evaluate(contador));
            }
            if (contador>=1)
            {
                //fuenteAudio.Stop();
                contador = 0;
                //fuenteAudio.Pause();
                andando = false;
            }
        }
    }
    //si es true la puerta se cierra
    public void MovePlataform(bool closed)
    {
        posicionInicial = plataform.transform.position;
        andando = true;
        cerrar = closed; 
    }
}
