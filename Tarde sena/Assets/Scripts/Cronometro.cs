using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cronometro : MonoBehaviour
{
    TextMeshProUGUI textoMinutos, textoSegundos, titulo, dosPuntos;
    int minutos, segundos;
    [SerializeField] string tituloEsperaOleada, tituloOleada;

    private void Start()
    {

        titulo = transform.Find("Titulo").GetComponent<TextMeshProUGUI>();
        dosPuntos = transform.Find(":").GetComponent<TextMeshProUGUI>();
        textoMinutos = transform.Find("Minutos").GetComponent<TextMeshProUGUI>();
        textoSegundos = transform.Find("Segundos").GetComponent<TextMeshProUGUI>();
        titulo.text = tituloEsperaOleada;
        dosPuntos.text = ":";
        textoMinutos.text = GameManager.Instance.esperaDuracionOleadasMin.ToString();
        textoSegundos.text = GameManager.Instance.esperaDuracionOleadasSeg.ToString();

    }
    public void FCronometro()
    {
        StartCoroutine(CCronometro());
    }
    IEnumerator CCronometro()
    {
        
        //while (GameManager.Instance.cantidadEtapas > 0)
        //while (GameManager.Instance.etapa.cantidadEtapas > 0)
        //{
            //if (GameManager.Instance.cantidadEtapas % 2 == 0)
            //if (!GameManager.Instance.etapa.BoolEstapa())
            //{
                titulo.text = tituloEsperaOleada;
                minutos = GameManager.Instance.esperaDuracionOleadasMin;
                segundos = GameManager.Instance.esperaDuracionOleadasSeg;
            //}
            //else
            //{
                //titulo.text = tituloOleada;
                //minutos = GameManager.Instance.duracionOleadasMin;
                //segundos = GameManager.Instance.duracionOleadasSeg;
            //}
            while (minutos > 0 || segundos > 0)
            {
                while (segundos > 0)
                {
                    yield return new WaitForSeconds(1f);
                    segundos -= 1;
                    textoSegundos.text = segundos.ToString();
                }
                if (minutos != 0 && segundos <= 0)
                {
                    segundos = 60;
                    textoSegundos.text = segundos.ToString();
                    minutos -= 1;
                    textoMinutos.text = minutos.ToString();
                }
            }
            dosPuntos.text = "";
            textoMinutos.text = "";
            textoSegundos.text = "";
        //GameManager.Instance.etapa.cantidadEtapas -= 1;
            GameManager.Instance.ControlSpawnEnemigos(this, GameManager.Instance.etapa);
        //}

    }
}
