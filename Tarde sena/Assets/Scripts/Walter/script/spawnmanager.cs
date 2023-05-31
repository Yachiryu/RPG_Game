using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{
 
    public generador[] spawnpoints;
    public GameObject boss;
    bool inicioOleada = true;

    void Start()
    {
        GameManager.Instance.OnEnemySpawn += GenerarEnemigos;
        spawnpoints = new generador[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnpoints[i] = transform.GetChild(i).GetComponent<generador>();

        }
    }

    void GenerarEnemigos(object sender, GameManager.Etapa e)
    {
        foreach (generador item in spawnpoints)
        {
            if (item.transform.childCount == 0)
            {
                if (item.currentEnemigosSpawn > 0)
                {
                    if (!inicioOleada)
                    {
                        item.Spawnear(sender, e);
                        break;
                    }
                    else
                    {
                        item.Spawnear(sender, e);
                    }
                }
                else
                {
                    e.spawnVacio--;
                    break;
                }
            }
        }
        inicioOleada = false;

        if (e.spawnVacio <= 0)
        {
            GameManager.Instance.etapa.cantidadEtapas--;
            if (e.cantidadEtapas == 0)
            {
                GameObject newBoss = Instantiate(boss, transform.position, Quaternion.identity);
                return;
            }
            inicioOleada = true;
            foreach (generador item in spawnpoints)
            {
                item.currentEnemigosSpawn = e.currentEnemigosPorSpawn;
            }
            GameManager.Instance.Oleada(this, GameManager.Instance.etapa);

        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnEnemySpawn -= GenerarEnemigos;
    }
}
