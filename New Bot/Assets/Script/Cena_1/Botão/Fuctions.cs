using UnityEngine;
using UnityEngine.EventSystems;

public class Fuctions : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler,IDropHandler //Para Drag&Drop
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake(){
        
        // Obtém referências aos componentes necessário
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData) //Começar a arrastar objeto
    {
        //Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) //Sendo Arrastado
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; //Move com o mouse
    }

    public void OnEndDrag(PointerEventData eventData) //Soltar o objeto
    {
        //Debug.Log("OnEndDrag");
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData) //Click no objeto
    {
       //Debug.Log("OnPointerDown");
       //Adicionar ação quando é clicado
    }

    public void OnDrop(PointerEventData eventData) //Objeto é solto
    {
        
    }

}
