using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class generador : MonoBehaviour
{
    
    public MapsEnemys[] mapsEnemys;
    public GameObject[] enemisPrefb;
    public int currentEnemigosSpawn;
    [Tooltip("Activar si este Generador contendra un Boss")]
    public bool boss;
    internal bool spawnBloqueado, corrutinaCSpawnear;
    internal spawnmanager padre;

    private void Start()
    {
        padre = transform.parent.GetComponent<spawnmanager>();
        foreach (var item in mapsEnemys)
        {
            if (item.sceneName == SceneManager.GetActiveScene().name)
            {
                if (!boss)
                    enemisPrefb = item.prefabEnemigos;
                else
                    enemisPrefb = item.boss;
            }
        }
    }
    public IEnumerator CSpawnear(float seconds)
    {
        if (!corrutinaCSpawnear)
        {
            if (!spawnBloqueado)
            {
                if (currentEnemigosSpawn > 0)
                {
                    corrutinaCSpawnear = true;//hasta que esta variable no este false no puede volver a entrar a la corrutina
                    //seconds *= 60;//Convertir minutos a segundos
                    if (transform.childCount != 0)//verificar si tiene hijos para activarlos
                    {
                        if (!spawnBloqueado)
                        {
                            transform.GetChild(0).gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        yield return new WaitForSeconds(seconds);
                        int aleatorio = Random.Range(0, enemisPrefb.Length);
                        GameObject currentEnemy = Instantiate(enemisPrefb[aleatorio], transform.position, Quaternion.identity);
                        currentEnemy.transform.parent = transform;
                        if (spawnBloqueado)
                        {
                            transform.GetChild(0).gameObject.SetActive(false);
                        }
                    }
                    if (padre.paraOleadas)//Solo entra aqui si el spawnManager es para Oleadas
                    {
                        currentEnemigosSpawn--;//Disminuye la cantidad de enemigos que el generador puede spawnear
                    }
                    corrutinaCSpawnear = false;
                }
                else
                {
                    GameManager.Instance.etapa.spawnVacio--;
                    padre.GenerarEnemigos(this, GameManager.Instance.etapa);
                }
            }
        }
    }
    

    [System.Serializable]
    public class MapsEnemys
    {
        public string sceneName;
        public GameObject[] boss;
        public GameObject[] prefabEnemigos;
       
    }
}
