using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generador : MonoBehaviour
{
    public GameObject[] enemisPrefb;
    public int currentEnemigosSpawn;
    public bool boss;

    public void Spawnear()
    {
        currentEnemigosSpawn--;
        int aleatorio = Random.Range(0, enemisPrefb.Length);
        GameObject currentEnemy = Instantiate(enemisPrefb[aleatorio], transform.position, Quaternion.identity);
        currentEnemy.transform.parent = transform;
    }

}