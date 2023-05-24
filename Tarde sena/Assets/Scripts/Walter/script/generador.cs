using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generador : MonoBehaviour
{
    public GameObject[] enemisPrefb;
    public GameObject zonaspawn;
    public bool activo=false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GenerarEnemigos();
    }
    void GenerarEnemigos()
    {
        int aleatorio = Random.Range(0, enemisPrefb.Length);
        if (activo==true)
        {
            
            Instantiate(enemisPrefb[aleatorio],zonaspawn.transform.parent);
            activo = false;
        }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        activo = true;
    }
}
