using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particulas_Creafteo : MonoBehaviour
{
    [SerializeField] internal ParticleSystem particle_Crafting;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particle_Crafting.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            particle_Crafting.Stop();
        }
    }
}
