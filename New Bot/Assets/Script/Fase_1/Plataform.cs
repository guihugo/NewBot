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

        if (eventData.pointerDrag != null)
        {

            Direction dragObject = eventData.pointerDrag.GetComponent<Direction>();

            if (dragObject != null)
            {

                int directionValue = dragObject.direction;

                gameManager.Numeros.Add(directionValue);


            }

        }

    }

}
