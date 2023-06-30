using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPGCharacterAnims;
using RPGCharacterAnims.Lookups;
using RPGCharacterAnims.Actions;

public class Ataque : MonoBehaviour
{
    [SerializeField] public Transform centroGolpe, armas;
    internal float esperaArma;
    private float tiempoEspera;
    
    internal bool onAttack;
    public float radioGolpe;

    private GameObject armaActual;

    Slot slot;

    public Slider barraDesgasteArma;
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

            bool golpe = true;
            foreach (Collider collisionador in objetos)
            {
                if(collisionador.CompareTag("enemis"))
                {
                    if (!collisionador.GetComponent<EmeraldAI.EmeraldAISystem>().IsDead)
                    {
                        collisionador.GetComponent<EmeraldAI.EmeraldAISystem>().Damage(proItem.danio, null, transform);
                    }
                    if (golpe)
                    {
                        golpe = false;
                        slot = armaActual.GetComponent<Item>().slot;
                        slot.armaTwohandSword = armaActual;
                        slot.UpdateUsoArma(proItem.velDesgateArma);
                        barraDesgasteArma.maxValue = slot.slotProperties.desgasteArma;
                        barraDesgasteArma.value = slot.muricionArma;
                    }
                }
            }
        }
    }

    public void DestruirArma()
    {
        transform.GetComponent<RPGCharacterWeaponController>().twoHandSword = null;
        for (int i = 0; i < armas.childCount; i++)
        {
            if (armas.GetChild(i).gameObject.activeInHierarchy)
            {
                armas.GetChild(i).gameObject.SetActive(false);
            }
        }
        GetComponent<RPGCharacterController>().TryStartAction(HandlerTypes.SwitchWeapon, new SwitchWeaponContext());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(centroGolpe.position, radioGolpe);
    }
}
