using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : MonoBehaviour
{
    public ItemProperties[] objectsOfRandomChest;
    [SerializeField] private int maxObjects;
    public bool boss;
    List<int> objetos = new List<int>();
    public ItemProperties[] objetosFijos;

    public MeshCofre meshBossCofre;
    bool cofreAbierto;



    private void Start()
    {
        //cofreAbierto = true;
        maxObjects = Mathf.Clamp(maxObjects, 1, objectsOfRandomChest.Length);
        if (boss)
        {
            GetComponent<MeshFilter>().mesh = meshBossCofre.meshCofre;
            transform.GetChild(0).GetComponent<MeshFilter>().mesh = meshBossCofre.meshTapaCofre;
        }
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

            while (objetos.Count < maxObjects)
            {

                int numberRandomOfList = Random.Range(0, objectsOfRandomChest.Length);
                for (int i = 0; i < objetos.Count; i++)
                {
                    if (numberRandomOfList == objetos[i])
                    {
                        numberRandomOfList = Random.Range(0, objectsOfRandomChest.Length);
                        i = 0;
                    }
                }
                objetos.Add(numberRandomOfList);
            }

            bool darObjeto = false;

            for (int i = 0; i < objetos.Count; i++)
            {
                int numberRandom = Random.Range(0, Mathf.CeilToInt(100f / objectsOfRandomChest[objetos[i]].porcentajeDeSalida));
                if (numberRandom == 0)
                {
                    darObjeto = true;
                    int num = Random.Range(1, 4);
                    for (int j = 0; j < num; j++)
                    {
                        other.GetComponent<Inventory>().AddItem(objectsOfRandomChest[objetos[i]]);
                    }
                }
            }
            if (!darObjeto)
            {
                other.GetComponent<Inventory>().AddItem(objectsOfRandomChest[0]);
            }
            objetos.RemoveRange(0, objetos.Count);
        }
    }

    [System.Serializable]
    public class MeshCofre
    {
        public Mesh meshCofre;
        public Mesh meshTapaCofre;
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnOleada -= AbrirCofre;

    }
}