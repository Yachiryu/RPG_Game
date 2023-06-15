using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescripcionObjeto : MonoBehaviour
{
    public GameObject panelPrefab;
    public Canvas canvas;

    private GameObject panelActual;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && EstaElMouseEncima())
        {
            if (panelActual != null)
            {
                Destroy(panelActual);
                panelActual = null;
            }

            Vector2 panelPosition = GetMousePositionOnCanvas();

            panelActual = Instantiate(panelPrefab, panelPosition, Quaternion.identity);

            panelActual.transform.SetParent(canvas.transform, false);
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

    private Vector2 GetMousePositionOnCanvas()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 panelPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, Input.mousePosition, null, out panelPosition);

        return panelPosition;
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
