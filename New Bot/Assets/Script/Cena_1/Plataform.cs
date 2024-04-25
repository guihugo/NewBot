using UnityEngine;
using UnityEngine.EventSystems;

public class Plataform : MonoBehaviour, IDropHandler
{
    private GameManager gameManager; // Referência ao GameManager
    public void Awake(){

        gameManager = FindObjectOfType<GameManager>(); // Encontra o objeto GameManager na cena

    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

             if (gameManager != null)
            {
                gameManager.AddMoviment();
            }
        }

    }

}
