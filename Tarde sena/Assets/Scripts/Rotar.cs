using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotar : MonoBehaviour
{
    [SerializeField] internal float velocidadRotacion;
    [SerializeField] internal EjeRotacion ejeRotacion;
    [SerializeField] float anguloLimite;
    void Update()
    {
        switch (ejeRotacion)
        {
            case EjeRotacion.x:
                transform.Rotate(new Vector3(velocidadRotacion, 0, 0) * Time.deltaTime);
                break;
            case EjeRotacion.y:
                transform.Rotate(new Vector3(0, velocidadRotacion, 0)*Time.deltaTime);
                break;
            case EjeRotacion.z:
                transform.Rotate(new Vector3(0, 0, velocidadRotacion) * Time.deltaTime);
                break;
        }

        
    }

    public enum EjeRotacion 
    {
        x,
        y,
        z
    }
}
