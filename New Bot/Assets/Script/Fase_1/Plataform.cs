using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Plataform : MonoBehaviour, IDropHandler
{
    private GameManager gameManager;
    private RectTransform rectTransform;
    public GridLayoutGroup gridLayoutGroup; 

    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {

            Direction dragObject = eventData.pointerDrag.GetComponent<Direction>();

            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            if (dragObject != null)
            {
                int directionValue = dragObject.direction;
                gameManager.Numeros.Add(directionValue);
             
            }
        }
    }
}
