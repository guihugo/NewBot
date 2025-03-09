using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropContainer : MonoBehaviour, IDropHandler
{
   
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag; // Pega o objeto que está sendo arrastado

        if (droppedObject != null && droppedObject.GetComponent<Itens>() != null )
        {
            
            droppedObject.transform.SetParent(transform); // Define como filho do container
            droppedObject.transform.localPosition = Vector3.zero; // Centraliza dentro do container
        }
    }
}

