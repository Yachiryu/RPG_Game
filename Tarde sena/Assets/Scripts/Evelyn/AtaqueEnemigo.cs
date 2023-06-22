using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EmeraldAI;

public class AtaqueEnemigo : MonoBehaviour
{
    [SerializeField] private Transform centroGolpe;

    public float radioGolpe;

    public EmeraldAISystem iA;

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void AtacarEnemigo()
    {
        Collider[] objetos = Physics.OverlapSphere(centroGolpe.position, radioGolpe);



        foreach (Collider collisionador in objetos)
        {
            if (collisionador.CompareTag("Player"))
            {
                if (collisionador.GetComponent<Vida>().vida>0)
                {
                    collisionador.GetComponent<Vida>().ManejoVida(-iA.CurrentDamageAmount);

                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(centroGolpe.position, radioGolpe);
    }

}
