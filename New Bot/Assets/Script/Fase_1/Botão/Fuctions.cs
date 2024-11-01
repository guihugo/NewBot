using UnityEngine;
using UnityEngine.EventSystems;

public class CloneAndDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas; // Referência ao Canvas
    private GameObject clonedObject; // Referência ao objeto clonado
    private RectTransform clonedRectTransform; // RectTransform do objeto clonado
    private CanvasGroup clonedCanvasGroup; // CanvasGroup do objeto clonado

    public void OnPointerDown(PointerEventData eventData)
    {
        // Criar uma cópia do objeto a ser clonado
        clonedObject = Instantiate(gameObject, canvas.transform);
        clonedRectTransform = clonedObject.GetComponent<RectTransform>();
        clonedCanvasGroup = clonedObject.GetComponent<CanvasGroup>();

        // Ajustar a posição do objeto clonado para a posição do cursor
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
        clonedRectTransform.anchoredPosition = localPoint;

        // Permitir arrastar o clone
        clonedCanvasGroup.blocksRaycasts = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Nada a fazer no início do arraste
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Mover o objeto clonado com o cursor
        if (clonedRectTransform != null)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
            clonedRectTransform.anchoredPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Final do arraste - Impedir que o clone continue sendo arrastado
        if (clonedObject != null)
        {
            // Permitir que o clone bloqueie raycasts novamente
            clonedCanvasGroup.blocksRaycasts = true;
        }

        // Não mover o objeto pai
        clonedObject = null; // Limpar a referência do objeto clonado
    }
}
