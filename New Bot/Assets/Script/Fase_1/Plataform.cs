using UnityEngine;
using UnityEngine.EventSystems;

public class Plataform : MonoBehaviour, IDropHandler
{
    private GameManager gameManager;
    
    public void Awake(){

        gameManager = FindObjectOfType<GameManager>();

    }
    public void OnDrop(PointerEventData eventData)
    {
        Drag dragObject = eventData.pointerDrag.GetComponent<Drag>();


        if (dragObject != null)
        {
            string objetoTag = eventData.pointerDrag.tag;

            int objectValue = dragObject.direction;

            gameManager.Numeros.Add(objectValue);

            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition; PUSH


        }

    }

}
