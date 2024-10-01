using System;
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
        Debug.Log("Drop event triggered");

        if (eventData.pointerDrag != null)
        {
            Debug.Log("Dragged object: " + eventData.pointerDrag.name);

            Direction dragObject = eventData.pointerDrag.GetComponent<Direction>();

            if (dragObject != null)
            {
                Debug.Log("Drag component found on: " + eventData.pointerDrag.name);
                string objetoTag = eventData.pointerDrag.tag;

                int objectValue = dragObject.direction;

                Debug.Log("Object value: " + objectValue);

                gameManager.Numeros.Add(objectValue);
            }
            else
            {
                Debug.Log("No Drag component found on dragged object");
            }
        }
        else
        {
            Debug.Log("No object was dragged");
        }
    }

}
