using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlataforme : MonoBehaviour
{
    public GameObject plataform;
    public float velocity;
    public GameObject maxposition;
    public GameObject minposition;
    bool andando, cerrar;
    public bool manual, puertaAbierta;

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
            if (!cerrar)
            {
                if (plataform.transform.position.y < maxposition.transform.position.y)
                {
                    plataform.transform.Translate(Vector3.up * Time.deltaTime * velocity);
                }
                else if (plataform.transform.position.y >= maxposition.transform.position.y)
                {
                    andando = false;
                }
            }
            if (cerrar)
            {
                if (plataform.transform.position.y > minposition.transform.position.y)
                {
                    plataform.transform.Translate(Vector3.down * Time.deltaTime * velocity);
                }
                else if (plataform.transform.position.y <= minposition.transform.position.y)
                {
                    andando = false;
                }
            }
        }
    }
    //si es true la puerta se cierra
    public void MovePlataform(bool closed)
    {
        andando = true;
        cerrar = closed; 
    }
}
