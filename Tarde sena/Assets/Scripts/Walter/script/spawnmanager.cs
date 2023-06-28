using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{
    public DoorPlataforme[] doorPlataformes;
    public generador[] spawnpoints;
    public GameObject cofreBoss;

    bool spawnearTodo = true, inicioOleada, puertaCerrada;

    [Tooltip("Activar si este SpawnManager sera parte de las oleadas principales del juego")]
    public bool paraOleadas;

    [Space]
    [Range(5, 60)]
    [Tooltip("Rango en minutos en que el enemigo aparecera, solo funciona si este SpawnManager no es parte de las oleadas principales del juego")]
    public float rangoAparicion;
    [Range(1, 5)]
    [Tooltip("Tiempo en minutos en que desapareceran los enemigos si el player sale de la zona de spawn, solo funciona si este SpawnManager no es parte de las oleadas principales del juego")]
    public float tiempoEsperaDesaparicion;



    int bossIndex = -1;
    void Start()
    {

        spawnpoints = new generador[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            spawnpoints[i] = transform.GetChild(i).GetComponent<generador>();
            if (spawnpoints[i].boss)
                bossIndex = i;
        }
        RellearSpawnPoints();

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && paraOleadas)
        {
            if (inicioOleada && !puertaCerrada)
            {
                puertaCerrada = true;
                Puertas(true);
                GenerarEnemigos(this, GameManager.Instance.etapa);
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !paraOleadas)
        {

            print("Entre zona spawn");
            puertaCerrada = true;
            spawnearTodo = true;

            BloquearSpawns(false);
            GenerarEnemigos();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !paraOleadas)
        {
            BloquearSpawns(true);
            StartCoroutine(CEsperaDesaparecerEnemigos());
        }
    }
    public void GenerarEnemigos(object sender = null, GameManager.Etapa e = null)
    {
        if (paraOleadas && !inicioOleada)
        {
            inicioOleada = true;
            StartCoroutine(CDañarPlayer());
        }
        if (puertaCerrada)
        {
            if (spawnearTodo)
            {
                for (var i = 0; i < spawnpoints.Length; i++)
                {
                    if (!spawnpoints[i].boss)
                        GenerarSpawn(i);
                }
            }
            spawnearTodo = false;
            if (paraOleadas)
            {
                if (e.spawnVacio == 1 && bossIndex >= 0)
                {
                    if (e.cantidadEtapas == 1)
                        GenerarSpawn(bossIndex);//Invoca El Boss
                    else
                        e.spawnVacio--;
                }
                if (e.spawnVacio <= 0)
                {
                    spawnearTodo = true;
                    Puertas(false);
                    if (bossIndex >= 0)
                    {
                        Instantiate(cofreBoss,spawnpoints[bossIndex].transform.position,Quaternion.identity);
                    }
                    RellearSpawnPoints();
                    GameManager.Instance.etapa.cantidadEtapas--;//Disminuye la cantidad de etapas
                    GameManager.Instance.Oleada(this, GameManager.Instance.etapa);//Invoca la siguiente etapa
                }
            }
        }
    }

    public void RellearSpawnPoints()
    {
        foreach (generador item in spawnpoints)
        {
            if (item.boss)
                item.currentEnemigosSpawn = 1;
            else
                item.currentEnemigosSpawn = GameManager.Instance.etapa.enemigosPorSpawn;
        }
    }

    public void GenerarSpawn(int index)
    {
        //if (!spawnpoints[index].corrutinaCSpawnear)
        //{
            if (paraOleadas)
                StartCoroutine(spawnpoints[index].CSpawnear(0));
            else
                StartCoroutine(spawnpoints[index].CSpawnear(Random.Range(5f, rangoAparicion)));
        //}
    }

    void BloquearSpawns(bool bloquear)
    {
        foreach (var item in spawnpoints)
        {
            item.spawnBloqueado = bloquear;
        }
    }

    void Puertas(bool estadoPuerta)
    {
        foreach (var item in doorPlataformes)
        {
            item.MovePlataform(estadoPuerta);
        }
    }

    public void EventSubscribe(bool Subscribe)
    {
        if (Subscribe)
            GameManager.Instance.OnEnemySpawn += GenerarEnemigos;
        else
            GameManager.Instance.OnEnemySpawn -= GenerarEnemigos;
    }

    //Corrutina para hacer daño al jugador si no esta en el lugar una vez acabado el tiempo de espera para empezar la oleada
    IEnumerator CDañarPlayer()
    {
        yield return new WaitForSeconds(1);
        Vida jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Vida>();
        while (inicioOleada && !puertaCerrada)
        {
            yield return new WaitForSeconds(1);
            jugador.ManejoVida(-2);
        }
    }

    //Solo para SpawnManager que no sean para Oleadas
    IEnumerator CEsperaDesaparecerEnemigos()
    {
        tiempoEsperaDesaparicion *= 60; //convertir en segundos
        yield return new WaitForSeconds(tiempoEsperaDesaparicion);
        foreach (var item in spawnpoints)
        {
            if (item.transform.childCount != 0)
                item.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnEnemySpawn -= GenerarEnemigos;
    }
}