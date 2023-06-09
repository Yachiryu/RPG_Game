using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotar : MonoBehaviour
{
    [SerializeField] internal float velocidadRotacion;
    [SerializeField] internal EjeRotacion ejeRotacion;

    public AnimationCurve funcionMovimiento;
    public AnimationCurve rotacionConstante;
    public float anguloFinal;
    float equis;

    private float funcion;
    public bool rotarConstantemente, iniciarRotacion;

    private Vector3 anguloInicial;

    private void Start()
    {
        if (rotarConstantemente)
        {
            iniciarRotacion = true;
        }
            
    }
    void Update()
    {
        if (iniciarRotacion)
        {
            if (rotarConstantemente)
            {
                funcion = rotacionConstante.Evaluate(equis);
            }
            else
            {
                funcion = funcionMovimiento.Evaluate(equis);
            }
            equis = equis + Time.deltaTime;

            switch (ejeRotacion)
            {
                case EjeRotacion.x:
                    transform.localRotation = Quaternion.Euler(Vector3.LerpUnclamped(anguloInicial,new Vector3(anguloFinal,0,0),funcion));
                    //transform.Rotate(new Vector3(velocidadRotacion, 0, 0) * Time.deltaTime);
                    break;
                case EjeRotacion.y:
                    transform.localRotation = Quaternion.Euler(Vector3.LerpUnclamped(anguloInicial,new Vector3(0, anguloFinal, 0),funcion));
                    break;
                case EjeRotacion.z:
                    transform.localRotation = Quaternion.Euler(Vector3.LerpUnclamped(anguloInicial,new Vector3(0,0, anguloFinal),funcion));
                    break;
            }

        }


    }

    public void EmpezarRotacion()
    {
        anguloInicial = transform.rotation.eulerAngles;
        iniciarRotacion = true;
    }

    public enum EjeRotacion 
    {
        x,
        y,
        z
    }
}
