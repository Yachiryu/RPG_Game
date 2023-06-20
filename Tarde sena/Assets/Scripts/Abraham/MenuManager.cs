using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject[] objetosADesactivar;

    private bool on = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ControladorDelMenu();
            ControladorDelTiempo();
        }
    }

    public void ControladorDelMenu()
    {
        on = !on;
        menuPausa.SetActive(on);

        if (!on)
        {
            DesactivarObjetos();
        }
    }

    public void ControladorDelTiempo()
    {
        if (on)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void DesactivarObjetos()
    {
        foreach (GameObject objeto in objetosADesactivar)
        {
            objeto.SetActive(false);
        }
    }
}
