using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public int vida = 100;
    public Slider barravida;

    void Start()
    {
        StartCoroutine(Mientras());
    }

    IEnumerator Mientras()
    {
        yield return new WaitForSeconds(5);
        Danio(100);
    
    }

    public void Danio(int danio)
    {
        vida -= danio;
        if (vida <= 0)
        {
            Transform padre = transform.parent.parent;
            transform.parent = null;
            padre.GetComponent<spawnmanager>().GenerarEnemigos(this, GameManager.Instance.etapa);
            //GameManager.Instance.ControlSpawnEnemigos(this, GameManager.Instance.etapa);
            Destroy(gameObject, 0.5f);//colocar tiempo de cuando muere

        }
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == ("Peligro"))
    //    {
    //        vida--;
    //    }
    //}
}
