using UnityEngine;
using UnityEngine.EventSystems;

public class CloneAndDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas; 
    private GameObject clonedObject; 
    private RectTransform clonedRectTransform;
    private CanvasGroup clonedCanvasGroup;
    internal bool isCloned;

    public void OnPointerDown(PointerEventData eventData)
    {
        
        clonedObject = Instantiate(gameObject, canvas.transform);
        clonedRectTransform = clonedObject.GetComponent<RectTransform>();
        clonedCanvasGroup = clonedObject.GetComponent<CanvasGroup>();

        
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
        clonedRectTransform.anchoredPosition = localPoint;

        
        clonedCanvasGroup.blocksRaycasts = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
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
