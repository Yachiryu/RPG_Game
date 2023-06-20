using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotar : MonoBehaviour
{
    [SerializeField][Range(-0.8f,0.8f)] internal float velocidadRotacion;
    [SerializeField] internal EjeRotacion ejeRotacion;

    public AnimationCurve funcionMovimiento;
    public AnimationCurve rotacionConstante;
    public float anguloFinal;
    float equis;

    private float funcion;
    public bool rotarConstantemente;

    bool iniciarRotacion;

    private Vector3 anguloInicial;

    private void Start()
    {
        if (rotarConstantemente)
        {
            iniciarRotacion = true;
            anguloFinal = 360;
        }
            
    }
    void Update()
    {
        if (iniciarRotacion)
        {
            if (rotarConstantemente)
                funcion = rotacionConstante.Evaluate(equis);
            else
                funcion = funcionMovimiento.Evaluate(equis);

            equis = equis + velocidadRotacion;

            switch (ejeRotacion)
            {
                case EjeRotacion.x:
                    transform.localRotation = Quaternion.Euler(Vector3.LerpUnclamped(anguloInicial,new Vector3(anguloFinal,0,0),funcion));
                    break;
                case EjeRotacion.y:
                    transform.localRotation = Quaternion.Euler(Vector3.LerpUnclamped(anguloInicial,new Vector3(0, anguloFinal, 0),funcion));
                    break;
                case EjeRotacion.z:
                    transform.localRotation = Quaternion.Euler(Vector3.LerpUnclamped(anguloInicial,new Vector3(0,0, anguloFinal),funcion));
                    break;
            }
            if (Mathf.Abs(funcion)>=1)
            {
                equis = 0;
                if (!rotarConstantemente)
                {
                    iniciarRotacion = false;
                }
            }

        }


    }

    public void EmpezarRotacion(float angFinal, bool invertirVelocidad = false)
    {
        anguloInicial = transform.rotation.eulerAngles;
        //anguloInicial = new Vector3(90,0,0);
        anguloFinal = angFinal;
        if (invertirVelocidad)
        {
            velocidadRotacion *= -1;

        }
        iniciarRotacion = true;
    }

    public enum EjeRotacion 
    {
        x,
        y,
        z
    }
}
