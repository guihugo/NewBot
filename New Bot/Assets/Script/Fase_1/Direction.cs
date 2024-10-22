using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Direction : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField] public Canvas canvas;
    public RectTransform rectTransform;
    public CanvasGroup canvasGroup;


    public int direction;

    private bool isAnchored;

    public bool isCloned = false;
    private GameObject clonedObject;

    private void Awake()
    {
        isAnchored = false;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    if (!isCloned)
    //    {
    //        clonedObject = Instantiate(gameObject, canvas.transform);
    //        Direction cloneDrag = clonedObject.GetComponent<Direction>();

    //        cloneDrag.isCloned = true;

    //        Vector2 localPoint;
    //        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
    //        clonedObject.GetComponent<RectTransform>().anchoredPosition = localPoint;


    //        clonedObject.transform.SetAsLastSibling();


    //        cloneDrag.OnBeginDrag(eventData);
    //        cloneDrag.OnDrag(eventData);
    //    }
    //}

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("begin Drag");
        if (isAnchored == false)
        {
            // Cria uma duplicata do objeto para ser arrastada.
            clonedObject = GameObject.Instantiate(this.gameObject);
            //clonedObject.transform.SetParent(GameObject.Find("PainelAcoes").transform);
            clonedObject.GetComponent<RectTransform>().position = rectTransform.position;
            clonedObject.GetComponent<RectTransform>().rotation = rectTransform.rotation;
            clonedObject.GetComponent<RectTransform>().localScale = rectTransform.localScale;
        }
        else
        {
            // Adiciona um marcador de posição nulo à fila de arrasto.
            //GameObject.Find("PainelAcoes").GetComponent<Fila>().Add(null, anchoredPlaceholder.GetComponent<DND_DropReceiver>().GetPosition());
        }

        // Ajusta a opacidade e bloqueia cliques no objeto arrastável original.
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("on drag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
