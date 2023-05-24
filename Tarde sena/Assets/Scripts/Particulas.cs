using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particulas : MonoBehaviour
{
    [SerializeField]internal ParticleSystem particulas;

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Enemigo" )
        { 
            ParticleSystem.MainModule mainModule = particulas.main;
            mainModule.startLifetime = Vector3.Distance(transform.position,other.transform.position)/particulas.main.startSpeed.constant;
            Vector3 direccion = other.transform.position - transform.position;
            transform.rotation =  Quaternion.Euler(0,Mathf.Atan2(direccion.x,direccion.z)*Mathf.Rad2Deg,0);
            //ParticleSystem.ShapeModule shapeModule = particulas.shape;
            //shapeModule.rotation = new Vector3(0,Mathf.Atan2(direccion.x,direccion.z)*Mathf.Rad2Deg,0);
            particulas.Play();
        }
    }
    
  
}
