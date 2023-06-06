using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{
    public generador[] spawnpoints;

    bool inicioOleada = true , exitZonaSpawn;

    [Tooltip("Activar si este SpawnManager sera parte de las oleadas principales del juego")]
    public bool paraOleadas;

    [Range(1,5)][Tooltip("Rango en minutos en que el enemigo aparecera, solo funciona si este SpawnManager no es parte de las oleadas principales del juego")]
    public float rangoAparicion; 

    int bossIndex;
    void Start()
    {
        //GameManager.Instance.OnEnemySpawn += GenerarEnemigos;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !paraOleadas)
        {
            print("Entre zona spawn");
            inicioOleada = true;
            exitZonaSpawn = false;
            GenerarEnemigos();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !paraOleadas)
        {
            exitZonaSpawn = true;
        }
    }
    public void GenerarEnemigos(object sender = null, GameManager.Etapa e = null)
    {
        if (!paraOleadas)
        {
            print("No oleada, rellenar");
            RellearSpawnPoints();
            if (exitZonaSpawn )
            {
                return;
            }
        }
        


        //foreach (generador item in spawnpoints)
        //{
            
            //if (item.transform.childCount == 0)
            //{
                //if (item.currentEnemigosSpawn > 0)
                //{
                    if (!inicioOleada)
                    {
                        List<int> listaSpawnsLibres = new List<int>();
                        for (int i = 0; i < spawnpoints.Length; i++)
                        {
                            if (spawnpoints[i].transform.childCount == 0 && !spawnpoints[i].boss)
                            {
                                if (spawnpoints[i].currentEnemigosSpawn > 0)
                                {
                                    listaSpawnsLibres.Add(i);
                                }
                                else
                                {
                                    if (!spawnpoints[i].spawnBloqueado)
                                    {
                                        spawnpoints[i].spawnBloqueado = true;
                                        e.spawnVacio--;
                                    }
                                }
                            }
                        }
                        if (listaSpawnsLibres.Count>0)
                        {
                            print($"cantidad items: {listaSpawnsLibres.Count}");
                            foreach (var item in listaSpawnsLibres)
                            {
                                print(item);
                            }
                            int randomNumber = Random.Range(0, listaSpawnsLibres.Count);
                            //if (item.currentEnemigosSpawn > 0)
                            //{
                            generador elegido = spawnpoints[listaSpawnsLibres[randomNumber]];
                            print($"elegido: {elegido.name}");
                                //if (!elegido.boss)
                                //{
                                    if (paraOleadas)
                                        StartCoroutine(elegido.CSpawnear(0));//StartCoroutine(item.CSpawnear(0));
                                    else
                                        StartCoroutine(elegido.CSpawnear(Random.Range(0.5f, rangoAparicion)));//StartCoroutine(item.CSpawnear(Random.Range(0.5f, rangoAparicion)));
                                        //item.Spawnear();
                                    //break;
                                //}
                            //}
                            //else
                            //{
                            //    e.spawnVacio--;
                            //    //break;
                            //}
                        }
                    }
                    else
                    {
                        foreach (generador item in spawnpoints)
                        {
                            if (!item.boss)
                            {
                                if (paraOleadas)
                                    StartCoroutine(item.CSpawnear(0));
                                else
                                    StartCoroutine(item.CSpawnear(Random.Range(0.5f, rangoAparicion)));
                                //item.Spawnear();
                            }
                        }
                    }
                //}
                //else
                //{
                //    e.spawnVacio--;
                //    break;
                //}
            //}
        //}
        inicioOleada = false;
        if (paraOleadas)
        {
            if (e.spawnVacio == 1)
            {
                if (e.cantidadEtapas == 1)
                {
                    spawnpoints[bossIndex].Spawnear();

                }
                else
                {
                    e.spawnVacio--;
                }
            }
            if (e.spawnVacio <= 0)
            {
                inicioOleada = true;
                RellearSpawnPoints();
                GameManager.Instance.etapa.cantidadEtapas--;
                GameManager.Instance.Oleada(this, GameManager.Instance.etapa);
            }
        }
    }

    void RellearSpawnPoints()
    {
        foreach (generador item in spawnpoints)
        {
            if (item.boss)
            {
                item.currentEnemigosSpawn = 1;
            }
            else
            {
                item.currentEnemigosSpawn = GameManager.Instance.etapa.enemigosPorSpawn;
            }
        }
    }

    public void EventSubscribe(bool Subscribe)
    {
        if (Subscribe)
            GameManager.Instance.OnEnemySpawn += GenerarEnemigos;
        else
            GameManager.Instance.OnEnemySpawn -= GenerarEnemigos;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnEnemySpawn -= GenerarEnemigos;
    }
}