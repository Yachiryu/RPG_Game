using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generador : MonoBehaviour
{
    public GameObject[] enemisPrefb;
    public int currentEnemigosSpawn;
    [Tooltip("Activar si este Generador contendra un Boss")]
    public bool boss;
    internal bool spawnBloqueado;

    public void Spawnear()
    {
        currentEnemigosSpawn--;
        int aleatorio = Random.Range(0, enemisPrefb.Length);
        GameObject currentEnemy = Instantiate(enemisPrefb[aleatorio], transform.position, Quaternion.identity);
        currentEnemy.transform.parent = transform;
    }

    public IEnumerator CSpawnear(float seconds)
    {
        seconds *= 60;
        yield return new WaitForSeconds(seconds);
        currentEnemigosSpawn--;
        int aleatorio = Random.Range(0, enemisPrefb.Length);
        GameObject currentEnemy = Instantiate(enemisPrefb[aleatorio], transform.position, Quaternion.identity);
        currentEnemy.transform.parent = transform;
    }
}

