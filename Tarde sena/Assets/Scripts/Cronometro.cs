using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cronometro : MonoBehaviour
{
    public TextMeshProUGUI textoMinutos, textoSegundos, titulo, dosPuntos;
    int minutos, segundos;
    [SerializeField] string tituloEsperaOleada;

    private void Start()
    {
        GameManager.Instance.OnOleada += FCronometro;
    }
    public void FCronometro(object sender, GameManager.Etapa e)
    {
        if (e.cantidadEtapas > 0)
        {
            StartCoroutine(CCronometro());
        }
    }
    IEnumerator CCronometro()
    {
        Setear("texto");
        minutos = GameManager.Instance.esperaDuracionOleadasMin;
        segundos = GameManager.Instance.esperaDuracionOleadasSeg;

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
        Setear("");
        GameManager.Instance.etapa.enEspera = false;
        GameManager.Instance.ControlSpawnEnemigos(this, GameManager.Instance.etapa);
    }

    void Setear(string texto)
    {
        if (texto != "")
        {
            titulo.text = tituloEsperaOleada;
            dosPuntos.text = ":";
            textoMinutos.text = GameManager.Instance.esperaDuracionOleadasMin.ToString();
            textoSegundos.text = GameManager.Instance.esperaDuracionOleadasSeg.ToString();
        }
        else
        {
            titulo.text = "";
            dosPuntos.text = "";
            textoMinutos.text = "";
            textoSegundos.text = "";
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnOleada -= FCronometro;
    }
}
