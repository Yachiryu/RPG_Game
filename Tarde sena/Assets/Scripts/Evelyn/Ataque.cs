using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims;

public class Ataque : MonoBehaviour
{
    [SerializeField] private Transform centroGolpe;
    internal float esperaArma;
    private float tiempoEspera;
    
    internal bool onAttack;
     public float radioGolpe;
    
   
    void Start()
    {
        
    }

    
    void Update()
    {
        if (onAttack)
        {
            tiempoEspera += Time.deltaTime;
        
            if(tiempoEspera >= esperaArma)
            {
                onAttack = false;
                tiempoEspera = 0;
            }
        } 
    }

    public void Attack()
    {
        onAttack = true;
        ItemProperties proItem = transform.GetComponent<RPGCharacterWeaponController>().twoHandSword.GetComponent<Item>().itemProperties;
        esperaArma = proItem.ColDown;

        Collider[] objetos = Physics.OverlapSphere(centroGolpe.position, radioGolpe);

        foreach (Collider collisionador in objetos)
        {
            if(collisionador.CompareTag("enemis"))
            {
                if (!collisionador.GetComponent<EmeraldAI.EmeraldAISystem>().IsDead)
                {
                    collisionador.GetComponent<EmeraldAI.EmeraldAISystem>().Damage(proItem.daño, null, transform);

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
