using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    [SerializeField]internal ParticleSystem particulas;
    [SerializeField]internal float esperaAtaque;
    bool ataque;
    float contadorEsperaAtaque;
    private void Update()
    {
        if (!ataque)
        {
            contadorEsperaAtaque += Time.deltaTime;
            if (contadorEsperaAtaque>=esperaAtaque)
            {
                ataque = true;
                contadorEsperaAtaque = 0;
            }
        }
    }
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("enemis") && ataque)
        {
            ataque = false;
            ParticleSystem.MainModule mainModule = particulas.main;
            mainModule.startLifetime = Vector3.Distance(transform.position,other.transform.position)/particulas.main.startSpeed.constant;
            Vector3 direccion = other.transform.position - transform.position;
            transform.rotation =  Quaternion.Euler(0,Mathf.Atan2(direccion.x,direccion.z)*Mathf.Rad2Deg,0);
            particulas.Play();
        }
    }
    
  
}
