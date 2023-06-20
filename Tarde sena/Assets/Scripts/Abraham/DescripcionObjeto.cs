using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescripcionObjeto : MonoBehaviour
{
    public GameObject panelPrefab;
    public Canvas canvas;

    private GameObject panelActual;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && EstaElMouseEncima() && !EstaSobreObjetoUI())
        {
            if (panelActual != null)
            {
                MoverPanelExistente();
            }
            else
            {
                CrearNuevoPanel();
            }
        }
    }

    private bool EstaElMouseEncima()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }

    private bool EstaSobreObjetoUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void CrearNuevoPanel()
    {
        Vector2 mousePosition = Input.mousePosition;
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        Vector2 panelPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, mousePosition, null, out panelPosition);

        panelActual = Instantiate(panelPrefab, canvas.transform);
        panelActual.transform.localPosition = panelPosition;
    }

    private void MoverPanelExistente()
    {
        Vector2 mousePosition = Input.mousePosition;
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        Vector2 panelPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, mousePosition, null, out panelPosition);

        panelActual.transform.localPosition = panelPosition;
    }

    private void OnMouseExit()
    {
        if (panelActual != null)
        {
            Destroy(panelActual);
            panelActual = null;
        }
    }
}
