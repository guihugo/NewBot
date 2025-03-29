using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropContainer : MonoBehaviour, IDropHandler, ContainerInterface
{
    public MECRECGerenciador gerente;

    public int index;   // posicao/indice do container
    public bool isAchorned = false; // esta preenchido

    public void OnDrop(PointerEventData eventData)
    {
        if (isAchorned) return;        //Se est� prenchido nada acontece

        GameObject droppedObject = eventData.pointerDrag; // Pega o objeto que est� sendo arrastado
        if (droppedObject != null && droppedObject.GetComponent<Itens>() != null )
        {
            droppedObject.transform.SetParent(transform); // Define como filho do container
            droppedObject.transform.localPosition = Vector3.zero; // Centraliza dentro do container
            isAchorned = true;
            //tocar evento de preenchimento do padr�o realizado
        }
    }
}

