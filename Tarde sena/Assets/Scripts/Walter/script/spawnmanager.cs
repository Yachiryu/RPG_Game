using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{
    public generador[] spawnpoints;

    public GameObject boss;
    bool inicioOleada = true;

    int bossIndex;
    void Start()
    {
        GameManager.Instance.OnEnemySpawn += GenerarEnemigos;
        spawnpoints = new generador[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnpoints[i] = transform.GetChild(i).GetComponent<generador>();
            if (spawnpoints[i].boss)
            {
                bossIndex = i;
            }
        }
        RellearSpawnPoints();
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
                        if (!item.boss)
                        {
                            item.Spawnear();
                            break;
                        }
                    }
                    else
                    {
                        if (!item.boss)
                            item.Spawnear();
                    }
                }
                else
                {
                    e.spawnVacio--;
                    break;
                }
            }
        }
        if (e.spawnVacio == 1)
        {
            if (e.cantidadEtapas == 1)
            {
                //spawnpoints[bossIndex].enemisPrefb[0].GetComponent<Vida>().jefe = true;
                spawnpoints[bossIndex].Spawnear();
            }
            else
            {
                e.spawnVacio--;
            }
        }

        inicioOleada = false;

        if (e.spawnVacio <= 0)
        {
            inicioOleada = true;
            RellearSpawnPoints();
            GameManager.Instance.etapa.cantidadEtapas--;
            GameManager.Instance.Oleada(this, GameManager.Instance.etapa);
        }
    }

    void RellearSpawnPoints()
    {
        foreach (generador item in spawnpoints)
        {
            if (item.boss)
            {
                //item.enemisPrefb[0].GetComponent<Vida>().jefe = false;
                item.currentEnemigosSpawn = 1;
            }
            else
            {
                item.currentEnemigosSpawn = GameManager.Instance.etapa.enemigosPorSpawn;
            }
        }
    }
}