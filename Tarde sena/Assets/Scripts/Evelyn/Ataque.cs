using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims;

public class Ataque : MonoBehaviour
{
    [SerializeField] private Transform centroGolpe, armas;
    internal float esperaArma;
    private float tiempoEspera;
    
    internal bool onAttack;
    public float radioGolpe;

    private GameObject armaActual;
    
   
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
        bool atacar = false;
        for (int i = 0; i < armas.childCount; i++)
        {
            if (armas.GetChild(i).gameObject.activeInHierarchy)
            {
                atacar = true;
            }
        }
        if (atacar)
        {
            onAttack = true;
            armaActual = transform.GetComponent<RPGCharacterWeaponController>().twoHandSword;
            ItemProperties proItem = armaActual.GetComponent<Item>().itemProperties;
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
            Slot slot = armaActual.GetComponent<Item>().slot;
            slot.armaTwohandSword = armaActual;
            slot.UpdateUsoArma(proItem.velDesgateArma);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(centroGolpe.position, radioGolpe);
    }
}
