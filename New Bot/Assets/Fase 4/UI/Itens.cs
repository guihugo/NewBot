using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Itens : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform parentBeforeDrag;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>(); // Obtém o Canvas automaticamente
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentBeforeDrag = transform.parent;
        transform.SetParent(canvas.transform, true); // Mantém a hierarquia correta
        canvasGroup.blocksRaycasts = false; // Permite que containers detectem o drop
        canvasGroup.alpha = 0.6f; // Deixa a imagem semi-transparente
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Converte a posição do mouse para coordenadas locais do Canvas
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint
        );

        rectTransform.anchoredPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // Permite que o item seja detectado novamente
        canvasGroup.alpha = 1f; // Restaura a opacidade

        // Tenta detectar um DropContainer válido
        GameObject dropTarget = GetDropTarget(eventData);
        if (dropTarget != null && dropTarget.CompareTag("DropContainer"))
        {
            transform.SetParent(dropTarget.transform, false); // Define o container como novo pai

            // Converte a posição do mouse para o espaço local do novo container
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                dropTarget.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint
            );

            rectTransform.anchoredPosition = localPoint;
        }
        else
        {
            transform.SetParent(parentBeforeDrag, false); // Volta ao local original
            rectTransform.anchoredPosition = Vector2.zero; // Garante que fique no lugar certo
        }
    }

    private GameObject GetDropTarget(PointerEventData eventData)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = eventData.position
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("DropContainer"))
            {
                return result.gameObject;
            }
        }
        return null;
    }
}
