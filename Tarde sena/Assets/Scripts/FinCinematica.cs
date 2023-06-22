using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinCinematica : MonoBehaviour
{
    public string nombreEscena;
    //public int indice;

    private void Start()
    {
        StartCoroutine(CCambiarEscena());
    }

    IEnumerator CCambiarEscena()
    {
        yield return new WaitForSeconds(25f);
        SceneManager.LoadScene(nombreEscena);
    }
}
