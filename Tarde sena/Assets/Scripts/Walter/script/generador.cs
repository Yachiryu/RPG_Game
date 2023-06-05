using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generador : MonoBehaviour
{
    public GameObject[] enemisPrefb;
    public int currentEnemigosSpawn;
    public GameObject zonaspawn;
    public bool activo=false;


    void Start()
    {
        currentEnemigosSpawn = GameManager.Instance.etapa.enemigosPorSpawn;
    }

  
    public void Spawnear(object sender, GameManager.Etapa e)
    {
        currentEnemigosSpawn--;
        int aleatorio = Random.Range(0, enemisPrefb.Length);
        GameObject currentEnemy = Instantiate(enemisPrefb[aleatorio], transform.position, Quaternion.identity);
        currentEnemy.transform.parent = transform;
  
    }

 
}
