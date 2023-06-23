using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torreta : MonoBehaviour
{
    [SerializeField]internal ParticleSystem particulas;
    [SerializeField]internal float esperaAtaque;
    [SerializeField]internal int cantidadAtaques;
    public AudioSource fuenteAudio;
    public AudioClip sonidoDanio, sonidoDestruccion;

    bool ataque;
    float contadorEsperaAtaque;
    int contadorAtaques;
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
        if (other.CompareTag("enemis"))
        {
            if (ataque)
            {
                ataque = false;
                ReproducirSonido(sonidoDanio);
                particulas.Play();
                contadorAtaques++;
                if (contadorAtaques>=cantidadAtaques)
                {
                    ReproducirSonido(sonidoDestruccion);
                    Destroy(transform.parent.gameObject,sonidoDestruccion.length);
                }
                if (other.CompareTag("enemis") && !other.GetComponent<EmeraldAI.EmeraldAISystem>().IsDead)
                {
                    other.GetComponent<EmeraldAI.EmeraldAISystem>().Damage(5, null, transform);
                }
            }
            ParticleSystem.MainModule mainModule = particulas.main;
            mainModule.startLifetime = Vector3.Distance(transform.position,other.transform.position)/particulas.main.startSpeed.constant;
            Vector3 direccion = other.transform.position - transform.position;
            transform.rotation =  Quaternion.Euler(0,Mathf.Atan2(direccion.x,direccion.z)*Mathf.Rad2Deg,0);
        }
    }
    
    void ReproducirSonido(AudioClip audioClip)
    {
        fuenteAudio.clip = audioClip;
        fuenteAudio.Play();
    }
}
