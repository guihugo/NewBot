using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Direction : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;


    public int direction;

    public bool isCloned = false;
    private GameObject clonedObject;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isCloned)
        {
            clonedObject = Instantiate(gameObject, canvas.transform);
            Direction cloneDrag = clonedObject.GetComponent<Direction>();

            cloneDrag.isCloned = true;

            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
            clonedObject.GetComponent<RectTransform>().anchoredPosition = localPoint;


            clonedObject.transform.SetAsLastSibling();


            cloneDrag.OnBeginDrag(eventData);
            cloneDrag.OnDrag(eventData);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isCloned)
        {

            OnDrag(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isCloned)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out localPoint
            );

            GetComponent<RectTransform>().anchoredPosition = localPoint;

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}