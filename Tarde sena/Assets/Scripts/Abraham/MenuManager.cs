using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPausa;
    private bool on = false; // Variable para controlar el estado del men� de pausa

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ControladorDelMenu(); // Llama al m�todo para abrir o cerrar el men� de pausa
            ControladorDelTiempo(); // Llama al m�todo para pausar o reanudar el tiempo
        }
    }

    public void ControladorDelMenu()
    {
        on = !on; // Cambia el estado del men� de pausa (abierto o cerrado)
        menuPausa.SetActive(on); // Activa o desactiva el men� de pausa seg�n el estado actual
    }

    public void ControladorDelTiempo()
    {
        if (on)
        {
            Time.timeScale = 0f; // Pausa el tiempo estableciendo Time.timeScale en 0
        }
        else
        {
            Time.timeScale = 1f; // Reanuda el tiempo estableciendo Time.timeScale en 1
        }
    }
}
