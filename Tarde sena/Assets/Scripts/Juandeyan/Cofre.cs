using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : MonoBehaviour
{
    public ItemProperties[] objectsOfRandomChest;
    public ItemProperties[] objetosFijos;
    [SerializeField] private int maxObjects;
    [SerializeField] private bool boss;
    bool cofreAbierto;

    private void Start()
    {
        GameManager.Instance.OnOleada += AbrirCofre;
    }

    void AbrirCofre(object sender, GameManager.Etapa e)
    {
        if (e.enEspera == true)
        {
            cofreAbierto = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player" && Input.GetButtonDown("Interaction") && cofreAbierto)
        {
            cofreAbierto = false;

            if (boss)
            {
                foreach (var item in objetosFijos)
                {
                    other.GetComponent<Inventory>().AddItem(item);
                }
            }

            int numberRandomOfList = Random.Range(0, objectsOfRandomChest.Length);

            print(Mathf.CeilToInt(100f / objectsOfRandomChest[numberRandomOfList].porcentajeDeSalida));


            int numberRandom = Random.Range(0, Mathf.CeilToInt(100f / objectsOfRandomChest[numberRandomOfList].porcentajeDeSalida));

            if (numberRandom == 0)
            {
                other.GetComponent<Inventory>().AddItem(objectsOfRandomChest[numberRandomOfList]);

            }

        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnOleada -= AbrirCofre;

    }
}
