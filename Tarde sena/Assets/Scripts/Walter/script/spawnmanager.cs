using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnmanager : MonoBehaviour
{
    public GameObject[] spawnpoints;
    public GameObject[] enemys;
    public int waveCount,wave;
    public int enemiType;
    public bool spawnting;
    public int enemisPawned;
    private remanager remanager;
    public GameObject nose;

    // Start is called before the first frame update
    void Start()
    {
        waveCount = 2;
        wave = 1;
        spawnting = false;
        enemisPawned = 0;
        nose = GameObject.Find("remanager");
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnting==false&& enemisPawned==remanager.defeatenemis)
        {
            StartCoroutine(SpawnWave(waveCount));
        }
    }
    IEnumerator SpawnWave(int wave)
    {
        spawnting = true;
        yield return new WaitForSeconds(4);
        for (int i = 0; i < wave; i++)
        {
            Spawnenemis(wave);
            yield return new WaitForSeconds(2);
        }
        wave += 1;
        waveCount += 2;
        spawnting = false;
        yield break;
    }
    public void Spawnenemis(int wave)
    {
        int spawnPos = Random.Range(0, 4);
        if (wave == 1)
        {
            enemiType = 1;
        }
        else if (wave <= 4)
        {
            enemiType = Random.Range(0, 2);
        }
        else
        {
            enemiType = Random.Range(0, 3);
        }

        Instantiate(enemys[enemiType], spawnpoints[spawnPos].transform.position, spawnpoints[spawnPos].transform.rotation);
        enemisPawned += 1;
    }
}
