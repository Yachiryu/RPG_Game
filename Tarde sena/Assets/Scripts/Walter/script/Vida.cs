using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public int vida=100;
    public Slider barravida;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        barravida.GetComponent<Slider>().value = vida;

        if (vida<=0)
        {
            Debug.Log("Muelto");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag==("Peligro"))
        {
            vida--;
        }
    }
}
